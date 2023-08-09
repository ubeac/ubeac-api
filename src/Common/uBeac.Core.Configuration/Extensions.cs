using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.Configuration
{
    public static class Extensions
    {
        public static IConfigurationBuilder AddJsonConfig(this IConfigurationBuilder configBuilder, IHostEnvironment env, string configFolder = "Config")
        {
            var folderName = Path.Join(env.ContentRootPath, configFolder);

            // setting base path for config folder
            configBuilder.SetBasePath(folderName);

            foreach (var filename in Directory.GetFiles(folderName))
            {
                // read only json files
                if (Path.GetExtension(filename) != ".json")
                    continue;

                // accept only root config files
                if (Path.GetFileName(filename).Split(".").Length != 2)
                    continue;

                // adding config file
                configBuilder.AddJsonFile(filename, optional: true, reloadOnChange: true)
                  .AddJsonFile($"{Path.GetFileNameWithoutExtension(filename)}.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            }

            return configBuilder;
        }

        public static T GetInstance<T>(this IConfiguration configuration, string sectionName)
        {
            return configuration.GetSection(sectionName).Get<T>();
        }
    }
}