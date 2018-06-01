namespace Shared
{
    using System.Data.SqlClient;
    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Persistence.Sql;

    public static class ConfigurationHelper
    {
        public static EndpointConfiguration GetSqlConfiguration(string name)
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

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            transport.ConnectionString(connectionString);
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
