using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LangSharp.Out.Handlers;

namespace LangSharp.Out.Core
{
    /// <summary>
    /// Implementação do Facade para simplificar a interação com o SDK.
    /// </summary>
    public class SDKFacade : ISDKService
    {
        private readonly IHandler _handlerChain;

        public SDKFacade()
        {
            var pythonCheck = new PythonInstallationHandler();
            var envCheck = new EnvironmentVariablesHandler();
            var setupCheck = new PythonSetupHandler();
            var executionHandler = new PythonExecutionHandler();
            var resultHandler = new ResultHandler();

            pythonCheck.SetNext(envCheck);
            envCheck.SetNext(setupCheck);
            setupCheck.SetNext(executionHandler);
            executionHandler.SetNext(resultHandler);

            _handlerChain = pythonCheck;
        }

        public string ExecutePythonCommand(string command)
        {
            try
            {
                return _handlerChain.Handle(command);
            }
            catch (Exception ex)
            {
                // Log ou tratamento de erro
                return $"Erro ao executar comando Python: {ex.Message}";
            }
        }
    }
}
