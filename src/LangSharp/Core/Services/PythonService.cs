using LangSharp.Core.Abstractions;
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
        public void InitializePythonEngine()
        {
            if (PythonEngine.IsInitialized)
                return;

            PythonEngine.Initialize();
            var threadState = PythonEngine.BeginAllowThreads();

            PythonThread.SetThreadState(threadState);
        }

        public void ConfigureEnvironmentPaths()
        {
            var pythonHome = EnvironmentUtils.GetPythonPath();

            if (string.IsNullOrEmpty(pythonHome) || !Directory.Exists(pythonHome))
                throw new DirectoryNotFoundException($"Python directory not found: {pythonHome}");

            var sitePackagesPath = EnvironmentUtils.GetSitePackagesPath(pythonHome);
            var pythonDllPath = EnvironmentUtils.GetPythonDllPath();

            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDllPath, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", pythonHome, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", sitePackagesPath, EnvironmentVariableTarget.Process);
        }


        public string ExecuteScript(AbstractScript scriptModel)
        {
            var pythonScriptPath = EnvironmentUtils.GetScriptsPath(scriptModel.Name);

            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append(Path.GetDirectoryName(pythonScriptPath));

                dynamic module = Py.Import(scriptModel.ModuleName);
                dynamic method = module.GetAttr(scriptModel.FunctionName);
                dynamic result = scriptModel.ProcessMethod(method);

                return result?["output"] != null ? $"Call result: {result["output"]}" : "Execution completed with no return.";
            }
        }

        public bool IsPythonEnvironmentInstalled()
        {
            try
            {
                var pythonDllPath = EnvironmentUtils.GetPythonDllPath();
                if (string.IsNullOrEmpty(pythonDllPath) || !File.Exists(pythonDllPath))
                {
                    Console.Error.WriteLine("Python DLL not found.");
                    return false;
                }

                var pythonHome = EnvironmentUtils.GetPythonPath();
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
  

        public void InstallPackage(string packageName)
        {
            if (IsPackageInstalled(packageName))
                return;

            string? pythonExecutable = EnvironmentUtils.GetPythonPathExecutable();

            using (Py.GIL())
            {
                dynamic subprocess = Py.Import("subprocess");


                subprocess.check_call(new[] { pythonExecutable, "-m", "pip", "install", packageName });
            }
        }

        public bool IsPackageInstalled(string packageName)
        {
            string? pythonExecutable = EnvironmentUtils.GetPythonPathExecutable();

            using (Py.GIL())
            {
                dynamic subprocess = Py.Import("subprocess");

                try
                {
                    subprocess.check_output(new[] { pythonExecutable, "-m", "pip", "show", packageName });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }



        public void CreateVirtualEnv()
        {
            string venvPath = EnvironmentUtils.GetVenvPath();

            using (Py.GIL())
            {
                dynamic subprocess = Py.Import("subprocess");
                subprocess.check_call(new[] { "python", "-m", "venv", venvPath });
            }

        }

        public bool IsVirtualEnvCreated()
        {
            return Directory.Exists(EnvironmentUtils.GetVenvPath());
        }

        public void ActivateVirtualEnv()
        {
            var venvPath = EnvironmentUtils.GetVenvPath();
            var sitePackagesPath = EnvironmentUtils.GetSitePackagesPath(venvPath);

            Environment.SetEnvironmentVariable("PYTHONHOME", venvPath, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", sitePackagesPath, EnvironmentVariableTarget.Process);
        }
    }
}
