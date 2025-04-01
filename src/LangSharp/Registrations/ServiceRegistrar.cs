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
        public static void AddRequiredServices(IServiceCollection services)
        {
            // Registrando os handlers como scoped
            services.AddScoped<IPythonInstallationHandler, PythonInstallationHandler>();
            services.AddScoped<IEnvironmentVariablesHandler, EnvironmentVariablesHandler>();
            services.AddScoped<IInitializePythonHandler, InitializePythonHandler>();
            services.AddScoped<IPythonExecutionHandler, PythonExecutionHandler>();

            // Add singletons
            services.TryAddSingleton<ILangSharpService, LangSharpService>();
        }
    }
}
