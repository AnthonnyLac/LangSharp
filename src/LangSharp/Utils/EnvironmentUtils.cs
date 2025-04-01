namespace LangSharp.Utils
{
    public static class EnvironmentUtils
    {
        public static string GetSitePackagesPath(string pythonHome) => Path.Combine(pythonHome, "Lib", "site-packages");
        public static string GetScriptsPath(string scriptName) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"scripts", scriptName);
        public static string GetPythonDllPath()
        {
            string pythonHome = GetPythonBasePath();

            return Path.Combine(pythonHome, EnvironmentConsts.DllVersionName);
        }
        public static string GetPythonBasePath()
        {
            string nugetPackageDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages");

            return Path.Combine(nugetPackageDir, "python", EnvironmentConsts.PythonVersion, "tools");
        }

    }
}
