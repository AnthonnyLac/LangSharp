using LangSharp.Core.Abstractions;
using LangSharp.Core.Configuration;
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
            try
            {
  

                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error setting Python environment variables: {ex.Message}");
                throw;
            }
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

        public string ExecutePythonScript(AbstractScript scriptModel)
        {
            var pythonScriptPath = EnvironmentUtils.GetScriptsPath(scriptModel.Name);

            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append(Path.GetDirectoryName(pythonScriptPath));

                dynamic module = Py.Import(scriptModel.ModuleName);
                dynamic method = module.GetAttr(scriptModel.FunctionName);
                dynamic result = scriptModel.ProcessMethod(method);

                return result != null ? $"Call result: {result}" : "Execution completed with no return.";
            }
        }

        public bool IsPythonInstalled()
        {
            try
            {
                var pythonDllPath = EnvironmentUtils.GetPythonDllPath();
                if (string.IsNullOrEmpty(pythonDllPath) || !File.Exists(pythonDllPath))
                {
                    Console.Error.WriteLine("Python DLL not found.");
                    return false;
                }

                var pythonHome = EnvironmentUtils.GetPythonBasePath();
                if (string.IsNullOrEmpty(pythonHome) || !Directory.Exists(pythonHome))
                {
                    Console.Error.WriteLine("Python directory not found.");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error checking Python installation: {ex.Message}");
                return false;
            }
        }
        public void SetEnvironmentConfigs(LangSharpConfiguration configuration)
        {
            Environment.SetEnvironmentVariable("OPENAI_API_KEY", configuration.ApiKey);
            Environment.SetEnvironmentVariable("OPENAI_MODEL", configuration.Model);
            Environment.SetEnvironmentVariable("OPENAI_DATABASE_URI", configuration.DatabaseUri);
        }

        public void InstallPythonPackage(string packageName)
        {
            string path = EnvironmentUtils.GetPythonBasePath();

            if(IsPythonPackageInstalled(packageName))
                return;

            using (Py.GIL()) 
            {
                dynamic subprocess = Py.Import("subprocess");

                string pythonExecutable = Path.Combine(path,  "python.exe");

                subprocess.check_call(new[] { pythonExecutable, "-m", "pip", "install", packageName });
            }
        }

        public bool IsPythonPackageInstalled(string packageName)
        {
            // Define the path to the virtual environment
            string path = EnvironmentUtils.GetPythonBasePath();

            using (Py.GIL()) // GIL - Global Interpreter Lock
            {
                dynamic pkg_resources = Py.Import("pkg_resources");

                try
                {
                    pkg_resources.get_distribution(packageName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void InstallOpenAIDependencies()
        {
            InstallPythonPackage(PythonPackage.LangChainOpenai);
            InstallPythonPackage(PythonPackage.LangChainCommunity);
            InstallPythonPackage(PythonPackage.PythonDotEnv);
        }
    }
}
