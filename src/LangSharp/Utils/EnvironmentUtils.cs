using System.Diagnostics;

namespace LangSharp.Utils
{
    public static class EnvironmentUtils
    {
        public static string GetNugetPackageDirPath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages");
        public static string GetSitePackagesPath(string pythonHome) => Path.Combine(pythonHome, "Lib", "site-packages");
        public static string GetScriptsPath(string scriptName) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"scripts", scriptName);
        public static string GetPythonDllPath()
        {
            string pythonHome = GetPythonBasePath();

            return Path.Combine(pythonHome, EnvironmentConsts.DllVersionName);
        }
        public static string GetPythonBasePath()
        {
            string nugetPackageDir = GetNugetPackageDirPath();

            return Path.Combine(nugetPackageDir, "python", EnvironmentConsts.PythonVersion, "tools");
        }

        public static string GetVenvPath()
        {
            var path = GetNugetPackageDirPath();

            return Path.Combine(path, "python", EnvironmentConsts.PythonVersion, EnvironmentConsts.VirtualEnvironment);
        }

    }
}
