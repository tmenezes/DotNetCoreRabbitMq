namespace DotNetCoreRabbitMq.Queues
{
    public static class QueueConstants
    {
        public static string ExchangeName = "AspnetCoreRabbitMq_Exchange";

        public static string Queue1 = "AspnetCoreRabbitMq_Queue1";
        public static string Queue2 = "AspnetCoreRabbitMq_Queue2";

        public static string NewMessageGenericRoute = "app.newMessageGeneric";
        public static string NewMessageSpecificRoute = "app.newMessageSpecific";

    }
}
