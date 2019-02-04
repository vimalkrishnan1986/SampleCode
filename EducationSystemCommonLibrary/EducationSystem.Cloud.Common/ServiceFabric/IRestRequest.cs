using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EducationSystem.Cloud.Common.ServiceFabric
{
    public interface IRestRequest<T>
    {
        string Route { get; }
        HttpMethod HttpMethod { get; }
        Dictionary<string, object> Headers { get; }
        T Body { get; set; }
    }
}
