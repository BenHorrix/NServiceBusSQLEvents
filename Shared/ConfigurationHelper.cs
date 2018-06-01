namespace Shared
{
    using NServiceBus;
    using NServiceBus.Features;

    public static class ConfigurationHelper
    {
        public static EndpointConfiguration GetSqlConfiguration(string name)
        {
            var endpointConfiguration = new EndpointConfiguration(name);

            endpointConfiguration.EnableInstallers();
            endpointConfiguration.DisableFeature<TimeoutManager>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            transport.ConnectionString("Data Source=localhost;Initial Catalog=Messages;User ID=messages_user;Password=password123");
            transport.Transactions(TransportTransactionMode.None);

            return endpointConfiguration;
        }

        public static EndpointConfiguration GetLearningConfiguration(string name)
        {
            var endpointConfiguration = new EndpointConfiguration(name);

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            return endpointConfiguration;
        }
    }
}
