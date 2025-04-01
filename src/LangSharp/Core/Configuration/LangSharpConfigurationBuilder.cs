using LangSharp.Core.Enums;

namespace LangSharp.Core.Configuration
{
    public class LangSharpConfigurationBuilder
    {
        private readonly LangSharpConfiguration _configuration;

        public LangSharpConfigurationBuilder()
        {
            _configuration = new LangSharpConfiguration();
        }

        public LangSharpConfigurationBuilder SetAIProvider(AIProviderType provider)
        {
            _configuration.AIProvider = provider;
            return this;
        }

        public LangSharpConfigurationBuilder SetApiKey(string apiKey)
        {
            _configuration.ApiKey = apiKey;
            return this;
        }

        public LangSharpConfigurationBuilder SetPythonEnvironment(string environment)
        {
            _configuration.PythonEnvironment = environment;
            return this;
        }

        public LangSharpConfiguration Build()
        {
            return _configuration;
        }
    }
}
