using RusParse.Core.Configs;
using RusParse.Core.Managers;
using RusParse.Core.ParserCore;
using RusParse.Logger;
using System.Text;

class Program
{
    public static void Main(string [] args)
    {
        LogWriter logWriter = new LogWriter("Start point");
        ParserConfig config = new ParserConfig();
        WordDocManager docManager = new WordDocManager();

        logWriter.LogWrite("Config Created");
        Console.WriteLine("Config Created");
        Parser parser = new Parser(config);
        var data = parser.Crawl().Result;
        logWriter.LogWrite("Crawl completed");
        Console.WriteLine("Crawl completed");
        var result = parser.Parse(data).Result;
        logWriter.LogWrite("Parse completed");
        Console.WriteLine("Parse completed");
        Save(result);
        logWriter.LogWrite("Saving completed");
        Console.WriteLine("Saving completed");
        docManager.Run(result);
        logWriter.LogWrite("Wording completed");
        Console.WriteLine("Wording completed");

        Console.WriteLine("End");
    }

    public static void Save(Dictionary<string, string> data)
    {
        int i = 0;
        foreach (var line in data)
        {
            using StreamWriter file = new($@"C:\prog\mocks\RUS\{i++}.txt");
            file.WriteLine(line.Value);
            //await file.WriteLineAsync(line.Key.ToString() + "\n###\n###\n" + line.Value);
        }
    }
}