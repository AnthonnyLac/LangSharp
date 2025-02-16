using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LangSharp.Out.Services;

namespace LangSharp.Out.Handlers
{
    /// <summary>
    /// Configura o ambiente Python (imports, caminhos, etc.).
    /// </summary>
    public class PythonSetupHandler : IHandler
    {
        private IHandler _nextHandler;

        public void SetNext(IHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public string Handle(string command)
        {
            try
            {
                PythonService.InitializePython();
                return _nextHandler?.Handle(command) ?? "Ambiente Python configurado.";
            }
            catch (Exception ex)
            {
                return $"Erro ao configurar ambiente Python: {ex.Message}";
            }
        }
    }
}
