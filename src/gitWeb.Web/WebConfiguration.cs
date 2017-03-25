using gitWeb.Core.Features.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace gitWeb.Web
{
    public class WebConfig : IConfigurationRepository
    {
        private static string _repositoryPathSection = "repositoryPath";

        public void SavePath(string path)
        {
            WebConfigurationManager.AppSettings[_repositoryPathSection] = path;
        }

        public string LoadPath()
        {
            return WebConfigurationManager.AppSettings[_repositoryPathSection];
        }
    }
}