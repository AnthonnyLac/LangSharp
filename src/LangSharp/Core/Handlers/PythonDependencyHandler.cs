using LangSharp.Core.Abstractions;
using LangSharp.Core.Configuration;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Handlers
{
    public class PythonDependencyHandler : AbstractHandler, IPythonDependencyHandler
    {
        private readonly IPythonService _pythonService;
        private readonly LangSharpConfiguration _configuration;

        public PythonDependencyHandler(IPythonService pythonService, LangSharpConfiguration configuration)
        {
            _pythonService = pythonService;
            _configuration = configuration;
        }

        public override object Handle(object request)
        {
            switch (_configuration.AIProvider)
            {
                case Enums.AIProviderType.OpenAI:
                    _pythonService.InstallOpenAIDependencies();
                    break;
                default:
                    return "Invalid AI provider type";
            }

            return base.Handle(request);
        }
    }
}
