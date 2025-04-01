using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Handlers.@base;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Services
{
    class LangSharpService : ILangSharpService
    {
        private readonly IHandler _handlerChain;

        public LangSharpService(
            IPythonInstallationHandler pythonInstallationHandler,
            IEnvironmentVariablesHandler environmentVariablesHandler,
            IInitializePythonHandler initializePythonHandler,
            IPythonExecutionHandler pythonExecutionHandler)
        {
            pythonInstallationHandler
                .SetNext(environmentVariablesHandler)
                .SetNext(initializePythonHandler)
                .SetNext(pythonExecutionHandler);

            _handlerChain = pythonInstallationHandler;
        }

        public Task<object> CallAIChatAsync(string prompt)
        {
            throw new NotImplementedException();
        }

        public Task<object> ExecuteDatabaseQueryAsync(string query)
        {
            throw new NotImplementedException();
        }
        public string? ExecuteCommand(string command)
        {
            return _handlerChain.Handle(command) as string;
        }
    }
}
