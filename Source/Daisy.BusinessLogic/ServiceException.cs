using System;


namespace Daisy.BusinessLogic
{
    public class ServiceException : Exception
    {
        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }


        public ServiceException(Exception innerException)
            : base("Service error occurred", innerException)
        {
        }
    }
}
