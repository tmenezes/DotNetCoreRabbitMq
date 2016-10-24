using System;
using RabbitMQ.Client;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue
{
    public interface IConnectionManager : IDisposable
    {
        IConnection GetConnection();
    }
}