using LangSharp.Core.Interfaces.Services;
using LangSharp.Utils;
using Python.Runtime;

namespace LangSharp.Core.Services
{
    /// <summary>
    /// Service responsible for executing Python commands in the configured environment.
    /// </summary>
    public class PythonService : IPythonService
    {
        public void InitializePython()
        {
            if (PythonEngine.IsInitialized)
                return;

            PythonEngine.Initialize();
            var threadState = PythonEngine.BeginAllowThreads();

            PythonThread.SetThreadState(threadState);
        }

        public void SetEnvironmentPath()
        {
            var pythonHome = EnvironmentUtils.GetPythonBasePath();

            if (string.IsNullOrEmpty(pythonHome) || !Directory.Exists(pythonHome))
                throw new DirectoryNotFoundException($"Python directory not found: {pythonHome}");

            var sitePackagesPath = EnvironmentUtils.GetSitePackagesPath(pythonHome);
            var pythonDllPath = EnvironmentUtils.GetPythonDllPath();

            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDllPath);
            Environment.SetEnvironmentVariable("PYTHONHOME", pythonHome);
            Environment.SetEnvironmentVariable("PYTHONPATH", sitePackagesPath);
        }


        public bool ArePythonNetVariablesSet()
        {
            string? pythonHome = Environment.GetEnvironmentVariable("PYTHONHOME");
            string? pythonPath = Environment.GetEnvironmentVariable("PYTHONPATH");

            if (string.IsNullOrEmpty(pythonHome) || string.IsNullOrEmpty(pythonPath))
            {
                SetEnvironmentPath();
            }

            return true;
        }

        public string ExecuteCommand(string command)
        {
            try
            {
                using (Py.GIL())
                {
                    dynamic pyScope = Py.CreateScope();
                    pyScope.Exec(command);
                    return "Command executed successfully.";
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error executing Python command: {ex.Message}");
                return $"Error executing Python command: {ex.Message}";
            }
        }

        public void DisposePython()
        {
            var threadState = PythonThread.GetThreadState();

            if (threadState == default)
                return;

            PythonEngine.EndAllowThreads(threadState);
            PythonEngine.Shutdown();
        }

        public string CallPythonFunction(string moduleName, string functionName, params object[] args)
        {
            try
            {
                using (Py.GIL())
                {
                    dynamic pyModule = Py.Import(moduleName);
                    dynamic pyFunction = pyModule.GetAttr(functionName);
                    dynamic result = pyFunction.Invoke(args);
                    return result?.ToString() ?? "Execution completed with no return.";
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error calling Python function: {ex.Message}");
                return $"Error calling Python function: {ex.Message}";
            }
        }

        public string ExecutePythonScript(object scriptModel)
        {
            throw new NotImplementedException();
        }

        public bool IsPythonInstalled()
        {
            //To Do: add implementation to check if Python is installed
            return true;
        }
    }
}
