using LangSharp.Core.Abstractions;
using LangSharp.Core.Interfaces.Infrastructure;
using LangSharp.Core.Interfaces.Services;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

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
        private readonly IPathService  _pathService;
        private readonly IFileSystemService _fileSystemService;

        public PythonService(IPythonRuntime pythonRuntime, IEnvironmentService env, IPathService pathService, IFileSystemService fileSystemService)
        {
            _pythonRuntime = pythonRuntime;
            _env = env;
            _pathService = pathService;
            _fileSystemService = fileSystemService;
        }

        public void InitializePythonEngine()
        {
            if (_pythonRuntime.IsInitialized)
                return;

            _pythonRuntime.Initialize();
        }

        public void ConfigureEnvironmentPaths()
        {
            var pythonHome = _pathService.GetPythonPath();

            if (string.IsNullOrEmpty(pythonHome) || !_fileSystemService.IsValidDirectory(pythonHome))
                throw new DirectoryNotFoundException($"Python directory not found: {pythonHome}");

            var sitePackagesPath = _pathService.GetSitePackagesPath(pythonHome);
            var pythonDllPath = _pathService.GetPythonDllPath();

            _env.ConfigurePythonEnvironment(pythonHome, sitePackagesPath, pythonDllPath);
        }


        public string ExecuteScript(AbstractScript scriptModel)
        {
            var pythonScriptPath = GetScriptPath(scriptModel.Name);
            var pythonPackgesPath = _pathService.GetSitePackagesPathFromPythonHome();

            using (_pythonRuntime.AcquireGIL())
            {
                dynamic sys = _pythonRuntime.Import("sys");
                sys.path.append(pythonPackgesPath);
                sys.path.append(_pathService.GetDirectoryName(pythonScriptPath));

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
                var pythonDllPath = _pathService.GetPythonDllPath();
                if (string.IsNullOrEmpty(pythonDllPath) || !_fileSystemService.IsFileExist(pythonDllPath))
                    throw new InvalidDataException("Python DLL not found.");


                var pythonHome = _pathService.GetPythonPath();
                if (string.IsNullOrEmpty(pythonHome) || !_fileSystemService.IsValidDirectory(pythonHome))
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

            string? pythonExecutable = _pathService.GetPythonPathExecutable();

            using (_pythonRuntime.AcquireGIL())
            {
                dynamic subprocess = _pythonRuntime.Import("subprocess");

                subprocess.check_call(new[] { pythonExecutable, "-m", "pip", "install", packageName });
            }
        }

        public bool IsPackageInstalled(string packageName)
        {
            string? pythonExecutable = _pathService.GetPythonPathExecutable();

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
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                CreateVirtualEnvLinux();
                return;
            }

            string venvPath = _pathService.GetVenvPath();
            string pythonExecutable = _pathService.GetPythonPathExecutable();


            using (_pythonRuntime.AcquireGIL())
            {

                dynamic subprocess = _pythonRuntime.Import("subprocess");
                subprocess.check_call(new[] { pythonExecutable, "-m", "venv", venvPath});
            }

        }

        private void CreateVirtualEnvLinux() 
        {
            string venvPath = _pathService.GetVenvPath();
            string pythonExecutable = _pathService.GetPythonPathExecutable();

            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = pythonExecutable,
                Arguments = $"-m venv \"{venvPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = System.Diagnostics.Process.Start(startInfo);

            if (process == null)
                throw new InvalidOperationException("Could not start the process to create the virtual environment.");

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
                throw new Exception($"Error creating virtual environment: {error}\n{output}");
        }

        public bool IsVirtualEnvCreated()
        {
            return _fileSystemService.IsValidDirectory(_pathService.GetVenvPath());
        }

        public void ActivateVirtualEnv()
        {
            var venvPath = _pathService.GetVenvPath(); 
            var sitePackagesPath = _pathService.GetSitePackagesPath(venvPath);

            _env.ConfigurePythonVirtualEnvironment(sitePackagesPath, true);
        }

        public string GetScriptPath(string scriptName)
        {
            var scriptPath = _pathService.GetScriptsPath(scriptName);

            if (_fileSystemService.IsFileExist(scriptPath))
                return scriptPath;

            scriptPath = _pathService.GetScriptsPathByPackageDir(scriptName);

            if (!_fileSystemService.IsFileExist(scriptPath))
                throw new FileNotFoundException($"Script '{scriptName}' not found in any of the verified paths.");

            return scriptPath;
        }
    }
}
