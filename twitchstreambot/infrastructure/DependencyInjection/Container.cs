using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace twitchstreambot.infrastructure.DependencyInjection
{
    public class Container : IContainer
    {
        private readonly Dictionary<Type, Func<IContainer, object>> _dependencies;
        private readonly Dictionary<Type, Type> _typeDependencies;
        private readonly Dictionary<Type, object> _instances;

        public Container()
        {
            _dependencies = new Dictionary<Type, Func<IContainer, object>>();
            _typeDependencies = new Dictionary<Type, Type>();
            _instances = new Dictionary<Type, object>();

            _dependencies.Add(typeof(IContainer), ctx => ctx);
        }

        public ContainerRegistration<T> When<T>()
            where T : class
        {
            return new ContainerRegistration<T>(this);
        }

        public void Register(Type @interface, Func<IContainer, object> instance)
        {
            _dependencies.Add(@interface, @instance);
        }

        public void Register(Type @interface, Type @instance)
        {
            _typeDependencies.Add(@interface, @instance);
        }

        public object GetInstance(Type t)
        {
            var getInstance = (from m in GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                where m.Name == "GetInstance" && m.IsGenericMethod
                select m).Single();

            var genericCall = getInstance.MakeGenericMethod(t);

            return genericCall.Invoke(this, null);
        }

        public TInstance GetInstance<TInstance>() where TInstance : class
        {
            if (_instances.ContainsKey(typeof(TInstance)))
            {
                return (TInstance) _instances[typeof(TInstance)];
            }

            if (_dependencies.ContainsKey(typeof(TInstance)))
            {
                var result = (TInstance) _dependencies[typeof(TInstance)](this);

                _instances.Add(typeof(TInstance), result);

                return result;
            }

            if (_typeDependencies.ContainsKey(typeof(TInstance)))
            {
                TInstance result;
                var resultingType = _typeDependencies[typeof(TInstance)];

                var matchingConstructors =
                    from c in resultingType.GetConstructors()
                    where !c.GetParameters().Any() || c.GetParameters().All(p =>
                              _typeDependencies.ContainsKey(p.ParameterType) ||
                              _dependencies.ContainsKey(p.ParameterType))
                    select c;

                var constructor = matchingConstructors.First();

                if (!constructor.GetParameters().Any())
                {
                    result = (TInstance) constructor.Invoke(null);
                }
                else
                {
                    var parameters = from p in constructor.GetParameters()
                        select GetInstance(p.ParameterType);

                    result = (TInstance) constructor.Invoke(parameters.ToArray());
                }

                _instances.Add(typeof(TInstance), result);

                return result;
            }

            Type instanceType = typeof(TInstance);

            if (instanceType.IsClass &&
                !instanceType.IsAbstract &&
                instanceType.IsPublic)
            {
                _typeDependencies.Add(typeof(TInstance), typeof(TInstance));

                return GetInstance<TInstance>();
            }

            return null;
        }
    }
}