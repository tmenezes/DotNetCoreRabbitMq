using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreRabbitMq.Infrastructure.Container
{
    public interface IDIContainer
    {
        TService Resolve<TService>();
        void RegisterAsSingleton<TService, TImplementor>() where TService : class where TImplementor : class, TService;
        void RegisterAsTransient<TService, TImplementor>() where TService : class where TImplementor : class, TService;
        void RegisterAsScoped<TService, TImplementor>() where TService : class where TImplementor : class, TService;
    }
}
