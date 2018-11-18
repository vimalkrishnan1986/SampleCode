using System;
using System.Threading.Tasks;
using Education.Utitlites.Logging;
using Education.DataServices.Contracts;
using Education.Domains.School.Entities;

namespace Education.DataServices
{
    public sealed class SchoolDataService : ISchoolDataService
    {
        private readonly ILoggingService _loggingService;

        public SchoolDataService(ILoggingService loggingService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }


        public async Task<bool> Register(RegistrationModel model)
        {
            return await Task.FromResult(true);
        }
    }
}
