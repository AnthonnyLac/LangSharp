using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Services;
using LangSharp.Core.Services;

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
            string? pythonHome = Environment.GetEnvironmentVariable("PYTHONHOME");
            string? pythonPath = Environment.GetEnvironmentVariable("PYTHONPATH");

            if (!string.IsNullOrEmpty(pythonHome) && !string.IsNullOrEmpty(pythonPath))
                return base.Handle(request);

            // Sets and validates environment variables
            _pythonService.SetEnvironmentPath();

            return base.Handle(request);
        }
    }
}
