using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Handlers
{
    public class PythonInitializerHandler : AbstractHandler, IPythonInitializerHandler
    {
        private readonly IPythonService _pythonService;

        public PythonInitializerHandler(IPythonService pythonService)
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
