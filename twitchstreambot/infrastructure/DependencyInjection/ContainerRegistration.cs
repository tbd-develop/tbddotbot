using System;

namespace twitchstreambot.Infrastructure.DependencyInjection
{
    public class ContainerRegistration<T>
        where T : class
    {
        private readonly IContainer _container;
        private bool _asSingleton;

        public ContainerRegistration(IContainer container)
        {
            _container = container;
        }

        public IContainer Use(Func<IContainer, T> createWith)
        {
            _container.Register(typeof(T), createWith, _asSingleton);

            return _container;
        }

        public IContainer Use<TResult>()
            where TResult : T
        {
            _container.Register(typeof(T), typeof(TResult), _asSingleton);

            return _container;
        }

        public ContainerRegistration<T> AsSingleton()
        {
            _asSingleton = true;

            return this;
        }
    }
}