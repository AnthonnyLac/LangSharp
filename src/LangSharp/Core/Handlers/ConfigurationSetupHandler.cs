using LangSharp.Core.Abstractions;
using LangSharp.Core.Configuration;
using LangSharp.Core.Interfaces.Configurations;
using LangSharp.Core.Interfaces.Handlers;

namespace LangSharp.Core.Handlers
{
    public class ConfigurationSetupHandler : AbstractHandler, IConfigurationSetupHandler
    {
        private readonly IConfigurationService _configurationService;
        private readonly LangSharpConfiguration _configuration;

        public ConfigurationSetupHandler(IConfigurationService configurationService, LangSharpConfiguration configuration)
        {
            _configurationService = configurationService;
            _configuration = configuration;
        }

        public override object Handle(object request)
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.Process);
            var model = Environment.GetEnvironmentVariable("OPENAI_MODEL", EnvironmentVariableTarget.Process);
            var databaseUri = Environment.GetEnvironmentVariable("OPENAI_DATABASE_URI", EnvironmentVariableTarget.Process);

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(databaseUri))
                return base.Handle(request);

            _configurationService.SetEnvironmentConfigs(_configuration);

            return base.Handle(request);
        }
    }
}
