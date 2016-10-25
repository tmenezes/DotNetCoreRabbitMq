using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Client;

namespace DotnetCoreRabbitMq.Core
{
    public static class QueueInitializer
    {
        public static void Initialize(IQueueClient queueClient)
        {
            queueClient.ExchangeDeclare(QueueConstants.ExchangeName);

            queueClient.QueueDeclare(QueueConstants.Queue1);
            queueClient.QueueBind(QueueConstants.Queue1, QueueConstants.ExchangeName, QueueConstants.NewMessageGenericRoute);

            queueClient.QueueDeclare(QueueConstants.Queue2);
            queueClient.QueueBind(QueueConstants.Queue2, QueueConstants.ExchangeName, QueueConstants.NewMessageGenericRoute);
            queueClient.QueueBind(QueueConstants.Queue2, QueueConstants.ExchangeName, QueueConstants.NewMessageSpecificRoute);

            queueClient.QueueDeclare(QueueConstants.Queue3);
            queueClient.QueueBind(QueueConstants.Queue3, QueueConstants.ExchangeName, QueueConstants.AllMessagesRoute);
        }
    }
}
