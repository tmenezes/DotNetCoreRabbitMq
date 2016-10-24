using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection;
using Microsoft.Extensions.Options;

namespace DotNetCoreRabbitMq.Queues
{
    public class QueueConnectionSettings : IQueueConnectionSettings
    {
        private readonly IOptions<QueueConnectionSettings> _optionsAccessor;

        public QueueConnectionSettings()
        {
        }
        public QueueConnectionSettings(IOptions<QueueConnectionSettings> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;

            HostName = _optionsAccessor.Value.HostName;
            VirtualHost = _optionsAccessor.Value.VirtualHost;
            UserName = _optionsAccessor.Value.UserName;
            Password = _optionsAccessor.Value.Password;
        }

        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
