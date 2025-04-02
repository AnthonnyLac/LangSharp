using LangSharp.Core.Configuration;
using LangSharp.Core.Factorys;
using LangSharp.Core.Handlers;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;
using LangSharp.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LangSharp.Registrations
{
    public class ServiceRegistrar
    {
        public static void AddRequiredServices(IServiceCollection services, LangSharpConfiguration configuration)
        {
            // Add handlers
            services.TryAddScoped<IValidatorHandler, RequestValidatorHandler>();
            services.TryAddScoped<IPythonInstallationHandler, PythonInstallationHandler>();
            services.TryAddScoped<IEnvironmentVariablesHandler, EnvironmentVariablesPythonHandler>();
            services.TryAddScoped<IInitializePythonHandler, InitializePythonHandler>();
            services.TryAddScoped<IExecuteHandler, ExecuteHandler>();
            services.TryAddScoped<IConfigurationHandler, ConfigurationHandler>();

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
