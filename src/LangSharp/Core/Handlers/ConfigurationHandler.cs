using LangSharp.Core.Abstractions;
using LangSharp.Core.Configuration;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Providers;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Handlers
{
    public class ConfigurationHandler : AbstractHandler, IConfigurationHandler
    {
        private readonly IPythonService _pythonService;
        private readonly LangSharpConfiguration _configuration;

        public ConfigurationHandler(IPythonService pythonService, LangSharpConfiguration configuration)
        {
            _pythonService = pythonService;
            _configuration = configuration;
        }

        public override object Handle(object request)
        {
            var apiKey = Environment.GetEnvironmentVariable("PYTHONNET_CONFIG_API_KEY");

            if (!string.IsNullOrEmpty(apiKey))
                return base.Handle(request);

            _pythonService.SetEnvironmentConfigs(_configuration);

            return base.Handle(request);
        }
    }
}
