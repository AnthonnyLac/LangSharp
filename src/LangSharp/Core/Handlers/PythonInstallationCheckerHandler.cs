using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Handlers
{
    /// <summary>
    /// Checks if Python is installed on the system.
    /// </summary>
    public class PythonInstallationCheckerHandler : AbstractHandler, IPythonInstallationCheckerHandler
    {
        private readonly IPythonService _pythonService;

        public PythonInstallationCheckerHandler(IPythonService pythonService)
        {
            _pythonService = pythonService;
        }

        public override object Handle(object request)
        {
            if (!_pythonService.IsPythonEnvironmentInstalled())
                return "Error: Python is not installed.";

            return base.Handle(request);
        }
    }
}
