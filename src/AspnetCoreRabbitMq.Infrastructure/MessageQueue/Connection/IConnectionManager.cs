using System;
using RabbitMQ.Client;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection
{
    public interface IConnectionManager : IDisposable
    {
        IConnection GetConnection();
    }
}