using LangSharp.Core.Configuration;
using LangSharp.Core.Factorys;
using LangSharp.Core.Handlers;
using LangSharp.Core.Infrastructure;
using LangSharp.Core.Interfaces.Configurations;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Infrastructure;
using LangSharp.Core.Interfaces.Providers;
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
            services.TryAddScoped<IEnvironmentService, EnvironmentService>();
            services.TryAddScoped<ILangSharpService, LangSharpService>();
            services.TryAddScoped<IFileSystemService, FileSystemService>();
            services.TryAddScoped<IFileSystemService, FileSystemService>();
            services.TryAddScoped<IPythonService, PythonService>();

            //Add Infra
            services.TryAddScoped<IPythonRuntime, PythonRuntime>();

            //Add SDK Config
            services.TryAddSingleton(configuration);

            //Add Path Service For Current Environment (windows/linux)
            services.TryAddSingleton(PathServiceFactory.CreateForCurrentEnvironment());

            // Build a temporary service provider to resolve IPythonService
            var serviceProvider = services.BuildServiceProvider();
            IPythonService pythonService = serviceProvider.GetRequiredService<IPythonService>();

            // Add AI Provider
            ICloudAIProvider aiProvider = CloudAIProviderFactory.CreateProvider(configuration.AIProvider, pythonService);
            services.TryAddSingleton(aiProvider);

        }
    }
}
