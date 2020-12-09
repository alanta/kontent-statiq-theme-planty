using Kentico.Kontent.Delivery.Abstractions;
using System.Threading.Tasks;
using Kentico.Kontent.Delivery.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Planty.Models;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

namespace Planty.Generator
{
    class Program
    {
        public static async Task<int> Main(string[] args) =>
            await Bootstrapper
                .Factory
                .CreateDefault(args)
                .ConfigureServices((services, settings) =>
                { 
                    // pull in site settings from configuration
                    var siteSettings = (settings as IConfiguration).GetSection("Site").Get<SiteSettings>();
                    services.AddSingleton(siteSettings);

                    services.AddSingleton<ITypeProvider, CustomTypeProvider>();
                    services.AddDeliveryClient((IConfiguration)settings);
                })
                .AddHostingCommands()
                .RunAsync();
    }
}
