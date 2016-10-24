using System;
using System.Collections.Generic;
using DotNetCoreRabbitMq.Infrastructure.Serializer;
using RabbitMQ.Client;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Client
{
    public class QueueClient : IQueueClient
    {
        // fields
        private readonly IConnectionManager _connectionManager;
        private readonly ISerializer _serializer;

        // constructor
        public QueueClient(IConnectionManager connectionManager, ISerializer serializer)
        {
            _connectionManager = connectionManager;
            _serializer = serializer;
        }


        // public queue client methods
        public void Publish<T>(string exchangeName, string routingKey, T content)
        {
            var contentSerialized = _serializer.Serialize(content);

            using (var model = GetModel())
            {
                var basicProperties = model.CreateBasicProperties();
                basicProperties.DeliveryMode = 2;

                model.BasicPublish(exchangeName, routingKey, basicProperties, contentSerialized);
            }
        }

        public void BatchPublish<T>(string exchangeName, string routingKey, IEnumerable<T> contentList)
        {
            using (var model = GetModel())
            {
                var basicProperties = model.CreateBasicProperties();
                basicProperties.DeliveryMode = 2;

                foreach (T entity in contentList)
                {
                    byte[] bytes = _serializer.Serialize(entity);
                    model.BasicPublish(exchangeName, routingKey, basicProperties, bytes);
                }
            }
        }

        public void QueueDeclare(string queueName, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            using (var model = GetModel())
            {
                model.QueueDeclare(queueName, durable, exclusive, autoDelete, arguments);
            }
        }

        public void QueueDeclarePassive(string queueName)
        {
            using (var model = GetModel())
            {
                model.QueueDeclarePassive(queueName);
            }
        }

        public void QueueBind(string queueName, string exchangeName, string routingKey)
        {
            using (var model = GetModel())
            {
                model.QueueBind(queueName, exchangeName, routingKey);
            }
        }

        public void ExchangeDeclare(string exchangeName, ExchangeType type = ExchangeType.Topic)
        {
            using (var model = GetModel())
            {
                model.ExchangeDeclare(exchangeName, GetExchangeTypeName(type), true);
            }
        }

        public bool QueueExists(string queueName)
        {
            using (var model = GetModel())
            {
                try
                {
                    model.QueueDeclarePassive(queueName);
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public uint GetMessageCount(string queueName)
        {
            using (var model = GetModel())
            {
                return model.QueueDeclarePassive(queueName).MessageCount;
            }
        }

        public uint GetConsumerCount(string queueName)
        {
            using (var model = GetModel())
            {
                return model.QueueDeclarePassive(queueName).ConsumerCount;
            }
        }


        // privates
        private IModel GetModel()
        {
            return _connectionManager.GetConnection().CreateModel();
        }

        private string GetExchangeTypeName(ExchangeType exchangeType) => exchangeType.ToString().ToLower();
    }
}
