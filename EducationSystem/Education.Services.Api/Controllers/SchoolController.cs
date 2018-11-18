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
        public async Task<IActionResult> Register([FromHeader] Guid correlationId, RegistrationModel model)
        {
            LoggingService.Log($"Register Request has been recieved for correlationId {correlationId} ");
            try
            {
                if (await _schoolBusinessService.Resgister(model))
                {
                    return Accepted();
                }
                throw new OperationCanceledException();
            }

            catch (Exception ex)
            {
                LoggingService.Log(ex);
                throw;
            }
            finally
            {
                LoggingService.Log($"Register Request has been completed for correlationId {correlationId} ");
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
