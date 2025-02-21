using System.Diagnostics;

namespace LangSharp.Out.Utils
{
    /// <summary>
    /// Classe utilitária para manipulação de variáveis de ambiente.
    /// </summary>
    public static class EnvironmentUtils
    {
        public const string PythonVersion = "3.11.7";
        public const string DllVersionName = "python311.dll";
        public static string GetSitePackagesPath(string pythonHome) => Path.Combine(pythonHome, "Lib", "site-packages");
        public static string GetScriptsPath(string scriptName) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"scripts", scriptName);
        public static string GetPythonDllPath()
        {
            string pythonHome = GetPythonBasePath();

            return Path.Combine(pythonHome, DllVersionName);
        }
        public static string GetPythonBasePath() 
        {
            string nugetPackageDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages");

            return Path.Combine(nugetPackageDir, "python", PythonVersion, "tools");
        }

        public static bool IsPythonInstalled()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = "--version",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    if (process == null) return false;

                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    return !string.IsNullOrEmpty(output) || !string.IsNullOrEmpty(error);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
