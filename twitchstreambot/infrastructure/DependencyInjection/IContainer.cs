using System;

namespace twitchstreambot.infrastructure.DependencyInjection
{
    public interface IContainer
    {
        TInstance GetInstance<TInstance>(params object[] args) where TInstance : class;
        object GetInstance(Type t);
        object GetInstance(Type t, object[] args);
        ContainerRegistration<T> When<T>() where T : class;
        void Register(Type @interface, Func<IContainer, object> @instance, bool singleton);
        void Register(Type @interface, Type @instance, bool singleton);
    }
}