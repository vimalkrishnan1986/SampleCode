using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Cloud.Common.ServiceFabric
{
    public interface IResolver
    {
        Uri GetRequestUrl(string relatieveUrl);
    }
}
