using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Handlers
{
    public class InitializePythonHandler : AbstractHandler, IInitializePythonHandler
    {
        private readonly IPythonService _pythonService;

        public InitializePythonHandler(IPythonService pythonService)
        {
            _pythonService = pythonService;
        }

        public override object Handle(object request)
        {
            // Initializes the Python runtime
            _pythonService.InitializePython();

            return base.Handle(request);
        }
    }
}
