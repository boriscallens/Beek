using System;

namespace Boris.Utils.Logging
{
    public interface ILoggingService
    {
        void Log(string name, LogLevels level, string message);
        void Log(string name, string message, Exception exception);
    }

    public enum LogLevels
    {
        Trace,
        Debug,
        Info,
        Warning,
        Exception
    }
}
