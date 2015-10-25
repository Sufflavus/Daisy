using System;


namespace Daisy.Terminal.Log
{
    public interface ILogger
    {
        void Error(string message);
        void Error(Exception message);
        void Error(string message, Exception ex);
        void Info(string message);
    }
}
