using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using System;

namespace ApiIsolated;
class program
{ 
    static async Task Main(string[] args)
    {

        var host = new HostBuilder()
                        .ConfigureFunctionsWorkerDefaults()
                        .ConfigureServices(services =>
                        {
                            services.AddSingleton(sp =>
                            {
                                return new CosmosClient(Environment.GetEnvironmentVariable("AZURE_COSMOS_ENDPOINT"), new CosmosClientOptions());
                            }
                            ); services.AddSingleton<QuotesRepository>();
                        })
                        .Build();

        await host.RunAsync();

    }
}