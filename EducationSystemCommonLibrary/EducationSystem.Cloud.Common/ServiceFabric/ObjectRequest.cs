using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EducationSystem.Cloud.Common.ServiceFabric
{
    public class ObjectRequest<T> : IRestRequest<T> where T : class
    {
        public string Route { get; private set; }
        public T Body { get; set; }
        public Dictionary<string, object> Headers { get; private set; }

        public HttpMethod HttpMethod
        {
            get;
            private set;
        }

        public ObjectRequest(string route, T body, HttpMethod httpMethod)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            if (string.IsNullOrWhiteSpace(route))
            {
                throw new ArgumentNullException(nameof(route));
            }
            HttpMethod = httpMethod ?? throw new ArgumentNullException(nameof(httpMethod));
            Headers = new Dictionary<string, object>();
        }
    }
}
