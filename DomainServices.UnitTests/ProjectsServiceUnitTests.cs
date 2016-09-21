using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;
using SSU.ITA.WorkFlow.DataAccess.EF.Repository.Repositories;
using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace DomainServices.UnitTests
{
    [TestClass]
    public class ProjectsServiceUnitTests
    {
        [TestMethod]
        public void GetEmployees_createContext_atListOnce()
        {
            //Arrange
            var fixture = new ProjectsServiceFixture()
                                .InitializeContext()
                                .CreateService();
            //Act
            fixture.Service.GetEmployees(It.IsAny<int>(), It.IsAny<int[]>());
            //Assert
            Mock.Get(fixture.ContextFactoryMock).Verify(f => f.CreateContext(), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task GetEmployees_getEmployeesCount_2Returned()
        {
            //Arrange
            int managerId = 2;
            int[] employeesId = {3, 4, 5};
            var users = new List<UserInformation>
            {
                new UserInformation
                {
                    UserId = 5,
                    ManagerId = managerId,
                    IsConfirmed = true
                },
                new UserInformation
                {
                    UserId = 6,
                    ManagerId = managerId,
                    IsConfirmed = true
                },
                new UserInformation
                {
                    UserId = 7,
                    ManagerId = managerId,
                    IsConfirmed = true
                }
            };

            var fixture = new ProjectsServiceFixture()
                                .InitializeContext()
                                .InitializeUserInformationDbSet(users)
                                .CreateService();

            //Act
            var employeeList = await fixture.Service.GetEmployees(managerId, employeesId);

            //Assert
            Assert.AreEqual(2, employeeList.Count);
            CollectionAssert.AllItemsAreNotNull(employeeList.ToList());
        }
        
        [TestMethod]
        public async Task GetProjects_getProjectCount_3Returned()
        {
            //Arrange
            int managerId = 3;
            var projects = new List<UserProject>
            {
                new UserProject
                {
                    ProjectId = 0,
                    CreatorId = 3
                },
                new UserProject
                {
                    ProjectId = 1,
                    CreatorId = 3
                },
                new UserProject
                {
                    ProjectId = 2,
                    CreatorId = 3
                },
            };

            var fixture = new ProjectsServiceFixture()
                                .InitializeProjectsRepository()
                                .CreateProjectRepository(managerId, projects)
                                .CreateService();

            //Act
            var projectList = await fixture.Service.GetProjects(managerId);
            //Assert
            Assert.AreEqual(3, projectList.Count);
        }

        class ProjectsServiceFixture
        {
            public IWorkFlowDbContextFactory ContextFactoryMock { get; private set; }
            private IWorkFlowDbContext ContextMock { get; set; }
            private DbSet<UserInformation> UserInformationMock { get; set; }
            private IProjectRepository ProjectRepository { get; set; }
            public IProjectsService Service { get; private set; }

            public ProjectsServiceFixture InitializeContext()
            {
                ContextFactoryMock = new Mock<IWorkFlowDbContextFactory>().Object;
                ContextMock = new Mock<IWorkFlowDbContext>().Object;
                Mock.Get(ContextFactoryMock).Setup(f => f.CreateContext()).Returns(ContextMock);
                return this;
            }

            public ProjectsServiceFixture InitializeUserInformationDbSet(IList<UserInformation> data)
            {
                UserInformationMock = new Mock<DbSet<UserInformation>>().Object;
                UserInformationMock = QueryProviderSetUpAsync(data.AsQueryable(), UserInformationMock);
                Mock.Get(ContextMock).Setup(c => c.UserInformation)
                                     .Returns(UserInformationMock);
                return this;
            }

            public ProjectsServiceFixture InitializeProjectsRepository()
            {
                ProjectRepository = new Mock<IProjectRepository>().Object;
                return this;
            }
            public ProjectsServiceFixture CreateProjectRepository(int managerId, IEnumerable<UserProject> projectsToReturn)
            {
                Mock.Get(ProjectRepository).Setup(x => x.GetProjects(managerId))
                    .ReturnsAsync(projectsToReturn);
                return this;
            }

            public ProjectsServiceFixture CreateService()
            {
                Service = new ProjectsService(ContextFactoryMock, ProjectRepository);
                return this;
            }

            private DbSet<TEntity> QueryProviderSetUpAsync<TEntity>(IQueryable<TEntity> objData,
                                                                   DbSet<TEntity> dbSet) where TEntity : class
            {
                Mock.Get(dbSet).As<IDbAsyncEnumerable<TEntity>>()
                               .Setup(t => t.GetAsyncEnumerator())
                               .Returns(new TestDbAsyncEnumerator<TEntity>(objData.GetEnumerator()));
                Mock.Get(dbSet).As<IQueryable<TEntity>>()
                               .Setup(t => t.Provider)
                               .Returns(new TestDbAsyncQueryProvider<TEntity>(objData.Provider));
                Mock.Get(dbSet).As<IQueryable<TEntity>>()
                               .Setup(t => t.Expression)
                               .Returns(objData.Expression);
                Mock.Get(dbSet).As<IQueryable<TEntity>>()
                               .Setup(t => t.ElementType)
                               .Returns(objData.ElementType);
                Mock.Get(dbSet).As<IQueryable<TEntity>>()
                               .Setup(t => t.GetEnumerator())
                               .Returns(objData.GetEnumerator());
                return dbSet;
            }
        }
    }
}
