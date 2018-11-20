using Education.DataServices.Contracts;
using Education.Utitlites.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Education.BusinessServices.Contracts;
using Education.BusinessServices;
using System.Threading.Tasks;
using Education.Domains.School.Entities;

namespace EducationSystem.Test.L0.Business
{
    [TestClass]
    public sealed class SchoolBusinessServiceTest
    {
        private Mock<ISchoolDataService> _mockSchoolDataService;
        private Mock<ILoggingService> _mockLoggingService;

        [TestInitialize]
        public void Initialize()
        {
            _mockLoggingService = new Mock<ILoggingService>();
            _mockSchoolDataService = new Mock<ISchoolDataService>();
        }

        #region Constructur Tests

        [TestMethod]
        public void SchoolBusinessServiceTest_Create_Success()
        {
            ISchoolBusinessService schoolBusinessService = new SchoolBusinessService(_mockSchoolDataService.Object, _mockLoggingService.Object);
            schoolBusinessService.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "loggingService")]
        public void SchoolBusinessServiceTest_Create_LoggingNullError()
        {
            ISchoolBusinessService schoolBusinessService = new SchoolBusinessService(_mockSchoolDataService.Object, null);
            schoolBusinessService.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "schoolDataService")]
        public void SchoolBusinessServiceTest_Create_DataServiceNullError()
        {
            ISchoolBusinessService schoolBusinessService = new SchoolBusinessService(null, _mockLoggingService.Object);
            schoolBusinessService.Should().NotBeNull();
        }
        #endregion

        #region Method Tests

        [TestMethod]
        public async Task SchoolBusinessServiceTest_Register_Success()
        {
            //assemble
            _mockSchoolDataService.Setup(m => m.Register(It.IsAny<RegistrationRequest>())).Returns(Task.FromResult(true));
            ISchoolBusinessService schoolBusinessService = new SchoolBusinessService(_mockSchoolDataService.Object, _mockLoggingService.Object);

            //act
            var res = await schoolBusinessService.Resgister(It.IsAny<RegistrationRequest>());

            //assert
            res.Should().BeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task SchoolBusinessServiceTest_Register_Failure()
        {
            //assemble
            _mockSchoolDataService.Setup(m => m.Register(It.IsAny<RegistrationRequest>())).Throws(new Exception());
            ISchoolBusinessService schoolBusinessService = new SchoolBusinessService(_mockSchoolDataService.Object, _mockLoggingService.Object);

            //act
            var res = await schoolBusinessService.Resgister(It.IsAny<RegistrationRequest>());

            //assert
            res.Should().BeFalse();
        }
        #endregion
    }
}
