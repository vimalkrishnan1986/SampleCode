﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdcuationSystem.Core.Business;
using EdcuationSystem.Core.Business.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EducationSystem.Cloud.Common.ServiceBus;
namespace EcucationSystem.Core.Api.Services
{
    public class Startup
    {
        const string serviceBusConnectionString = "Endpoint=sb://educationsystembus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=iuIr6BCJKNacEFQHyIDqEgOK+oIPt/wBCQGBPlOeHAU=";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IMessageBusinessService, MessageBusinessService>();
            services.AddTransient<IMessageBusClient, MessageBusClient>();
            services.AddSingleton<ServiceBusSetting>(new ServiceBusSetting(serviceBusConnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
