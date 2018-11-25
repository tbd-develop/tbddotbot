using System;

namespace twitchbot.infrastructure.DependencyInjection
{
    public interface IContainer
    {
        TInstance GetInstance<TInstance>() where TInstance : class;
        object GetInstance(Type t);
        ContainerRegistration<T> When<T>() where T : class;
        void Register(Type @interface, Func<IContainer, object> @instance);
        void Register(Type @interface, Type @instance);
    }
}