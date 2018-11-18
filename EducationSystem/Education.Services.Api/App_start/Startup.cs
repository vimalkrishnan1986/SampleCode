using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Education.BusinessServices.Contracts;
using Education.DataServices.Contracts;
using Education.Utitlites.Logging;
using Education.DataServices;
using Education.BusinessServices;
using Microsoft.Extensions.Configuration;

namespace Education.Services.Api.App_start
{
    public class Startup
    {
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
            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }

    }
}
