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

        public LangSharpConfigurationBuilder SetModel(string model)
        {
            _configuration.Model = model;
            return this;
        }

        public LangSharpConfigurationBuilder SetDatabaseUri(string? databaseUri)
        {
            _configuration.DatabaseUri = databaseUri;
            return this;
        }


        public LangSharpConfiguration Build()
        {
            return _configuration;
        }
    }
}
