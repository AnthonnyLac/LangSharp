using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LangSharp.Out.Services;

namespace LangSharp.Out.Handlers
{
    /// <summary>
    /// Executa comandos Python utilizando o Python.NET.
    /// </summary>
    public class PythonExecutionHandler : IHandler
    {
        private IHandler _nextHandler;

        public void SetNext(IHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public string Handle(string command)
        {
            string result = PythonService.ExecuteCommand(command);
            if (result.StartsWith("Erro"))
            {
                return result;
            }

            return _nextHandler?.Handle(command) ?? result;
        }
    }
}
