using LangSharp.Core.Abstractions;
using LangSharp.Core.Configuration;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Providers;

namespace LangSharp.Core.Handlers
{
    public class PythonDependenciesInstallerHandler : AbstractHandler, IPythonDependenciesInstallerHandler
    {
        private readonly ICloudAIProvider _cloudAIProvider;
        private readonly LangSharpConfiguration _configuration;

        public PythonDependenciesInstallerHandler(ICloudAIProvider cloudAIProvider, LangSharpConfiguration configuration)
        {
            _cloudAIProvider = cloudAIProvider;
            _configuration = configuration;
        }

        public override object Handle(object request)
        {
            var result = _cloudAIProvider.InstallDependencies();

            if (result == false)
                return "Failed to install dependencies";

            return base.Handle(request);
        }
    }
}
