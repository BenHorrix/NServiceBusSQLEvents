using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Sales
{
    using Shared;

    class Program
    {
        static async Task Main()
        {
            Console.Title = "Sales";

            var endpointConfiguration = ConfigurationHelper.GetLearningConfiguration("Sales");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}