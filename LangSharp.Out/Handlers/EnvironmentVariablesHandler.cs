using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSharp.Out.Handlers
{
    /// <summary>
    /// Valida as variáveis de ambiente necessárias para o Python.NET.
    /// </summary>
    public class EnvironmentVariablesHandler : IHandler
    {
        private IHandler _nextHandler;

        public void SetNext(IHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public string Handle(string command)
        {
            // TODO: Lógica para verificar variáveis de ambiente
            bool envVarsSet = true;
            if (!envVarsSet)
            {
                return "Erro: Variáveis de ambiente do Python.NET não configuradas corretamente.";
            }

            return _nextHandler?.Handle(command) ?? "Variáveis de ambiente verificadas.";
        }
    }
}
