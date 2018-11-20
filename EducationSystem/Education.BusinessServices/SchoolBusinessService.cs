using Education.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Education.Domains.School.Entities;
using System.Threading.Tasks;
using Education.DataServices.Contracts;
using Education.Utitlites.Logging;

namespace Education.BusinessServices
{
    public sealed class SchoolBusinessService : ISchoolBusinessService
    {
        private readonly ISchoolDataService _schoolDataService;
        private readonly ILoggingService _loggingService;

        private string ServiceName
        {
            get
            {
                return this.GetType().Name;
            }

        }
        public SchoolBusinessService(ISchoolDataService schoolDataService, ILoggingService loggingService)
        {
            _schoolDataService = schoolDataService ?? throw new ArgumentNullException(nameof(schoolDataService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }
        public async Task<bool> Resgister(RegistrationRequest registrationModel)
        {
            _loggingService.Log($"Request has been recieved at {ServiceName} ");

            try
            {
                var res = await _schoolDataService.Register(registrationModel);
                if (!res)
                {
                    throw new Exception("There was exception happend in the  DB layer");
                }
                return res;
            }
            catch (Exception ex)
            {
                _loggingService.Log(ex);
                throw new OperationCanceledException();
            }
        }

    }
}
