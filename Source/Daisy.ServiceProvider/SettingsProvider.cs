using System;
using System.Configuration;

using Daisy.ServiceProvider.Interfaces;


namespace Daisy.ServiceProvider
{
    public class SettingsProvider : ISettingsProvider
    {
        public string GetServerAddress()
        {
            return ConfigurationManager.AppSettings["DaisyServerAddress"];
        }
    }
}
