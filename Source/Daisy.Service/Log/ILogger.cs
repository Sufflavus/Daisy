using System;


namespace Daisy.Service.Log
{
    public interface ILogger
    {
        void Error(string message);
        void Error(Exception ex);
        void Info(string message);
    }
}
