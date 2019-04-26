using System;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using Emails.Views;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Emails
{
    public class Mailer
    {
        static Mailer()
        {
            _website = CreateWebHostBuilder(new string[] { }).Build();
            //we won't ever call : _website.Run();
        }

        public static IWebHost _website;

        public class Startup
        {
                   
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddRazorOptions(o =>
                {
                    // Webapp.Netcore won't work without this.
                    // Interestingly Webapp.NetFramework works without this.                   
                    var modules = System.AppDomain.CurrentDomain.GetAssemblies().Where((x => !x.IsDynamic));
                    foreach (var module in modules)
                    {
                        o.AdditionalCompilationReferences.Add(MetadataReference.CreateFromFile(module.Location));
                    }
                });

                services.AddTransient<RazorViewToStringRenderer>();
                services.AddTransient<Mailer>();
            }
            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            {
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });
            }
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseSetting(WebHostDefaults.ApplicationKey, "RazorEmail")
                .UseStartup<Startup>();

        public async Task<string> SendTestEmail(string to, string subject, TestViewData viewData)
        {            
            using (var scope = _website.Services.CreateScope())
            {
                var renderer = scope.ServiceProvider.GetService(typeof(RazorViewToStringRenderer)) as RazorViewToStringRenderer;
                var htmlBody = await renderer.RenderViewToStringAsync("Test", viewData);
                // TODO: Inline CSS of the HTML body

                var message = new MailMessage("sender@example.com", to, subject, htmlBody);

                // TODO: send message

                Console.WriteLine(message.Body);

                return message.Body;
            }          
        }
    }
}
