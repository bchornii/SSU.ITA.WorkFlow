using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using Moq;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DomainServices.UnitTests
{
    [TestClass]
    public class EmployeeInformationServiceUnitTest
    {
        [TestMethod]
        public async Task TestFetchEmployeeNames()
        {
            IEmpInformationService _service = Mock.Of<IEmpInformationService>();
            IEnumerable<IEmpInitialsDto> data = await _service.FetchEmployeeNames();

            Assert.IsNotNull(data);
        }
    }
}
