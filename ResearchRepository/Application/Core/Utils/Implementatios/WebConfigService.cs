using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.Core.Utils.Implementatios
{
    public class WebConfigService : IWebConfigService
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationSection webParams;

        public WebConfigService(IConfiguration configuration)
        {
            _config = configuration;
            webParams = _config.GetSection("WebParams");
            if (!webParams.Exists())
                throw new ArgumentException("Section: 'WebParams' doesn't exist in appsettings.json");            
        }

        public int GetIntValue(string value)
        {
            var result = 0;

            if (this.ValueExists(value))
            {
                var temp = webParams.GetSection(value).Value;
                try
                {
                    result = Int32.Parse(temp);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{temp}'");
                }
            }

            return result;
        }

        public string GetStringValue(string value)
        {
            var result = string.Empty;
            if (this.ValueExists(value))
            {
                result = webParams.GetSection(value).Value;
            }
            return result;
        }

        public bool ValueExists(string value)
        {
            var result = webParams.GetSection(value).Exists();
            return result;
        }
    }
}
