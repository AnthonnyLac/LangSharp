using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Handlers
{
    /// <summary>
    /// Checks if Python is installed on the system.
    /// </summary>
    public class PythonInstallationHandler : AbstractHandler, IPythonInstallationHandler
    {
        private readonly IPythonService _pythonService;

        public PythonInstallationHandler(IPythonService pythonService)
        {
            _pythonService = pythonService;
        }

        public override object Handle(object request)
        {
            if (!_pythonService.IsPythonInstalled())
            {
                return "Error: Python is not installed.";
            }

            return base.Handle(request);
        }
    }
}
