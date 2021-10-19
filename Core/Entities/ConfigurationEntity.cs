using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RusParse.Core.Configs;

namespace RusParse.Core.Entities
{
    internal class ConfigurationEntity
    {
        public ParserConfig parserConf;

        public ConfigurationEntity()
        {
            parserConf = new ParserConfig();
        }
    }
}
