using LangSharp.Core.Interfaces.Services;
using LangSharp.Utils;
using NuGet.Configuration;

namespace LangSharp.Core.Services
{
    public class PathLinuxService : IPathService
    {
        public string GetNuggetPath()
        {
            var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER", EnvironmentVariableTarget.Process);

            if (!string.IsNullOrEmpty(isDocker) && bool.Parse(isDocker))
            {
                return Path.Combine(Path.DirectorySeparatorChar.ToString(), Environment.CurrentDirectory, "root", ".nuget", "packages");
            }

            ISettings settings = Settings.LoadDefaultSettings(null);
            var nugetPath = SettingsUtility.GetGlobalPackagesFolder(settings);

            return nugetPath;
        }
        public string GetPythonDllPath()
        {
            var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER", EnvironmentVariableTarget.Process);

            if (!string.IsNullOrEmpty(isDocker) && bool.Parse(isDocker))
            {
                return Path.Combine(Path.DirectorySeparatorChar.ToString(), "usr", "lib", "x86_64-linux-gnu", "libpython3.11.so.1.0");
            }

            return Path.Combine(GetPythonPath(), EnvironmentConsts.DllVersionName);
        }

        public string GetPythonPath()
        {
            return Path.Combine(Path.DirectorySeparatorChar.ToString(), "usr");
        }

        public string GetPythonVenvPath()
        {
            return Path.Combine(Path.DirectorySeparatorChar.ToString(), "usr");
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
            return Path.Combine(basePath, "lib", "python3.11", "site-packages");
        }

        public string GetSitePackagesPathFromPythonHome()
        {
            var pythonHome = Environment.GetEnvironmentVariable("PYTHONHOME", EnvironmentVariableTarget.Process);

            if (string.IsNullOrEmpty(pythonHome))
                return string.Empty;

            return GetSitePackagesPath(pythonHome);
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
            return Path.Combine(pythonHome, "bin", "python3.11");
        }

        private static string GetPythonPathExecutableForStandardInstallation(string pythonHome)
        {
            return Path.Combine(pythonHome, "bin", "python3.11");
        }

    }
}
