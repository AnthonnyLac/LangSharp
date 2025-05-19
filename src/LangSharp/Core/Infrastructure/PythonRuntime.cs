using LangSharp.Core.Interfaces.Infrastructure;
using Python.Runtime;

namespace LangSharp.Core.Infrastructure
{
    public class PythonRuntime : IPythonRuntime
    {
        public bool IsInitialized => PythonEngine.IsInitialized;

        public void Initialize()
        {
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();
        }

        public IDisposable AcquireGIL()
        {
            return Py.GIL();
        }

        public PyObject Import(string moduleName)
        {
            return Py.Import(moduleName);
        }

        public int ExecuteProcess(string fileName, string arguments, out string output, out string error)
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = System.Diagnostics.Process.Start(startInfo);

            if (process == null)
                throw new InvalidOperationException("Could not start the process.");

            output = process.StandardOutput.ReadToEnd();
            error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return process.ExitCode;
        }
    }
}
