using LangSharp.Out.Consts;
using LangSharp.Out.Services;
using Python.Runtime;

namespace LangSharp.Out.Handlers
{
    public class InitializePythonHandler : IHandler
    {
        private IHandler _nextHandler;

        public string Handle(string command)
        {
            PythonService.InitializePython();

            return _nextHandler?.Handle(command) ?? "Python path verificado.";
        }

        public void SetNext(IHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }
    }
}
