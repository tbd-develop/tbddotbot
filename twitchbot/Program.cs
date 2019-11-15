using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace twitchbot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CreateHostedService(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostedService(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => { services.AddHostedService<BotService>(); });
        }
    }
}