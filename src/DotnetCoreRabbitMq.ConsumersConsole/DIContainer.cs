using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetCoreRabbitMq.Infrastructure.Container;
using SimpleInjector;

namespace DotnetCoreRabbitMq.ConsumersConsole
{
    public class DIContainer : IDIContainer
    {
        private readonly Container _container;

        public DIContainer(Container container)
        {
            _container = container;
        }


        public TService Resolve<TService>()
        {
            return (TService)_container.GetInstance(typeof(TService));
        }

        public void RegisterAsScoped<TService, TImplementor>() where TService : class where TImplementor : class, TService
        {
            _container.Register<TService, TImplementor>(Lifestyle.Scoped);
        }

        public void RegisterAsSingleton<TService, TImplementor>() where TService : class where TImplementor : class, TService
        {
            _container.RegisterSingleton<TService, TImplementor>();
        }

        public void RegisterAsTransient<TService, TImplementor>() where TService : class where TImplementor : class, TService
        {
            _container.Register<TService, TImplementor>();
        }
    }
}
