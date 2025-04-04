using LangSharp.Core.Configuration;

namespace LangSharp.Core.Interfaces.Configurations
{
    public interface IConfigurationService
    {
        void SetEnvironmentConfigs(LangSharpConfiguration configuration);
    }
}
