using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace DomainServices.UnitTests
{
    [TestClass]
    public class EmpInformationServiceUnitTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void FetchEmployeeNamesContextCreateAtLeastOnceTest()
        {
            var fixture = new EmployeeServiceFixture()
                              .InitializeContext()
                              .CreateService();
            // Act
            fixture.Service.FetchEmployeeNames(It.IsAny<int>());

            // Assert
            Mock.Get(fixture.ContextFactoryMock).Verify(f => f.CreateContext(), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task FetchEmployeeNamesTest()
        {
            // Arrange           
            int EmployeeManagerId = 2;
            var users = new List<UserInformation>
            {
                new UserInformation
                {
                    ManagerId = EmployeeManagerId,
                    IsConfirmed = true
                },
                new UserInformation
                {
                    ManagerId = EmployeeManagerId,
                    IsConfirmed = true
                },
                new UserInformation
                {
                    ManagerId = EmployeeManagerId,
                    IsConfirmed = true
                }
            };

            var fixture = new EmployeeServiceFixture()
                             .InitializeContext()
                             .InitializeUserInformationDbSet(users)
                             .CreateService();

            // Act
            var userListDto = await fixture.Service.FetchEmployeeNames(EmployeeManagerId);

            // Assert
            Assert.AreEqual(3, userListDto.Count);
            CollectionAssert.AllItemsAreNotNull(userListDto.ToList());
        }

        [TestMethod]
        public void FetchEmployeeProjectsListContextCreateAtLeastOnceTest()
        {
            // Arrange
            var fixture = new EmployeeServiceFixture()
                              .InitializeContext()
                              .CreateService();
            // Act
            fixture.Service.FetchEmployeeProjectsList(It.IsAny<int>());

            // Assert
            Mock.Get(fixture.ContextFactoryMock).Verify(f => f.CreateContext(), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task FetchEmployeeProjectsListTest()
        {
            // Arrange
            int userId = 1;
            var userProjectRelation = new List<UserProjectRelation>
            {
                new UserProjectRelation
                {
                     ProjectId = 1,
                     UserId = userId
                },
                new UserProjectRelation
                {
                    ProjectId = 2,
                    UserId = userId
                },
                new UserProjectRelation
                {
                    ProjectId = 3,
                    UserId = userId
                }
            };

            var projects = new List<UserProject>
            {
                new UserProject
                {
                    ProjectId = 1,                    
                    UserProjectRelation = userProjectRelation
                },
                new UserProject
                {
                    ProjectId = 2,                    
                    UserProjectRelation = userProjectRelation
                },
                new UserProject
                {
                    ProjectId = 3,                    
                    UserProjectRelation = userProjectRelation
                }
            };

            var fixture = new EmployeeServiceFixture()
                             .InitializeContext()
                             .InitializeUserProjectDbSet(projects)
                             .CreateService();

            // Act
            var result = await fixture.Service.FetchEmployeeProjectsList(userId);            

            // Assert
            Assert.AreEqual(3, result.Count);
            CollectionAssert.AllItemsAreNotNull(result.ToList());
        }

        [TestMethod]
        public void FetchEmployeeProjectsContextCreateAtLeastOnceTest()
        {
            var fixture = new EmployeeServiceFixture()
                             .InitializeContext()
                             .CreateService();

            // Act 
            fixture.Service.FetchEmployeeProjects(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Mock.Get(fixture.ContextFactoryMock).Verify(f => f.CreateContext(), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task FetchEmployeeProjectsTest()
        {
            // Arrange
            int userId = 1;
            int pageNum = 1;
            int numPerPage = 3;

            var userProjectRelation = new List<UserProjectRelation>
            {
                new UserProjectRelation
                {
                     ProjectId = 1,
                     UserId = userId
                },
                new UserProjectRelation
                {
                    ProjectId = 2,
                    UserId = userId
                },
                new UserProjectRelation
                {
                    ProjectId = 3,
                    UserId = userId
                }
            };

            var userProjects = new List<UserProject>
            {
                new UserProject
                {
                    ProjectId = 1,                                        
                    UserProjectRelation = userProjectRelation                    
                },
                new UserProject
                {
                    ProjectId = 2,                                  
                    UserProjectRelation = userProjectRelation                    
                },
                new UserProject
                {
                    ProjectId = 3,                                   
                    UserProjectRelation = userProjectRelation                    
                }
            };

            var fixture = new EmployeeServiceFixture()
                             .InitializeContext()
                             .InitializeUserProjectDbSet(userProjects)
                             .InitializeUserProjectRelationDbSet(userProjectRelation)
                             .CreateService();

            // Act
            var result = await fixture.Service.FetchEmployeeProjects(userId, pageNum, numPerPage) as PagerListDto;            

            // Assert
            Assert.AreEqual(3, result.TotalCount);                  
            CollectionAssert.AllItemsAreNotNull(result.Items.ToList());
        }

        class EmployeeServiceFixture
        {
            private IWorkFlowDbContext ContextMock { get; set; }
            private DbSet<UserInformation> UserInformationMock { get; set; }
            private DbSet<UserProject> UserProjectMock { get; set; }
            private DbSet<UserProjectRelation> UserProjectRelationMock { get; set; }
            public IWorkFlowDbContextFactory ContextFactoryMock { get; private set; }
            public IEmployeeService Service { get; private set; }

            public EmployeeServiceFixture InitializeContext()
            {
                ContextMock = new Mock<IWorkFlowDbContext>().Object;
                ContextFactoryMock = new Mock<IWorkFlowDbContextFactory>().Object;
                Mock.Get(ContextFactoryMock).Setup(f => f.CreateContext()).Returns(ContextMock);
                return this;
            }

            public EmployeeServiceFixture InitializeUserInformationDbSet(IList<UserInformation> data)
            {
                UserInformationMock = new Mock<DbSet<UserInformation>>().Object;
                UserInformationMock = QueryProviderSetUpAsync(data.AsQueryable(), UserInformationMock);
                Mock.Get(ContextMock).Setup(c => c.UserInformation)
                                     .Returns(UserInformationMock);
                return this;
            }

            public EmployeeServiceFixture InitializeUserProjectDbSet(IList<UserProject> data)
            {
                UserProjectMock = new Mock<DbSet<UserProject>>().Object;
                UserProjectMock = QueryProviderSetUpAsync(data.AsQueryable(), UserProjectMock);
                Mock.Get(ContextMock).Setup(c => c.UserProject)
                                     .Returns(UserProjectMock);
                return this;
            }

            public EmployeeServiceFixture InitializeUserProjectRelationDbSet(IList<UserProjectRelation> data)
            {
                UserProjectRelationMock = new Mock<DbSet<UserProjectRelation>>().Object;
                UserProjectRelationMock = QueryProviderSetUpAsync(data.AsQueryable(), UserProjectRelationMock);
                Mock.Get(ContextMock).Setup(c => c.UserProjectRelation)
                                     .Returns(UserProjectRelationMock);
                return this;
            }

            private DbSet<TEntity> QueryProviderSetUp<TEntity>(IQueryable<TEntity> objData,
                                                               DbSet<TEntity> dbSet) where TEntity : class
            {
                Mock.Get(dbSet).As<IQueryable<TEntity>>()
                               .Setup(t => t.Provider)
                               .Returns(objData.Provider);
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

            public EmployeeServiceFixture CreateService()
            {
                Service = new EmployeeService(ContextFactoryMock);
                return this;
            }
        }         
    }
}
