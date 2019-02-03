using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education.BusinessServices;
using Education.BusinessServices.Contracts;
using Education.Data.Common;
using Education.DataServices;
using Education.DataServices.Contracts;
using Education.Domains.School.Entities;
using Education.Utitlites.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Education.Domains.School.Repositories;
using Education.Domains.School;

namespace EducationSystemServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ILoggingService, LoggingService>();
            services.AddTransient<ISchoolDataService, SchoolDataService>();
            services.AddTransient<ISchoolBusinessService, SchoolBusinessService>();
            services.AddTransient<IGenericRepository<RegistrationRequest>, RegistrationRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggingService loggerFactory
           )
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<SchoolDBContext>();
                    context.Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                loggerFactory.Log(ex);
            }
            app.UseMvc();
        }
    }
}
