using Emails;
using Emails.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();
            var p = provider.GetRequiredService<Program>();
            await p.Run();
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // Not sure if this is needed!
            var applicationEnvironment = PlatformServices.Default.Application;
            services.AddSingleton(applicationEnvironment);

            // AddMailer will setup everything need for razor rendering, etc.
            services.AddMailer();

            services.AddSingleton<Program>();
        }

        readonly Mailer _mailer;

        public Program(Mailer mailer)
        {
            _mailer = mailer;
        }

        public async Task Run()
        {
            await _mailer.SendTestEmail("test@example.com", "Subject line", new TestViewData
            {
                Message = "Hi from Console"
            });

            Console.WriteLine("Done.");
            Console.WriteLine("Hit Enter...");
            Console.ReadLine();
        }
    }
}
