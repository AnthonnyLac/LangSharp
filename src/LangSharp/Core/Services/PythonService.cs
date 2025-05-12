using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Infrastructure;
using LangSharp.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace LangSharp.Core.Services
{
    /// <summary>
    /// Service responsible for executing Python commands in the configured environment.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PythonService : IPythonService
    {
        private readonly IPythonRuntime _pythonRuntime;
        private readonly IEnvironmentService _env;

        public PythonService(IPythonRuntime pythonRuntime, IEnvironmentService environmentService)
        {
            _pythonRuntime = pythonRuntime;
            _env = environmentService;
        }

        public void InitializePythonEngine()
        {
            if (_pythonRuntime.IsInitialized)
                return;

            _pythonRuntime.Initialize();
        }

        public void ConfigureEnvironmentPaths()
        {
            var pythonHome = _env.GetPythonPath();

            if (string.IsNullOrEmpty(pythonHome) || !_env.IsValidDirectory(pythonHome))
                throw new DirectoryNotFoundException($"Python directory not found: {pythonHome}");

            var sitePackagesPath = _env.GetSitePackagesPath(pythonHome);
            var pythonDllPath = _env.GetPythonDllPath();

            _env.ConfigurePythonEnvironment(pythonHome, sitePackagesPath, pythonDllPath);
        }


        public string ExecuteScript(AbstractScript scriptModel)
        {
            var pythonScriptPath = GetScriptPath(scriptModel.Name);
            var pythonPackgesPath = _env.GetSitePackagesPathFromPythonHome();

            using (_pythonRuntime.AcquireGIL())
            {
                dynamic sys = _pythonRuntime.Import("sys");
                sys.path.append(pythonPackgesPath);
                sys.path.append(_env.GetDirectoryName(pythonScriptPath));

                dynamic module = _pythonRuntime.Import(scriptModel.ModuleName);
                dynamic method = module.GetAttr(scriptModel.FunctionName);
                dynamic result = scriptModel.ProcessMethod(method);

                return result != null ? $"Call result: {result}" : "Execution completed with no return.";
            }
        }

        public bool IsPythonEnvironmentInstalled()
        {
            try
            {
                var pythonDllPath = _env.GetPythonDllPath();
                if (string.IsNullOrEmpty(pythonDllPath) || !_env.IsFileExist(pythonDllPath))
                    throw new InvalidDataException("Python DLL not found.");


                var pythonHome = _env.GetPythonPath();
                if (string.IsNullOrEmpty(pythonHome) || !_env.IsValidDirectory(pythonHome))
                    throw new InvalidDataException("Python directory not found.");


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

            string? pythonExecutable = _env.GetPythonPathExecutable();

            using (_pythonRuntime.AcquireGIL())
            {
                dynamic subprocess = _pythonRuntime.Import("subprocess");


                subprocess.check_call(new[] { pythonExecutable, "-m", "pip", "install", packageName });
            }
        }

        public bool IsPackageInstalled(string packageName)
        {
            string? pythonExecutable = _env.GetPythonPathExecutable();

            using (_pythonRuntime.AcquireGIL())
            {
                dynamic subprocess = _pythonRuntime.Import("subprocess");

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
            string venvPath = _env.GetVenvPath();

            using (_pythonRuntime.AcquireGIL())
            {
                dynamic subprocess = _pythonRuntime.Import("subprocess");
                subprocess.check_call(new[] { "python", "-m", "venv", venvPath });
            }
        }

        public bool IsVirtualEnvCreated()
        {
            return _env.IsValidDirectory(_env.GetVenvPath());
        }

        public void ActivateVirtualEnv()
        {
            var venvPath = _env.GetVenvPath();
            var sitePackagesPath = _env.GetSitePackagesPath(venvPath);

            _env.ConfigurePythonEnvironment(venvPath, sitePackagesPath);
        }

        public string GetScriptPath(string scriptName)
        {
            var scriptPath = _env.GetScriptsPath(scriptName);

            if (_env.IsFileExist(scriptPath))
                return scriptPath;

            scriptPath = _env.GetScriptsPathByPackageDir(scriptName);

            if (!_env.IsFileExist(scriptPath))
                throw new FileNotFoundException($"Script '{scriptName}' not found in any of the verified paths.");

            return scriptPath;
        }
    }
}
