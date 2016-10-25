namespace DotnetCoreRabbitMq.Core
{
    public static class QueueConstants
    {
        public static string ExchangeName = "AspnetCoreRabbitMq_Exchange";

        public static string Queue1 = "AspnetCoreRabbitMq_Queue1";
        public static string Queue2 = "AspnetCoreRabbitMq_Queue2";
        public static string Queue3 = "AspnetCoreRabbitMq_Queue3_ALL";

        public static string NewMessageGenericRoute = "app.newMessageGeneric";
        public static string NewMessageSpecificRoute = "app.newMessageSpecific";
        public static string AllMessagesRoute = "app.*";

    }
}
