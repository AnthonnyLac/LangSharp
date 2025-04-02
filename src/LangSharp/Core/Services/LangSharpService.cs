using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Handlers.Base; 
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Services
{
    public class LangSharpService : ILangSharpService
    {
        private readonly IHandler _handlerChain;
        private readonly IPythonService _pythonService;

        public LangSharpService(
             IValidatorHandler validatorHandler,
             IPythonInstallationHandler pythonInstallationHandler,
             IEnvironmentVariablesHandler environmentVariablesHandler,
             IInitializePythonHandler initializePythonHandler,
             IExecuteHandler executeHandler,            
             IPythonService pythonService)
        {
            pythonInstallationHandler
                .SetNext(validatorHandler)
                .SetNext(environmentVariablesHandler)
                .SetNext(initializePythonHandler)
                .SetNext(executeHandler);

            _handlerChain = pythonInstallationHandler;
            _pythonService = pythonService;
        }

        public Task<object> CallAIChatAsync(string prompt)
        {
            var result = _handlerChain.Handle(prompt);

            return Task.FromResult(result);
        }

        public Task<object> ExecuteDatabaseQueryAsync(string query)
        {
            throw new NotImplementedException();
        }
    }
}
