using LangSharp.Core.Configuration;
using LangSharp.Core.Factorys;
using LangSharp.Core.Handlers;
using LangSharp.Core.Interfaces.Configurations;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;
using LangSharp.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LangSharp.Registrations
{
    public class ServiceRegistration
    {
        public static void AddRequiredServices(IServiceCollection services, LangSharpConfiguration configuration)
        {
            // Add handlers
            services.TryAddScoped<IRequestValidatorHandler, RequestValidatorHandler>();
            services.TryAddScoped<IConfigurationSetupHandler, ConfigurationSetupHandler>();
            services.TryAddScoped<ISetEnvironmentVariablesHandler, SetEnvironmentVariablesHandler>();
            services.TryAddScoped<IVirtualEnvironmentHandler, VirtualEnvironmentHandler>();
            services.TryAddScoped<IPythonInstallationCheckerHandler, PythonInstallationCheckerHandler>();
            services.TryAddScoped<IPythonInitializerHandler, PythonInitializerHandler>();
            services.TryAddScoped<IPythonDependenciesInstallerHandler, PythonDependenciesInstallerHandler>();
            services.TryAddScoped<ICommandExecutionHandler, CommandExecutionHandler>();

            //Configs
            services.TryAddScoped<IConfigurationService, ConfigurationService>();

            //add services
            services.TryAddScoped<ILangSharpService, LangSharpService>();
            services.TryAddScoped<IPythonService, PythonService>();

            //Add SDK Config
            services.TryAddSingleton(configuration);

            // Build a temporary service provider to resolve IPythonService
            var serviceProvider = services.BuildServiceProvider();
            IPythonService pythonService = serviceProvider.GetRequiredService<IPythonService>();

            // Add AI Provider
            var aiProvider = CloudAIProviderFactory.CreateProvider(configuration.AIProvider, pythonService);
            services.TryAddSingleton(aiProvider);
        }
    }
}
