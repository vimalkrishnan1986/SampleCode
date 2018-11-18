using System;

namespace Education.Utitlites.Logging
{
    public sealed class LoggingService : ILoggingService
    {
        public void Log(string message)
        {
            Console.Write($"{DateTime.UtcNow} : {message}");
        }

        public void Log(Exception exception)
        {
            Log($"Error : {exception.Message} , details {exception.InnerException?.Message} stacktrace: {exception.StackTrace}");
        }
    }
}
