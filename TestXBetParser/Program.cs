using Microsoft.Extensions.DependencyInjection;
using System;
using TestXBetParser.Services;

namespace TestXBetParser
{
    class Program
    {
        private static IServiceProvider ServiceProvider { get; set; }
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            var services = new ServiceCollection();

            services.AddTransient<IParserService>(s => new ParserService("https://ua1xbet.com/live/Football"));

            ServiceProvider = services.BuildServiceProvider().CreateScope().ServiceProvider;

            var parserService = ServiceProvider.GetRequiredService<IParserService>();

            parserService.SeleniumParse();

            parserService.PrintData();

            var input = "";

            Console.WriteLine("Enter MatchId to get its RateData or enter 'c'(or Ctrl+c) to exit :");

            while ((input = Console.ReadLine()) != "c")
            {
                if (int.TryParse(input, out var id))
                {
                    var match = parserService.GetMatchById(id);

                    if (match != null)
                    {

                        match.PrintRates();
                    }
                    else
                    {
                        Console.WriteLine("Match with Id = {id} not exist in parsed data!");
                    }
                }

                Console.WriteLine("Enter MatchId to get its RateData or enter 'c'(or Ctrl+c) to exit :");
            }

            Console.WriteLine("Enter 'c'(or Ctrl+c) to exit");
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Notified of a thread exception... application is terminating.");
        }
    }
}
