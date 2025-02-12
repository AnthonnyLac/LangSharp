using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSharp.Out.Handlers
{
    /// <summary>
    /// Retorna o resultado final para o usuário.
    /// </summary>
    public class ResultHandler : IHandler
    {
        public void SetNext(IHandler nextHandler) { }

        public string Handle(string command)
        {
            return $"Resultado final do comando: {command}";
        }
    }
}
