using LangSharp.Out.Services;
using LangSharp.Out.Utils;

namespace LangSharp.Out.Handlers
{
    public class PythonPathConfigurationHandler : IHandler
    {
        private IHandler _nextHandler;
        public void SetNext(IHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public string Handle(string command)
        {
            PythonService.SetEnvironmentPath();

            return _nextHandler?.Handle(command) ?? "Python path verificado.";
        }
    }
}
