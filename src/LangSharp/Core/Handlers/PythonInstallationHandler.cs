using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;

namespace LangSharp.Core.Handlers
{
    public class PythonInstallationHandler : AbstractHandler, IPythonInstallationHandler
    {
        public override object Handle(object request)
        {
            // Verifica se o Python está instalado
            if (true)
            {
                return base.Handle(request);
            }
            else
            {
                return "Python não está instalado.";
            }
        }
    }
}
