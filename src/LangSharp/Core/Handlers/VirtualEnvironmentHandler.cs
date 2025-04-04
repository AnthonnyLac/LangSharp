using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Handlers
{
    public class VirtualEnvironmentHandler : AbstractHandler, IVirtualEnvironmentHandler
    {
        private readonly IPythonService _pythonService;

        public VirtualEnvironmentHandler(IPythonService pythonService)
        {
            _pythonService = pythonService;
        }

        public override object Handle(object request)
        {
            if (!_pythonService.IsVirtualEnvCreated())
                _pythonService.CreateVirtualEnv();

            _pythonService.ActivateVirtualEnv();

            return base.Handle(request);
        }
    }
}

