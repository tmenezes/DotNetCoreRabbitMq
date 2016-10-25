using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection;

namespace DotnetCoreRabbitMq.ConsumersConsole
{
    public class QueueConnectionSettings : IQueueConnectionSettings
    {
        public string HostName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string VirtualHost { get; set; }

        public QueueConnectionSettings()
        {
            HostName = "b4riodsk-028";
            VirtualHost = "/";
            UserName = "remote";
            Password = "remote";
        }
    }
}