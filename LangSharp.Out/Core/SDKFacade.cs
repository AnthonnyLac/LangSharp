using LangSharp.Out.Handlers;
using LangSharp.Out.Models.Base;
using LangSharp.Out.Services;

namespace LangSharp.Out.Core
{
    /// <summary>
    /// Implementação do Facade para simplificar a interação com o SDK.
    /// </summary>
    public class SDKFacade : ISDKService, IDisposable
    {
        private readonly IHandler _handlerChain;

        //public SDKFacade()
        //{
        //    var pythonCheck = new PythonInstallationHandler();
        //    var envCheck = new EnvironmentVariablesHandler();
        //    var setupCheck = new PythonSetupHandler();
        //    var executionHandler = new PythonExecutionHandler();
        //    var resultHandler = new ResultHandler();

        //    pythonCheck.SetNext(envCheck);
        //    envCheck.SetNext(setupCheck);
        //    setupCheck.SetNext(executionHandler);
        //    executionHandler.SetNext(resultHandler);

        //    _handlerChain = pythonCheck;
        //}

        public SDKFacade() 
        {
            var pythonPathConfiguration = new PythonPathConfigurationHandler();
            var initializePython = new InitializePythonHandler();

            pythonPathConfiguration.SetNext(initializePython);


            _handlerChain = pythonPathConfiguration;
        }

        public void Dispose()
        {
            PythonService.DisposePython();
        }

        public string ExecutePythonCommand(string command)
        {
            try
            {
                _handlerChain.Handle(command);

                string result = PythonService.ExecuteCommand(command);

                if (result.StartsWith("Erro"))
                {
                    return result;
                }

                return command;
            }
            catch (Exception ex)
            {
                // Log ou tratamento de erro
                return $"Erro ao executar comando Python: {ex.Message}";
            }
        }

        public string ExecutePythonScript(BaseScriptModel scriptModel)
        {
            try
            {
                _handlerChain.Handle(string.Empty);

                string result = PythonService.ExecutePythonScript(scriptModel);

                if (result.StartsWith("Erro"))
                {
                    return result;
                }

                return result;
            }
            catch (Exception ex)
            {
                // Log ou tratamento de erro
                return $"Erro ao executar Script Python: {ex.Message}";
            }
        }
    }
}
