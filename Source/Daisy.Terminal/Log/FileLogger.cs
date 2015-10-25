using System;

using NLog;


namespace Daisy.Terminal.Log
{
    public sealed class FileLogger : ILogger
    {
        private static readonly Logger _instance;


        static FileLogger()
        {
            LogManager.ReconfigExistingLoggers();
            _instance = LogManager.GetCurrentClassLogger();
        }


        public void Error(Exception ex)
        {
            _instance.Error(ex);
        }


        public void Error(string message, Exception ex)
        {
            _instance.Error(ex, message);
        }


        public void Error(string message)
        {
            _instance.Error(message);
        }


        public void Info(string message)
        {
            _instance.Info(message);
        }
    }
}
