using NLog;

namespace Boris.Utils.Logging.NLog
{
    public class NlogLoggingService: ILoggingService
    {
        public void Log(string name, LogLevels level, string message)
        {
            LogManager.GetLogger(name).Log(MapLevels(level), message);
        }

        public void Log(string name, string message, System.Exception exception)
        {
            LogManager.GetLogger(name).LogException(LogLevel.Error, message, exception);
        }

        private static LogLevel MapLevels(LogLevels level)
        {
            switch (level)
            {
                case LogLevels.Trace:
                    return LogLevel.Trace;
                case LogLevels.Debug:
                    return LogLevel.Debug;
                case LogLevels.Info:
                    return LogLevel.Info;
                case LogLevels.Warning:
                    return LogLevel.Warn;
                case LogLevels.Exception:
                    return LogLevel.Error;
                default:
                    return LogLevel.Fatal;
            }
        }

    }
}