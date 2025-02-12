using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSharp.Out.Handlers
{
    /// <summary>
    /// Verifica se o Python está instalado no sistema.
    /// </summary>
    public class PythonInstallationHandler : IHandler
    {
        private IHandler _nextHandler;

        public void SetNext(IHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public string Handle(string command)
        {
            // TODO: Lógica para verificar a instalação do Python
            bool pythonInstalled = true; // Simulação
            if (!pythonInstalled)
            {
                return "Erro: Python não está instalado.";
            }

            return _nextHandler?.Handle(command) ?? "Python verificado.";
        }
    }

}
