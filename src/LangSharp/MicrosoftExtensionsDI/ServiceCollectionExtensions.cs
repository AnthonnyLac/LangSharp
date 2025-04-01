using Microsoft.Extensions.DependencyInjection;

namespace LangSharp.MicrosoftExtensionsDI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLangSharp(this IServiceCollection services, object configuration)
        {
            //Chama aclasse de config
            if (configuration == null)
            {
                throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");
            }

            return services;
        }
    }
}
