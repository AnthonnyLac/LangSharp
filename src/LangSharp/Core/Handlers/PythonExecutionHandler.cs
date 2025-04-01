using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;

namespace LangSharp.Core.Handlers
{
    public class PythonExecutionHandler : AbstractHandler, IPythonExecutionHandler
    {
        public override object Handle(object request)
        {
            // Executa o código Python e retorna o resultado
            var result = string.Empty ;
            return result ?? base.Handle(request);
        }
    }
}
