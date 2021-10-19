using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusParse.Core.ParserCore
{
    internal interface IParser
    {
        Task<List<string>> Crawl();
        Task<Dictionary<string, string>> Parse(List<string> parr);

    }
}
