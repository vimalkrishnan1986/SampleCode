using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Education.BusinessServices.Contracts;
using Education.DataServices.Contracts;
using Education.Domains.School;
using Education.Services.Api.Controllers;
using Education.Utitlites.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Education.Domains.School.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Test.L0.Controller
{
    [TestClass]
    public class SchoolControllerTestL0
    {
        private Mock<ISchoolDataService> _mockSchoolDataService;
        private Mock<ISchoolBusinessService> _mockSchoolBusinessService;
        private Mock<ILoggingService> _mockLoggingService;
        private SchoolController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockLoggingService = new Mock<ILoggingService>();
            _mockSchoolBusinessService = new Mock<ISchoolBusinessService>();
            _mockSchoolDataService = new Mock<ISchoolDataService>();
        }


        #region Constructor Tests

        [TestMethod]
        public void SchoolControllerTestL0_Initialise()
        {
            // act
            _controller = new SchoolController(_mockSchoolBusinessService.Object, _mockLoggingService.Object);

            //assert
            _controller.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "loggingService")]
        public void SchoolControllerTestL0_Initialise_ThrowNullLoggerException()
        {
            // act
            _controller = new SchoolController(_mockSchoolBusinessService.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "schoolBusinessService")]
        public void SchoolControllerTestL0_Initialise_ThrowNullBusinessService()
        {
            // act
            _controller = new SchoolController(null, _mockLoggingService.Object);
        }

        #endregion

        #region  method tests
        [TestMethod]
        public async Task SchoolControllerTestL0_Register_Success()
        {
            // Assemble
            _mockSchoolBusinessService.Setup(m => m.Resgister(It.IsAny<RegistrationModel>())).Returns(Task.FromResult(true));
            _controller = new SchoolController(_mockSchoolBusinessService.Object, _mockLoggingService.Object);

            // Act
            var res = await _controller.Register(It.IsAny<Guid>(),  It.IsAny<RegistrationModel>());

            //Assert
            res.Should().NotBeNull();
            res.Should().BeAssignableTo(typeof(AcceptedResult));

        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task SchoolControllerTestL0_Register_Failure()
        {
            // Assemble
            _mockSchoolBusinessService.Setup(m => m.Resgister(It.IsAny<RegistrationModel>())).Throws(new OperationCanceledException());
            _controller = new SchoolController(_mockSchoolBusinessService.Object, _mockLoggingService.Object);

            // Act
            var res = await _controller.Register(It.IsAny<Guid>(), It.IsAny<RegistrationModel>());

        }
        #endregion
    }
}
