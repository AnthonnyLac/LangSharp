using LangSharp.Core.Abstractions;
using LangSharp.Core.Configuration;
using LangSharp.Core.Interfaces.Handlers;
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
            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            var model = Environment.GetEnvironmentVariable("OPENAI_MODEL");
            var databaseUri = Environment.GetEnvironmentVariable("OPENAI_DATABASE_URI");

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(databaseUri))
                return base.Handle(request);

            _pythonService.SetEnvironmentConfigs(_configuration);

            return base.Handle(request);
        }
    }
}
