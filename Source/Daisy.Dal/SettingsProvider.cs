using System;
using System.Configuration;


namespace Daisy.Dal
{
    public static class SettingsProvider
    {
        public static string GetDbConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Daisy"].ConnectionString;
        }
    }
}
