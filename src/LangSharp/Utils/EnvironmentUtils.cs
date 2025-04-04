namespace LangSharp.Utils
{
    public static class EnvironmentUtils
    {
        public static string GetNugetPackageDirPath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages");
        public static string GetNugetPythonRoot() => Path.Combine(GetNugetPackageDirPath(), "python", EnvironmentConsts.PythonVersion);
        public static string GetPythonPath() => Path.Combine(GetNugetPythonRoot(), "tools");
        public static string GetVenvPath() => Path.Combine(GetNugetPythonRoot(), EnvironmentConsts.VirtualEnvironment);
        public static string GetSitePackagesPath(string pythonHome) => Path.Combine(pythonHome, "Lib", "site-packages");
        public static string GetScriptsPath(string scriptName) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"scripts", scriptName);
        public static string GetPythonDllPath() => Path.Combine(GetPythonPath(), EnvironmentConsts.DllVersionName);
        public static string? GetPythonHomeFromEnvironment() => Environment.GetEnvironmentVariable("PYTHONHOME", EnvironmentVariableTarget.Process);
        public static string? GetPythonPathFromEnvironment() => Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process);
        public static string? GetPythonPathExecutable()
        {
            var pythonHome = GetPythonHomeFromEnvironment();
            if (string.IsNullOrEmpty(pythonHome))
            {
                return null;
            }

            var isVirtualEnv = pythonHome.EndsWith(EnvironmentConsts.VirtualEnvironment, StringComparison.OrdinalIgnoreCase);
            return isVirtualEnv ? Path.Combine(pythonHome, "Scripts", "python.exe") : Path.Combine(pythonHome, "python.exe");
        }
    }
}
