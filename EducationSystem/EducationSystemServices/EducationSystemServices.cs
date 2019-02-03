using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Data;
using EducationSystem.Cloud.Common.AppSettings;
using Education.Domains.School;
using Microsoft.EntityFrameworkCore;

namespace EducationSystemServices
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class EducationSystemServices : StatelessService
    {
        public EducationSystemServices(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {

                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    {
                        IAppSettingService appSettingService=new AppSettingService(serviceContext);
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        return new WebHostBuilder()
                                    .UseKestrel()
                                    .ConfigureServices
                                    (
                                        services =>
                                        { services
                                            .AddSingleton<StatelessServiceContext>(serviceContext);
                                            services.AddDbContext<SchoolDBContext>(option => option.UseSqlServer
                                              (appSettingService.ConnectionString, options => options.CommandTimeout(60))
                                             .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll));
                                            }
                                            )
                                             .UseContentRoot(Directory.GetCurrentDirectory())
                                              .UseStartup<Startup>()
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url)
                                    .Build();
                    }))
            };
        }
    }
}
