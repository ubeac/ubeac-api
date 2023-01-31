using Microsoft.Extensions.Configuration;
using uBeac.BackgroundTasks;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration config, string sectionName = "BackgroundTasks")
    {
        var options = config.GetSection(sectionName).Get<List<RecurringBackgroundTaskOption>>();

        var serviceDescriptors = new List<KeyValuePair<RecurringBackgroundTaskOption, Type>>();

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var option in options)
        {
            var serviceTypes = assemblies.Select(x => x).Select(x => x.GetType(option.Type)).Where(x => typeof(IRecurringBackgroundTask).IsAssignableFrom(x)).ToList();

            if (serviceTypes is null || serviceTypes.Count == 0)
                throw new Exception(option.Type + " (RecurringBackgroundService) is configured, but does not exist!");

            if (serviceTypes.Count > 1)
                throw new Exception(option.Type + " (RecurringBackgroundService) is configured, but the type name is not unique in all assemblies!");

            var serviceType = serviceTypes[0];

            if (!typeof(IRecurringBackgroundTask).IsAssignableFrom(serviceType))
                throw new Exception(option.Type + " (RecurringBackgroundService) is configured, but it is not inherited from RecurringBackgroundService class!");

            serviceDescriptors.Add(new KeyValuePair<RecurringBackgroundTaskOption, Type>(option, serviceType));

        }

        services.AddSingleton(serviceProvider =>
        {
            var registeredTasks = new List<IRecurringBackgroundTask>();

            foreach (var serviceDescriptor in serviceDescriptors)
            {
                var ctorParams = new List<object>();
                var serviceType = serviceDescriptor.Value;

                var ctors = serviceType.GetConstructors();
                var ctor = ctors[0];
                var ctorParamTypes = ctor.GetParameters();

                foreach (var param in ctorParamTypes)
                {
                    if (param.ParameterType == typeof(RecurringBackgroundTaskOption))
                    {
                        ctorParams.Add(serviceDescriptor.Key);
                        continue;
                    }

                    ctorParams.Add(serviceProvider.GetRequiredService(param.ParameterType));
                }

                var serviceInstance = Activator.CreateInstance(serviceType, ctorParams.ToArray());

                if (serviceInstance == null)
                    throw new Exception("Unable to create instance of (RecurringBackgroundService): " + serviceType.FullName);

                registeredTasks.Add((IRecurringBackgroundTask)serviceInstance);
            }

            return registeredTasks.AsEnumerable();
        });

        services.AddHostedService<BackgroundTaskManager>();

        return services;
    }
}
