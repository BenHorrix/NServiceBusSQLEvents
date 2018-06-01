namespace Shared
{
    using System;
    using System.Data.SqlClient;
    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Persistence.Sql;

    public static class ConfigurationHelper
    {

        public static EndpointConfiguration GetSqlConfiguration(string name)
        {
            return GetSqlConfiguration(name, out var transport);
        }

        public static EndpointConfiguration GetSqlConfiguration(string name, out TransportExtensions<SqlServerTransport> transport)
        {
            const string connectionString = "Data Source=localhost;Initial Catalog=Messages;User ID=messages_user;Password=password123";
            var endpointConfiguration = new EndpointConfiguration(name);

            endpointConfiguration.EnableInstallers();
            endpointConfiguration.DisableFeature<TimeoutManager>();
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(connectionString);
                });
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(5));

            transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            transport.ConnectionString(connectionString);
            transport.Transactions(TransportTransactionMode.None);

            return endpointConfiguration;
        }

        public static EndpointConfiguration GetLearningConfiguration(string name)
        {
            return GetLearningConfiguration(name, out var transport);
        }

        public static EndpointConfiguration GetLearningConfiguration(string name, out TransportExtensions<LearningTransport> transport)
        {
            var endpointConfiguration = new EndpointConfiguration(name);

            transport = endpointConfiguration.UseTransport<LearningTransport>();

            return endpointConfiguration;
        }
    }
}
