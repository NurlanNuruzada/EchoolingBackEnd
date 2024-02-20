using Microsoft.Extensions.Configuration;

namespace Echooling.Persistance.Helper
{
    public static class Configuration
    {
        public static string ConnetionString { get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Directory.GetCurrentDirectory());
                configurationManager.AddJsonFile("appsettings.json");
                return  configurationManager.GetConnectionString("Default");
            }
        }
    }
}
