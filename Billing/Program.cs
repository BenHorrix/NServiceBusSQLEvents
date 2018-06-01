using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Billing
{
    using Shared;

    class Program
    {
        static async Task Main()
        {
            Console.Title = "Billing";

            var endpointConfiguration = ConfigurationHelper.GetSqlConfiguration("Billing");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}