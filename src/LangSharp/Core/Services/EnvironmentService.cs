using LangSharp.Core.Interfaces.Services;
using LangSharp.Utils;

namespace LangSharp.Core.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        public string GetPythonPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".nuget", "packages", "python", EnvironmentConsts.PythonVersion, "tools");
        }

        public string GetPythonDllPath()
        {
            return Path.Combine(GetPythonPath(), EnvironmentConsts.DllVersionName);
        }

        public string? GetPythonPathExecutable()
        {
            var pythonHome = Environment.GetEnvironmentVariable("PYTHONHOME", EnvironmentVariableTarget.Process);

            if (string.IsNullOrEmpty(pythonHome))
                return null;

            var isVirtualEnv = pythonHome.EndsWith(EnvironmentConsts.VirtualEnvironment, StringComparison.OrdinalIgnoreCase);
            return isVirtualEnv
                ? Path.Combine(pythonHome, "Scripts", "python.exe")
                : Path.Combine(pythonHome, "python.exe");
        }

        public string GetScriptsPath(string scriptName)
        {
            return Path.Combine(AppContext.BaseDirectory, "scripts", scriptName);
        }

        public string GetScriptsPathByPackageDir(string scriptName)
        {
            return Path.Combine(
              Path.Combine(
                  Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                  ".nuget", "packages", "langsharp", EnvironmentConsts.GetLangSharpAssemblyVersion()
              ),
              "Scripts", scriptName);
        }

        public string GetSitePackagesPath(string basePath)
        {
            return Path.Combine(basePath, "Lib", "site-packages");
        }

        public string GetVenvPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".nuget", "packages", "python", EnvironmentConsts.PythonVersion,
                EnvironmentConsts.VirtualEnvironment);
        }

        public void ConfigurePythonEnvironment(string pythonHome, string sitePackagesPath, string pythonDllPath)
        {
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDllPath, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", pythonHome, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", sitePackagesPath, EnvironmentVariableTarget.Process);
        }

        public void ConfigurePythonEnvironment(string pythonHome, string sitePackagesPath)
        {
            Environment.SetEnvironmentVariable("PYTHONHOME", pythonHome, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", sitePackagesPath, EnvironmentVariableTarget.Process);
        }

        public bool IsValidDirectory(string? path)
        {
            return !string.IsNullOrWhiteSpace(path) && Directory.Exists(path);
        }

        public bool IsFileExist(string? path)
        {
            return !string.IsNullOrWhiteSpace(path) && File.Exists(path);
        }
    }
}
