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
            //var builder = new ConfigurationBuilder()
            // .SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json");

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
            get { return _configuration["apiKey"]; }
        }
    }
}
