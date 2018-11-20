using System;
using System.Threading.Tasks;
using Education.Utitlites.Logging;
using Education.DataServices.Contracts;
using Education.Domains.School.Entities;
using Education.Domains.School.Common;
namespace Education.DataServices
{
    public sealed class SchoolDataService : ISchoolDataService
    {
        private readonly ILoggingService _loggingService;
        private readonly IGenericRepository<RegistrationRequest> _registrationRepository;

        private string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }
        public SchoolDataService(ILoggingService loggingService, IGenericRepository<RegistrationRequest> registrationRepository)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
            _registrationRepository = registrationRepository ?? throw new ArgumentNullException(nameof(registrationRepository));
        }


        public async Task<bool> Register(RegistrationRequest model)
        {
            _loggingService.Log($"Request has been recieved by {Name}");
            try
            {
                await _registrationRepository.InsertAsync(model);
                return true;
            }
            catch (Exception ex)
            {
                _loggingService.Log(ex);
            }
            return false;
        }
    }
}
