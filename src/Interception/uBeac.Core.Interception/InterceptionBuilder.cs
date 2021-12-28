using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace uBeac.Interception
{
    public class InterceptionBuilder<TInterface>
    {
        private readonly List<Type> _interceptorTypes;
        private readonly IServiceCollection _services;
        private readonly Type _targetType;

        public InterceptionBuilder(IServiceCollection services)
        {
            _services = services;
            _targetType = typeof(TInterface);
            _interceptorTypes = new List<Type>();
        }
        public InterceptionBuilder<TInterface> With<T>() where T : IInterceptor
        {
            _interceptorTypes.Add(typeof(T));
            return this;
        }

        public IServiceCollection Build()
        {
            var targetDescriptor = _services.FirstOrDefault(s => s.ServiceType == _targetType);
            if (targetDescriptor == null)
                throw new InvalidOperationException($"{_targetType.Name} is not registered");

            var proxiedDescriptor = ServiceDescriptor.Describe(_targetType,
              provider =>
              {
                  var targetInstance = CreateInstance(provider, targetDescriptor);
                  return Proxy<TInterface>.Decorate(targetInstance, GetInterceptors(provider));
              },
              targetDescriptor.Lifetime);

            return _services.Replace(proxiedDescriptor);

        }

        private List<IInterceptor> GetInterceptors(IServiceProvider provider)
        {
            var interceptors = new List<IInterceptor>();
            foreach (var interceptorType in _interceptorTypes)
            {
                var interceptor = (IInterceptor)ActivatorUtilities.CreateInstance(provider, interceptorType);
                interceptors.Add(interceptor);
            }
            return interceptors;
        }

        private static TInterface CreateInstance(IServiceProvider provider, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
                return (TInterface)descriptor.ImplementationInstance;

            if (descriptor.ImplementationFactory != null)
                return (TInterface)descriptor.ImplementationFactory(provider);

            return (TInterface)ActivatorUtilities.GetServiceOrCreateInstance(provider, descriptor.ImplementationType);
        }

    }
}
