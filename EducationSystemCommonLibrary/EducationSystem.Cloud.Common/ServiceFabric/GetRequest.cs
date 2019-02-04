using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EducationSystem.Cloud.Common.ServiceFabric
{
    public sealed class GetRequest : IRestRequest<Object>
    {
        public string Route { get; private set; }
        public HttpMethod HttpMethod { get; private set; }
        public Dictionary<string, object> Headers { get; private set; }
        public object Body { get; set; }

        public GetRequest(string route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                throw new ArgumentNullException(nameof(route));
            }
            HttpMethod = HttpMethod.Get;
            Headers = new Dictionary<string, object>();
        }
    }
}
