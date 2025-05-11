using Python.Runtime;

namespace LangSharp.Core.Interfaces.Infrastructure
{
    public interface IPythonRuntime
    {
        bool IsInitialized { get; }
        void Initialize();
        IDisposable AcquireGIL();
        PyObject Import(string moduleName);
    }
}
