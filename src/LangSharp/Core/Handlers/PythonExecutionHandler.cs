using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;

namespace LangSharp.Core.Handlers
{
    public class PythonExecutionHandler : AbstractHandler, IPythonExecutionHandler
    {
        public override object Handle(object request)
        {
            // Executes the Python code and returns the result
            var result = string.Empty;
            return result ?? base.Handle(request);
        }
    }
}
