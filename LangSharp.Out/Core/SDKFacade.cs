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
            // Configurando a cadeia de responsabilidade
            var pythonCheck = new PythonInstallationHandler();
            var envCheck = new EnvironmentVariablesHandler();
            var setupCheck = new PythonSetupHandler();
            var langChainCheck = new LangChainHandler();

            pythonCheck.SetNext(envCheck);
            envCheck.SetNext(setupCheck);
            setupCheck.SetNext(langChainCheck);

            _handlerChain = pythonCheck;
        }

        public string ExecutePythonCommand(string command)
        {
            return _handlerChain.Handle(command);
        }
    }
}
