using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.ProjectOxford.Text.Test
{
    public class AppSettings
    {
        private IConfigurationRoot _configuration;
        private static AppSettings _instance;

        private AppSettings()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public static AppSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AppSettings();

                return _instance;
            }
        }

        public string ApiKey
        {
            get
            {
                var apiKey = _configuration["apiKey"];

                if (string.IsNullOrEmpty(apiKey))
                    throw new Exception("API key not found appsettings.json");

                return apiKey;
            }
        }
    }
}
