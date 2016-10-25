namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer
{
    public class ConsumerProperties
    {
        public ConsumerProperties(string queueName, int consumersQuantity)
        {
            QueueName = queueName;
            ConsumersQuantity = consumersQuantity;
            PrefetchCount = 1;
        }

        public string QueueName { get; private set; }
        public int ConsumersQuantity { get; private set; }
        public int PrefetchCount { get; set; }
        //public AutoScale AutoScale { get; set; }
        //public ExceptionHandlingStrategy ExceptionHandlingStrategy { get; set; }


        public static ConsumerProperties ForSingleConsumer(string queueName)
        {
            return new ConsumerProperties(queueName, 1);
        }

        public static ConsumerProperties ForMultipleConsumers(string queueName, int consumersQuantity)
        {
            return new ConsumerProperties(queueName, consumersQuantity);
        }
    }
}