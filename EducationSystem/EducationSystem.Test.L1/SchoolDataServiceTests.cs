using System;
using System.Configuration;
using System.Reflection;
using Education.Domains.School.Entities;
using Education.Domains.School.Repositories;
using Education.Domains.School;
using Education.Data.Common;
using System.Threading.Tasks;
using Education.DataServices;
using Education.DataServices.Contracts;
using Education.Utitlites.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EducationSystem.Test.L1
{
    [TestClass]
    public class SchoolDataServiceTests
    {
        IGenericRepository<RegistrationRequest> _repository;
        ISchoolDataService _schoolDataService;
        SchoolDBContext _dbContext;

        [TestInitialize]

        public void Initialize()
        {
            ILoggingService _loggingService = Activator.CreateInstance<LoggingService>();
            var contextOpions = new DbContextOptionsBuilder<SchoolDBContext>().
                UseSqlServer(GetConnectionString("schoolCon")).Options;
            _dbContext = new SchoolDBContext(contextOpions);
            _dbContext.Database.EnsureCreated();
            _repository = new RegistrationRepository(_dbContext);
            _schoolDataService = new SchoolDataService(_loggingService, _repository);
        }


        [TestCleanup]
        public void CleannUp()
        {
            _dbContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task SchoolDataServiceTests_RegisterTest()

        {
            var model = new RegistrationRequest()
            {
                Name = "test"
            };

            var res = await _schoolDataService.Register(model);
            Assert.IsTrue(res);
        }

        private string GetConnectionString(String key)
        {
            var configuation = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);

            return configuation.ConnectionStrings.ConnectionStrings[key].ConnectionString;
        }

    }
}
