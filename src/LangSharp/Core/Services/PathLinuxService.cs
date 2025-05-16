using LangSharp.Core.Interfaces.Services;
using LangSharp.Utils;

namespace LangSharp.Core.Services
{
    public class PathLinuxService : IPathService
    {
        private const string PythonVersion = "3.11";
        private const string PythonDll = $"libpython{PythonVersion}.so.1.0";
        private const string PythonBin = $"python{PythonVersion}";
        private const string LibDir = "lib";
        private const string SitePackages = "site-packages";
        private const string NugetRoot = "root";
        private const string NugetFolder = ".nuget";
        private const string NugetPackages = "packages";
        private const string ScriptsFolder = "Scripts";
        private const string LangSharpFolder = "langsharp";

        public string GetNuggetPath()
        {
            return Path.Combine(Path.DirectorySeparatorChar.ToString(), Environment.CurrentDirectory, NugetRoot, NugetFolder, NugetPackages);
        }

        public string GetPythonDllPath()
        {
            return Path.Combine("/usr/lib/x86_64-linux-gnu", PythonDll);
        }

        public string GetPythonPath()
        {
            return "/usr";
        }

        public string GetPythonVenvPath()
        {
            return GetVenvPath();
        }

        public string GetPythonPathExecutable()
        {
            var isVenv = Environment.GetEnvironmentVariable("LANGSHARP_IS_VENV", EnvironmentVariableTarget.Process);

            return !string.IsNullOrEmpty(isVenv) && bool.TryParse(isVenv, out var venv) && venv
                ? Path.Combine(GetVenvPath(), "bin", PythonBin)
                : Path.Combine(GetPythonPath(), "bin", PythonBin);
        }

        public string GetScriptsPath(string scriptName)
        {
            return Path.Combine(AppContext.BaseDirectory, ScriptsFolder, scriptName);
        }

        public string GetScriptsPathByPackageDir(string scriptName)
        {
            var nuggetPath = GetNuggetPath();
            var version = EnvironmentConsts.GetLangSharpAssemblyVersion();

            return Path.Combine(nuggetPath, LangSharpFolder, version, ScriptsFolder, scriptName);
        }

        public string GetSitePackagesPath(string basePath)
        {
            return Path.Combine(basePath, LibDir, $"python{PythonVersion}", SitePackages);
        }

        public string GetSitePackagesPath()
        {
            var isVenv = Environment.GetEnvironmentVariable("LANGSHARP_IS_VENV", EnvironmentVariableTarget.Process);

            return !string.IsNullOrEmpty(isVenv) && bool.TryParse(isVenv, out var venv) && venv
                ? GetSitePackagesPath(GetVenvPath())
                : Path.Combine(GetPythonPath(), LibDir, $"python{PythonVersion}", SitePackages);
        }

        public string GetVenvPath()
        {
            var nuggetPath = GetNuggetPath();

            return Path.Combine(nuggetPath, "python", EnvironmentConsts.PythonVersion, EnvironmentConsts.VirtualEnvironment);
        }

        public string GetDirectoryName(string? path)
        {
            return Path.GetDirectoryName(path) ?? string.Empty;
        }
    }
}
