using LangSharp.Out.Services;

namespace LangSharp.Out.Handlers
{
    public class PythonDisposeHandler : IHandler
    {
        private IHandler _nextHandler;

        public string Handle(string command)
        {
            PythonService.DisposePython();

            return _nextHandler?.Handle(command) ?? "Dispose";
        }

        public void SetNext(IHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }
    }
}
