using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Cloud.Common.AppSettings
{
    public interface IAppSettingService
    {
        string ConnectionString { get; }
    }
}
