using System;


namespace Daisy.Service.Log
{
    public interface ILogger
    {
        void Error(string message);
        void Info(string message);
    }
}
