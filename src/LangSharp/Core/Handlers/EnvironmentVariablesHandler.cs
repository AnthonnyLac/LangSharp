using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Handlers
{
    public class EnvironmentVariablesHandler : AbstractHandler, IEnvironmentVariablesHandler
    {
        private readonly IPythonService _pythonService;

        public EnvironmentVariablesHandler(IPythonService pythonService)
        {
            _pythonService = pythonService;
        }

        public override object Handle(object request)
        {
            // Sets and validates environment variables
            if (!_pythonService.ArePythonNetVariablesSet())
                return "Environment variables are not set correctly.";

            return base.Handle(request);
        }
    }
}
