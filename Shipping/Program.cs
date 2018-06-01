using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Shipping
{
    using Shared;

    class Program
    {
        static async Task Main()
        {
            Console.Title = "Shipping";

            var endpointConfiguration = ConfigurationHelper.GetSqlConfiguration("Shipping");

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}