using System;
using System.Reflection;
using System.ServiceModel.Web;

using Daisy.Service;

using Ninject;


namespace Daisy.Server
{
    internal class Program
    {
        private static void BuildDependencies()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
        }


        private static void Main(string[] args)
        {
            BuildDependencies();
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
