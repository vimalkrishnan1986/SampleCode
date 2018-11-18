using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education.BusinessServices.Contracts;
using Education.Utitlites.Logging;

namespace Education.Services.Api.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected ILoggingService LoggingService { get; private set; }

        protected ControllerBase(ILoggingService loggingService)
        {
            LoggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

    }
}
