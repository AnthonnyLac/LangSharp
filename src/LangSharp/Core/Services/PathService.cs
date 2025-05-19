using LangSharp.Core.Interfaces.Services;
using LangSharp.Utils;
using NuGet.Configuration;

namespace LangSharp.Core.Services
{
    public class PathService : IPathService
    {
        public string GetNuggetPath()
        {
            ISettings settings = Settings.LoadDefaultSettings(null);
            var nugetPath = SettingsUtility.GetGlobalPackagesFolder(settings);

            return nugetPath;
        }
        public string GetPythonDllPath()
        {
            return Path.Combine(GetPythonPath(), EnvironmentConsts.DllVersionName);
        }

        public string GetPythonPath()
        {
            string nugetPath = GetNuggetPath();

            var pythonPath = Path.Combine(nugetPath, "python", EnvironmentConsts.PythonVersion, "tools");

            return pythonPath;
        }

        public string GetPythonVenvPath()
        {
            string nugetPath = GetNuggetPath();

            var pythonPath = Path.Combine(nugetPath, "python", EnvironmentConsts.PythonVersion, EnvironmentConsts.VirtualEnvironment);

            return pythonPath;
        }

        public string GetPythonPathExecutable()
        {
            var isVenv = Environment.GetEnvironmentVariable("LANGSHARP_IS_VENV", EnvironmentVariableTarget.Process);

            return !string.IsNullOrEmpty(isVenv) && bool.Parse(isVenv)
                ? GetPythonPathExecutableForVenv(GetPythonVenvPath())
                : GetPythonPathExecutableForStandardInstallation(GetPythonPath());
        }

        public string GetScriptsPath(string scriptName)
        {
            return Path.Combine(AppContext.BaseDirectory, "Scripts", scriptName);
        }

        public string GetScriptsPathByPackageDir(string scriptName)
        {
            var nuggetPath = GetNuggetPath();

            return Path.Combine(
              Path.Combine(
                  nuggetPath, "langsharp", EnvironmentConsts.GetLangSharpAssemblyVersion()
              ),
              "Scripts", scriptName);
        }

        public string GetSitePackagesPath(string basePath)
        {
            return Path.Combine(basePath, "Lib", "site-packages");
        }

        public string GetSitePackagesPath()
        {
            var pythonHome = Environment.GetEnvironmentVariable("PYTHONHOME", EnvironmentVariableTarget.Process);

            if (string.IsNullOrEmpty(pythonHome))
                return string.Empty;

            var isVirtualEnv = Environment.GetEnvironmentVariable("LANGSHARP_IS_VENV", EnvironmentVariableTarget.Process);

            return  !string.IsNullOrEmpty(isVirtualEnv) && bool.Parse(isVirtualEnv)
                ? Path.Combine(GetPythonVenvPath(), "Lib", "site-packages")
                : Path.Combine(pythonHome, "Lib");
        }

        public string GetVenvPath()
        {
            var nuggetPath = GetNuggetPath();

            return Path.Combine(
                nuggetPath, "python", EnvironmentConsts.PythonVersion,
                EnvironmentConsts.VirtualEnvironment);
        }

        public string GetDirectoryName(string? path)
        {
            return Path.GetDirectoryName(path) ?? string.Empty;
        }

        private static string GetPythonPathExecutableForVenv(string pythonHome)
        {
            return Path.Combine(pythonHome, "Scripts", "python.exe");
        }

        private static string GetPythonPathExecutableForStandardInstallation(string pythonHome)
        {
            return Path.Combine(pythonHome, "python.exe");
        }

    }
}
