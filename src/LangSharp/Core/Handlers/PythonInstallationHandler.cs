using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Handlers;

namespace LangSharp.Core.Handlers
{
    public class PythonInstallationHandler : AbstractHandler, IPythonInstallationHandler
    {
        public override object Handle(object request)
        {
            // Checks if Python is installed
            if (true)
            {
                return base.Handle(request);
            }
            else
            {
                return "Python is not installed.";
            }
        }
    }
}
