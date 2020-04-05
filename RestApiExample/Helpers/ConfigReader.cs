using Microsoft.Extensions.Configuration;

namespace RestApiExample.Helpers
{
    public class ConfigReader
    {
        public IConfigurationRoot Configuration { get; set; }

        public IConfiguration GetConfig()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            return Configuration = configurationBuilder.Build();
        }
    }
}
