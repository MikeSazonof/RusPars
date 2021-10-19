using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusParse.Core.Configs
{
    public class ParserConfig
    {
        private readonly DefaultConfig defaultConfig;
        public readonly string url = "https://www.interfax.ru";
        public readonly string selector = ".sPageResult div div a";
        public readonly string title_selector = ".leftside article h1";
        public readonly string date_df = "01.01.2021";

        public string title = "Цифоровизация";
        public string title_encode = "%D6%E8%F4%F0%EE%E2%E8%E7%E0%F6%E8%FF";
        //public string title_encode = "%EF%F0%EE%E3%F0%E0%EC%EC%E8%F0%EE%E2%E0%ED%E8%E5";

        public readonly string date_now;

        public ParserConfig()
        {
            date_now = DateTime.Now.ToString("dd.MM.yyyy");
            defaultConfig = new DefaultConfig();
            UpdateConfig();
        }

        public void UpdateConfig()
        {
            defaultConfig.UpdateConfig();
            title = defaultConfig.defaultValues.ContainsKey("title") ? defaultConfig.defaultValues["title"] : title;
        }

        private class DefaultConfig
        {
            public Dictionary<string, string> defaultValues;
            public DefaultConfig()
            {
                defaultValues = new Dictionary<string, string>();
                UpdateConfig();
            }

            public void UpdateConfig()
            {
                foreach (string key in ConfigurationManager.AppSettings.AllKeys)
                {
                    defaultValues.Add(key.Trim().ToLower(), ConfigurationManager.AppSettings[key]);
                }
            }
        }
    }
}