using System;

namespace twitchstreambot.infrastructure.DependencyInjection
{
    public class ContainerRegistration<T>
        where T : class
    {
        private readonly IContainer _container;

        public ContainerRegistration(IContainer container)
        {
            _container = container;
        }

        public IContainer Use(Func<IContainer, T> createWith)
        {
            _container.Register(typeof(T), createWith);

            return _container;
        }

        public IContainer Use<TResult>()
            where TResult : T
        {
            _container.Register(typeof(T), typeof(TResult));

            return _container;
        }
    }
}