using System;
using Microsoft.AspNetCore.Mvc;
using Education.Utitlites.Logging;

namespace Education.Services.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ILoggingService LoggingService { get; private set; }

        protected BaseController(ILoggingService loggingService)
        {
            LoggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

    }
}
