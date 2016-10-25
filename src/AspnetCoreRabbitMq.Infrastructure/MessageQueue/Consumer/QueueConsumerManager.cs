using System.Collections.Generic;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer
{
    public class QueueConsumerManager<TMessage>
        where TMessage : class
    {
        private readonly IEnumerable<ConsumerWorker<TMessage>> _workers;
        private readonly ConsumerProperties _consumerProperties;

        internal QueueConsumerManager(IEnumerable<ConsumerWorker<TMessage>> workers, ConsumerProperties consumerProperties)
        {
            _workers = workers;
            _consumerProperties = consumerProperties;
        }


        public void Start()
        {
            // TODO: implement auto-scale thread/task

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
    }
}