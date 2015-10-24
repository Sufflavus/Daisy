using System;


namespace Daisy.Terminal.Log
{
    public interface ILogger
    {
        void Error(string message);
        void Info(string message);
    }
}
