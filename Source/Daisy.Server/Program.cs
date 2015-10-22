using System;
using System.ServiceModel.Web;

using Daisy.Service;


namespace Daisy.Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var host = new WebServiceHost(typeof(DaisyService)))
            {
                host.Open();
                Console.WriteLine("Service is running");
                Console.WriteLine("Press enter to quit...");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
