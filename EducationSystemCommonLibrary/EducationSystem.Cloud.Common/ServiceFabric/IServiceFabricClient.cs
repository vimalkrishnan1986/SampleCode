using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace EducationSystem.Cloud.Common.ServiceFabric
{
    public interface IServiceFabricClient<T> : IDisposable
    {
        Task<HttpResponseMessage> GetAsync(IRestRequest<T> request);

        Task<HttpResponseMessage> PostAsync(IRestRequest<T> restRequest);

    }
}
