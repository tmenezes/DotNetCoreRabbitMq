using RabbitMQ.Client;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection
{
    public class ConnectionManager : IConnectionManager
    {
        // fields
        private readonly IQueueConnectionSettings _connectionSettings;
        private IConnection _connection;

        // constructor
        public ConnectionManager(IQueueConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
        }

        public IConnection GetConnection()
        {
            if (_connection != null)
            {
                return _connection;
            }

            var factory = new ConnectionFactory
            {
                HostName = _connectionSettings.HostName,
                VirtualHost = _connectionSettings.VirtualHost,
                UserName = _connectionSettings.UserName,
                Password = _connectionSettings.Password
            };

            _connection = factory.CreateConnection();

            return _connection;
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection = null;
        }
    }
}