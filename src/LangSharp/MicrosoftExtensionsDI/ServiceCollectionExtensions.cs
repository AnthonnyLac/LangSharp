using LangSharp.Core.Configuration;
using LangSharp.Registrations;
using Microsoft.Extensions.DependencyInjection;

namespace LangSharp.MicrosoftExtensionsDI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLangSharp(this IServiceCollection services, LangSharpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentException("No configuration found.");
            }

            ServiceRegistration.AddRequiredServices(services, configuration); 

            return services;
        }
    }
}
