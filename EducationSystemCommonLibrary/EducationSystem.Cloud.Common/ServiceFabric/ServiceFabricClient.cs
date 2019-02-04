using Microsoft.ServiceFabric.Services.Remoting.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Cloud.Common.ServiceFabric
{
    public sealed class ServiceFabricClient<T> : IServiceFabricClient<T> where T : class
    {
        private readonly IResolver _resolver;

        public ServiceFabricClient(IResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private HttpRequestMessage CreateRequest(IRestRequest<T> restRequest)
        {
            if (restRequest == null)
            {
                throw new ArgumentNullException(nameof(restRequest));
            }

            var requestMessage = new HttpRequestMessage
            {
                Method = restRequest.HttpMethod
            };

            if (restRequest.Headers != null)
            {
                foreach (KeyValuePair<string, object> keyValuePair in restRequest.Headers)
                {
                    requestMessage.Headers.Add(keyValuePair.Key, keyValuePair.Value.ToString());
                }
            }
            return requestMessage;
        }
        public Task<HttpResponseMessage> GetAsync(IRestRequest<T> restRequest)
        {

            HttpClient httpClient = new HttpClient
            {
                BaseAddress = _resolver.GetRequestUrl(restRequest.Route)
            };

            HttpRequestMessage httpRequestMessage = CreateRequest(restRequest);

            return httpClient.SendAsync(httpRequestMessage);
        }

        public async Task<HttpResponseMessage> PostAsync(IRestRequest<T> restRequest)
        {
            if (restRequest == null)
            {
                throw new ArgumentNullException(nameof(restRequest));
            }
            var url = _resolver.GetRequestUrl(restRequest.Route);
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = url
            };

            return await httpClient.PostAsync(restRequest.Route, new StringContent(JsonConvert.SerializeObject(restRequest.Body)));
        }
    }
}
