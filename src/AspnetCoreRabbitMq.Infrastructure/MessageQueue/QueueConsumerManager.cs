using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetCoreRabbitMq.Infrastructure.Container;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue
{
    public class QueueConsumerFactory
    {
        private readonly IConnectionManager _connectionManager;
        private readonly IDIContainer _container;

        public QueueConsumerFactory(IConnectionManager connectionManager, IDIContainer container)
        {
            _connectionManager = connectionManager;
            _container = container;
        }

        public QueueConsumerManager<TMessage> CreateConsumer<TMessage>(MessageQueueService<TMessage> service, ConsumerProperties consumerProperties)
            where TMessage : class
        {
            return new QueueConsumerManager<TMessage>(_connectionManager, service, consumerProperties);
        }

        public QueueConsumerManager<TMessage> CreateConsumer<TService, TMessage>(ConsumerProperties consumerProperties)
            where TService : MessageQueueService<TMessage>
            where TMessage : class
        {
            var service = _container.Resolve<TService>();

            return CreateConsumer(service, consumerProperties);
        }


        private ConsumerWorker<TMessage> CreateConsumerWorker<TMessage>(MessageQueueService<TMessage> service, ConsumerProperties consumerProperties)
            where TMessage : class
        {
            return new ConsumerWorker<TMessage>(_connectionManager, service, consumerProperties);
        }
    }

    public class QueueConsumerManager<TMessage>
        where TMessage : class
    {
        private const int ONE_SECOND = 1000;

        private readonly IConnectionManager _connectionManager;
        private readonly IEnumerable<ConsumerWorker<TMessage>> _workers;
        private readonly Type _serviceClassType;
        private readonly ConsumerProperties _consumerProperties;

        internal QueueConsumerManager(IConnectionManager connectionManager, IEnumerable<ConsumerWorker<TMessage>> workers, ConsumerProperties consumerProperties)
        {
            _connectionManager = connectionManager;
            _workers = workers;
            _consumerProperties = consumerProperties;
        }


        public void Start()
        {
            // TODO: implement auto-scale thread

            foreach (var consumerWorker in _workers)
            {
                consumerWorker.Start();
            }
        }

        public void Stop()
        {
            foreach (var consumerWorker in _workers)
            {
                consumerWorker.Stop();
            }
        }


        //private IEnumerable<Task> BuildConsumersTasks()
        //{
        //    var count = _consumerProperties.ConsumersQuantity;

        //    // creates and starts 'N' consumers tasks delayed (consumer number * one second)
        //    var consumersTasks = Enumerable.Range(1, count)
        //                                   .Select(i => Task.Delay(i * ONE_SECOND)
        //                                                    .ContinueWith(t => ConsumerOperation()));
        //    return consumersTasks;

        //}
    }

    internal class ConsumerWorker<TMessage>
            where TMessage : class
    {
        private readonly IConnectionManager _connectionManager;
        private readonly MessageQueueService<TMessage> _service;
        private readonly ConsumerProperties _consumerProperties;
        private readonly Task _workerTask;
        private IModel _channel;

        public ConsumerWorker(IConnectionManager connectionManager, MessageQueueService<TMessage> service, ConsumerProperties consumerProperties)
        {
            _connectionManager = connectionManager;
            _service = service;
            _consumerProperties = consumerProperties;

            _workerTask = new Task(Consume);
        }

        private IModel CreateChannel()
        {
            return _connectionManager.GetConnection().CreateModel();
        }

        internal void Start()
        {
            _channel = CreateChannel();
            _channel.BasicQos(0, (ushort)_consumerProperties.PreFetchCount, false);

            _workerTask.Start();
        }

        internal void Stop()
        {
            // implement cancellation token
            _channel.Close();
        }

        private void Consume()
        {
            var subscription = new Subscription(_channel, _consumerProperties.QueueName, false);

            while (true)
            {
                var deliveryArguments = subscription.Next();

                //var message = (T)  deliveryArguments.Body.DeSerialize(typeof(PurchaseOrder));
                //var routingKey = deliveryArguments.RoutingKey;

                subscription.Ack(deliveryArguments);
            }
        }
    }

    public abstract class MessageQueueService<T> where T : class
    {
        public abstract void ProcessMessage(T message);
    }

    public class ConsumerProperties
    {
        public ConsumerProperties(string queueName, int consumersQuantity)
        {
            QueueName = queueName;
            ConsumersQuantity = consumersQuantity;
        }

        public string QueueName { get; private set; }
        public int ConsumersQuantity { get; private set; }
        public int PreFetchCount { get; set; }


        public static ConsumerProperties ForSingleConsumer(string queueName)
        {
            return new ConsumerProperties(queueName, 1);
        }

        public static ConsumerProperties ForSingleConsumer(string queueName, int consumersQuantity)
        {
            return new ConsumerProperties(queueName, consumersQuantity);
        }
    }
}