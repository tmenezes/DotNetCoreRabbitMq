using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotnetCoreRabbitMq.Core;
using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer;

namespace DotnetCoreRabbitMq.ConsumersConsole
{
    public class Queue1Service : IMessageQueueService<Message>
    {
        public void ProcessMessage(Message message)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread {threadId} - {nameof(Queue1Service)} - Message -> {message.ToString()}");
        }
    }
}
