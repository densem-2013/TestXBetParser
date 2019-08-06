using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TestXBetParser.Services;

namespace TestXBetParser
{
    class Program
    {
        private static IServiceProvider ServiceProvider { get; set; }
        static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            var services = new ServiceCollection();

            services.AddTransient<IParserService>(s => new ParserService("https://ua1xbet.com/live/Football"));

            ServiceProvider = services.BuildServiceProvider().CreateScope().ServiceProvider;

            var parserService = ServiceProvider.GetRequiredService<IParserService>();

            var data = await parserService.ParseMatchesData();

            if (data==null )
                throw new ArgumentNullException(nameof(data));

            parserService.PrintData();
            
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Notified of a thread exception... application is terminating.");
        }
    }
}
