using System;
using System.Threading;
using DotnetCoreRabbitMq.Core;
using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer;

namespace DotnetCoreRabbitMq.ConsumersConsole
{
    public class Queue2Service : IMessageQueueService<Message>
    {
        public void ProcessMessage(Message message)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread {threadId} - {nameof(Queue2Service)} - Message -> {message.ToString()}");
        }
    }
}