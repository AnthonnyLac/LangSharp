using LangSharp.Core.Interfaces.Services;
using LangSharp.Utils;
using NuGet.Configuration;
using System.Runtime.InteropServices;

namespace LangSharp.Core.Services
{
    public class PathService : IPathService
    {
        public string GetNuggetPath() 
        {
            var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER", EnvironmentVariableTarget.Process);

            if (!string.IsNullOrEmpty(isDocker) && bool.Parse(isDocker) && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Path.Combine(Environment.CurrentDirectory, "root", ".nuget", "packages");
            }

            ISettings settings = Settings.LoadDefaultSettings(null);
            var nugetPath = SettingsUtility.GetGlobalPackagesFolder(settings);

            return nugetPath;
        }
        public string GetPythonDllPath()
        {
            var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER", EnvironmentVariableTarget.Process);

            if (!string.IsNullOrEmpty(isDocker) && bool.Parse(isDocker) && RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Path.Combine(Path.DirectorySeparatorChar.ToString(), "usr", "lib", "x86_64-linux-gnu", "libpython3.11.so.1.0");
            }

            return Path.Combine(GetPythonPath(), EnvironmentConsts.DllVersionName);
        }

        public string GetPythonPath()
        {
            string nugetPath = GetNuggetPath();

            var pythonPath = Path.Combine(nugetPath, "python", EnvironmentConsts.PythonVersion, "tools");

            return pythonPath;
        }

        public string GetPythonPathExecutable()
        {
            var pythonHome = Environment.GetEnvironmentVariable("PYTHONHOME", EnvironmentVariableTarget.Process);

            if (string.IsNullOrEmpty(pythonHome))
                return string.Empty;

            var isVirtualEnv = pythonHome.EndsWith(EnvironmentConsts.VirtualEnvironment, StringComparison.OrdinalIgnoreCase);

            return isVirtualEnv
                ? GetPythonPathExecutableForVenv(pythonHome)
                : GetPythonPathExecutableForStandardInstallation(pythonHome);
        }

        public string GetScriptsPath(string scriptName)
        {
            return Path.Combine(AppContext.BaseDirectory, "scripts", scriptName);
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

        public string GetSitePackagesPathFromPythonHome()
        {
            var pythonHome = Environment.GetEnvironmentVariable("PYTHONHOME", EnvironmentVariableTarget.Process);

            if (string.IsNullOrEmpty(pythonHome))
                return string.Empty;

            var isVirtualEnv = pythonHome.EndsWith(EnvironmentConsts.VirtualEnvironment, StringComparison.OrdinalIgnoreCase);

            return isVirtualEnv
                ? Path.Combine(pythonHome, "Lib", "site-packages")
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
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) switch
            {
                true => Path.Combine(pythonHome, "Scripts", "python.exe"),
                false when RuntimeInformation.IsOSPlatform(OSPlatform.Linux) => Path.Combine(pythonHome, "bin", "python"),
                _ => throw new PlatformNotSupportedException("Unsupported operating system.")
            };
        }

        private static string GetPythonPathExecutableForStandardInstallation(string pythonHome)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) switch
            {
                true => Path.Combine(pythonHome, "python.exe"),
                false when RuntimeInformation.IsOSPlatform(OSPlatform.Linux) => Path.Combine(pythonHome, "bin", "python"),
                _ => throw new PlatformNotSupportedException("Unsupported operating system.")
            };
        }

    }
}
