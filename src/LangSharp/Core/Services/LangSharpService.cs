using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Handlers.Base; 
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Services
{
    public class LangSharpService : ILangSharpService, IDisposable
    {
        private readonly IHandler _handlerChain;
        private readonly IPythonService _pythonService;

        public LangSharpService(
             IPythonInstallationHandler pythonInstallationHandler,
             IEnvironmentVariablesHandler environmentVariablesHandler,
             IInitializePythonHandler initializePythonHandler,
             IPythonService pythonService)
        {
            _pythonService = pythonService;

            pythonInstallationHandler
                .SetNext(environmentVariablesHandler)
                .SetNext(initializePythonHandler);

            _handlerChain = pythonInstallationHandler;
        }

        public async Task<object> CallAIChatAsync(string prompt)
        {
            _handlerChain.Handle(string.Empty);

            return  _pythonService.ExecuteCommand(prompt);
        }

        public Task<object> ExecuteDatabaseQueryAsync(string query)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _pythonService.DisposePython();
        }
    }
}
