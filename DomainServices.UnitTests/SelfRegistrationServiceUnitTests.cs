using System;
using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;

namespace DomainServices.UnitTests
{
    [TestClass]
    public class SelfRegistrationServiceUnitTests
    {
        // Needs to be improved.
        [TestMethod]
        public void TestSelfRegisterUserAsync()
        {
            //Arrange
            IWorkFlowDbContext dbContext = new WorkFlowDbContext();


            var fixture = new SelfRegistrationServiceFixture()
                .BuildSelfRegistrationServiceFixture()
                .BuildWorkFlowDbContextFactory(dbContext);

            SelfRegistrationDto selfRegistrationDto = null;

            //Act
            fixture.Service.SelfRegisterUserAsync(selfRegistrationDto);

            //Assert
            Mock.Get(fixture.dbFactory).Verify(f => f.CreateContext(), Times.AtLeastOnce);
        }

        public class SelfRegistrationServiceFixture
        {
            public IWorkFlowDbContextFactory dbFactory { get; private set; }
            
            public IHasher hasher { get; private set; }

            public ISelfRegistrationService Service { get; private set; }

            public  SelfRegistrationServiceFixture BuildSelfRegistrationServiceFixture(){

                dbFactory = new Mock<IWorkFlowDbContextFactory>().Object;
                hasher = new Mock<IHasher>().Object;

                Service = new SelfRegistrationService(dbFactory, hasher);

                return this;
            }

            public SelfRegistrationServiceFixture BuildWorkFlowDbContextFactory(IWorkFlowDbContext contextToReturn)
            {
                Mock.Get(dbFactory).Setup(f => f.CreateContext()).Returns(contextToReturn);
                return this;
            }

            public SelfRegistrationServiceFixture BuildHasher()
            {
                //Mock.Get(hasher).Setup(f => f.ComputeHash(It.IsAny<string>, It.Is<string>(x => x.i)));
                return this;
            }
        } 

    }
}