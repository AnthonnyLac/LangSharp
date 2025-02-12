using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSharp.Out.Handlers
{
    /// <summary>
    /// Chama o LangChain via Python.NET.
    /// </summary>
    public class LangChainHandler : IHandler
    {
        private IHandler _nextHandler;

        public void SetNext(IHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public string Handle(string command)
        {
            // TODO: Lógica para chamar o LangChain via Python.NET
            bool langChainSuccess = true; 
            if (!langChainSuccess)
            {
                return "Erro: Falha ao executar LangChain.";
            }

            return _nextHandler?.Handle(command) ?? "LangChain executado com sucesso.";
        }
    }
}
