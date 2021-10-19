using AngleSharp;
using AngleSharp.Dom;
using RusParse.Core.Configs;
using RusParse.Core.Entities;
using System.Text.RegularExpressions;

namespace RusParse.Core.ParserCore
{
    internal class Parser : IParser
    {
        ParserConfig config;

        public Parser(ConfigurationEntity confEntity)
        {
            this.config = confEntity.parserConf;
        }

        public Parser(ParserConfig conf)
        {
            this.config = conf;
        }

        public async Task<List<string>> Crawl()
        {
            using var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            List<string> parr = new List<string>();

            int pageItter = 1;
            const int pageSize = 40;
            bool havePages = true;
            while (havePages)
            {
                string url = $"{config.url}/search/?df={config.date_df}&dt={config.date_now}&sw={config.title_encode}&p=page_{pageItter}";
                using var doc = await context.OpenAsync(url);
                
                var data = doc.QuerySelectorAll(config.selector)?.Select(el => config.url + el.Attributes["href"].Value)
                    .Where(el => !el.EndsWith('/')).Distinct().ToList();

                if (data.Count < pageSize)
                    havePages = false;
                
                parr.AddRange(data);
                pageItter++;
            }
            
            return parr;
        }


        public async Task<Dictionary<string, string>> Parse(List<string> parr)
        {
            using var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            Dictionary<string, string> text = new Dictionary<string, string>();
            Regex regex = new Regex(@"<[^<>]+>");
            var docs = new List<Task<IDocument>>();

            int batch_count = 5;
            int repet_cycle = 0;
            var batch_data = parr.Skip(repet_cycle).Take(batch_count);
            do
            {
                Console.WriteLine($"batch {(repet_cycle / batch_count) + 1}; elements completed {repet_cycle - batch_count + batch_data.Count()}");
                batch_data = parr.Skip(repet_cycle).Take(batch_count);
                if (batch_data.Count() == 0) break;
                docs.Clear();

                foreach (var url in batch_data)
                    docs.Add(context.OpenAsync(url));

                Task.WaitAll(docs.ToArray());

                foreach (var t in docs)
                {
                    var res = t.Result;
                    string title = res.QuerySelector("title").InnerHtml;
                    var data = res?.QuerySelectorAll("p").Select(el => el.InnerHtml).ToList();
                    data.Insert(0, title + '\n');
                    var data_join = string.Join('\n', data);
                    data_join = regex.Replace(data_join, string.Empty);
                    text.Add(res.BaseUrl.ToString(), data_join);
                    res.Dispose();
                }

                repet_cycle += batch_count;
            } while (batch_data.Count() > 0);

            return text;
        }
    }
}