using System;

namespace Boris.Utils.Logging
{
    public class NullLoggingService: ILoggingService
    {
        public void Log(string name, LogLevels level, string message)
        {
            
        }

        public void Log(string name, string message, Exception exception)
        {
        }
    }
}