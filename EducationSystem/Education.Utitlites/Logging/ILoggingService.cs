using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Utitlites.Logging
{
    public interface ILoggingService
    {
        void Log(string message);
        void Log(Exception exception);
    }
}
