using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Education.BusinessServices.Contracts;
using Education.DataServices.Contracts;
using Education.Utitlites.Logging;
using Education.DataServices;
using Education.BusinessServices;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Education.Helpers;
using Education.Data.Common;
using Education.Domains.School.Repositories;
using Education.Domains.School.Entities;
using System;
using Education.Domains.School;

namespace Education.Services.Api.App_start
{
    public class Startup
    {
        const string schoolConnectionstring = "schoolCon";
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ILoggingService, LoggingService>();
            services.AddTransient<ISchoolDataService, SchoolDataService>();
            services.AddTransient<ISchoolBusinessService, SchoolBusinessService>();
            services.AddTransient<IGenericRepository<RegistrationRequest>, RegistrationRepository>();
            services.AddDbContext<SchoolDBContext>(option => option.UseSqlServer
            (ConfigHelper.GetConnectionString(schoolConnectionstring), options => options.CommandTimeout(60))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll));
            services.AddMvc();

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
