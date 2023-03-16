using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.Core.Utils
{
    /// <summary>
    /// Interface to access the values of the configuration of the website
    /// </summary>
    public interface IWebConfigService
    {
        /// <summary>
        /// Get a value in appsettings.json converted to string
        /// </summary>
        /// <param name="value">Name of the value to get</param>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-1.9
        /// <returns>Value in string format if exist or string.Empty if not </returns>
        string GetStringValue(string value);

        /// <summary>
        /// Get a value in appsettings.json converted to int
        /// </summary>
        /// <param name="value">Name of the value to get</param>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-1.9
        /// <returns>Value in int format if exist or Zero if not </returns>
        int GetIntValue(string value);

        /// <summary>
        /// Verify if value in appsettings.json exists
        /// </summary>
        /// <param name="value">Name of the value to verify</param>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-1.9
        /// <returns>True if exists False otherwise</returns>
        bool ValueExists(string value);
    }
}
