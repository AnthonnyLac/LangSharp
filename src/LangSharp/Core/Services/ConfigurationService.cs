using LangSharp.Core.Configuration;
using LangSharp.Core.Interfaces.Configurations;

namespace LangSharp.Core.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public void SetEnvironmentConfigs(LangSharpConfiguration configuration)
        {
            Environment.SetEnvironmentVariable("OPENAI_API_KEY", configuration.ApiKey, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("OPENAI_MODEL", configuration.Model, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("OPENAI_DATABASE_URI", configuration.DatabaseUri, EnvironmentVariableTarget.Process);
        }

    }
}
