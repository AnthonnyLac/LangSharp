using LangSharp.Core.Commands;
using LangSharp.Core.Enums;
using LangSharp.Core.Interfaces.Handlers;
using LangSharp.Core.Interfaces.Handlers.Base;
using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Services
{
    public class LangSharpService : ILangSharpService
    {
        private readonly IHandler _handlerChain;

        public LangSharpService(
             IRequestValidatorHandler requestValidatorHandler,
             IConfigurationSetupHandler configurationSetupHandler,
             ISetEnvironmentVariablesHandler setEnvironmentVariablesHandler,
             IVirtualEnvironmentHandler virtualEnvironmentHandler,
             IPythonInstallationCheckerHandler pythonInstallationCheckerHandler,
             IPythonInitializerHandler pythonInitializerHandler,
             IPythonDependenciesInstallerHandler pythonDependenciesInstallerHandler,
             ICommandExecutionHandler commandExecutionHandler)
        {
            requestValidatorHandler
                .SetNext(configurationSetupHandler)
                .SetNext(setEnvironmentVariablesHandler)
                .SetNext(pythonInstallationCheckerHandler)
                .SetNext(pythonInitializerHandler)
                .SetNext(virtualEnvironmentHandler)
                .SetNext(pythonDependenciesInstallerHandler)
                .SetNext(commandExecutionHandler);

            _handlerChain = requestValidatorHandler;
        }

        public Task<object> CallAIChatAsync(string prompt)
        {
            var result = _handlerChain.Handle(new CommandRequest(TypeCommand.GetResponse, prompt));

            return Task.FromResult(result);
        }

        public Task<object> ExecuteDatabaseQueryAsync(string query)
        {
            var result = _handlerChain.Handle(new CommandRequest(TypeCommand.ExecuteDatabaseQuery, query));

            return Task.FromResult(result);
        }
    }
}
