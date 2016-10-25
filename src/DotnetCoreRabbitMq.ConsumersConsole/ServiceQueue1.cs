using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreRabbitMq.Core;
using DotNetCoreRabbitMq.Infrastructure;
using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer;

namespace DotnetCoreRabbitMq.ConsumersConsole
{
    public class ServiceQueue1 : IMessageQueueService<Message>
    {
        public void ProcessMessage(Message message)
        {
            Console.WriteLine($"{nameof(ServiceQueue1)} - Message -> {message.ToString()}");
        }
    }
}
