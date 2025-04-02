using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Handlers.Base; 
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Services
{
    public class LangSharpService : ILangSharpService
    {
        private readonly IHandler _handlerChain;

        public LangSharpService(
             IValidatorHandler validatorHandler,
             IEnvironmentVariablesHandler environmentVariablesHandler,
             IPythonInstallationHandler pythonInstallationHandler,
             IInitializePythonHandler initializePythonHandler,
             IExecuteHandler executeHandler,
             IConfigurationHandler configurationHandler)
        {
            pythonInstallationHandler
                .SetNext(environmentVariablesHandler)
                .SetNext(configurationHandler)
                .SetNext(validatorHandler)
                .SetNext(initializePythonHandler)
                .SetNext(executeHandler);

            _handlerChain = pythonInstallationHandler;
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
