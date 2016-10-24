using System;
using System.Collections.Generic;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue
{
    public interface IQueueClient : IDisposable
    {
        void Publish<T>(string exchangeName, string routingKey, T content);
        void BatchPublish<T>(string exchangeName, string routingKey, IEnumerable<T> contentList);
        void QueueDeclare(string queueName, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null);
        void QueueDeclarePassive(string queueName);
        void QueueBind(string queueName, string exchangeName, string routingKey);
        void ExchangeDeclare(string exchangeName, string type = "topic");
        bool QueueExists(string queueName);
        uint GetMessageCount(string queueName);
        uint GetConsumerCount(string queueName);
    }
}