using System;

using NLog;


namespace Daisy.Service.Log
{
    public sealed class FileLogger : ILogger
    {
        private static readonly Logger _instance;


        static FileLogger()
        {
            LogManager.ReconfigExistingLoggers();
            _instance = LogManager.GetCurrentClassLogger();
        }


        public void Error(string message)
        {
            _instance.Error(message);
        }


        public void Error(Exception ex)
        {
            _instance.Error(ex);
        }


        public void Info(string message)
        {
            _instance.Info(message);
        }
    }
}
