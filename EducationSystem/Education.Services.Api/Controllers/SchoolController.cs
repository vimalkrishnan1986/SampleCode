using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Education.Utitlites.Logging;
using Education.Domains.School.Entities;
using Education.BusinessServices.Contracts;

namespace Education.Services.Api.Controllers
{
    [Route("api/SchoolService")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolBusinessService _schoolBusinessService;

        public SchoolController(ISchoolBusinessService schoolBusinessService, ILoggingService loggingService) :
            base(loggingService)
        {
            _schoolBusinessService = schoolBusinessService ?? throw new ArgumentNullException(nameof(schoolBusinessService));
        }


        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                return Accepted(await _schoolBusinessService.Resgister(model));
            }

            catch (Exception ex)
            {
                LoggingService.Log(ex);
                throw;
            }
        }


        [HttpGet]
        [Route("Sample")]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(Ok(new RegistrationModel
            {
                Name = "SampleName",
                Address = "SampleAddress"
            }));
        }
    }
}
