namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer
{
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