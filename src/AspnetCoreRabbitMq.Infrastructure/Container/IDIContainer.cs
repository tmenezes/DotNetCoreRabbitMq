using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreRabbitMq.Infrastructure.Container
{
    public interface IDIContainer
    {
        T Resolve<T>();
    }
}
