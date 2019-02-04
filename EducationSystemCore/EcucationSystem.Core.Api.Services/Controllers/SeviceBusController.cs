using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EdcuationSystem.Core.Business.Interfaces;
using System.Net;


namespace EcucationSystem.Core.Api.Services.Controllers
{
    [Route("api/messagebus")]
    [ApiController]
    public class SeviceBusController : ControllerBase
    {
        private readonly IMessageBusinessService _messageBusinessService;

        public SeviceBusController(IMessageBusinessService messageBusinessService)
        {
            _messageBusinessService = messageBusinessService ?? throw new ArgumentNullException(nameof(messageBusinessService));
        }
        [HttpPost]
        [Route("post/{queue}")]
        public async Task<IActionResult> Post([FromBody] object payload, [FromRoute] string queue)
        {
            try
            {
                await _messageBusinessService.Send(payload, queue);
                return Accepted();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
