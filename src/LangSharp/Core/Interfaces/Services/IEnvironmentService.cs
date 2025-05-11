namespace LangSharp.Core.Interfaces.Services
{
    public interface IEnvironmentService
    {
        string GetPythonPath();
        string GetPythonDllPath();
        string GetSitePackagesPath(string basePath);
        string? GetPythonPathExecutable();
        string GetScriptsPath(string scriptName);
        string GetScriptsPathByPackageDir(string scriptName);
        string GetVenvPath();
        void ConfigurePythonEnvironment(string pythonHome, string sitePackagesPath, string pythonDllPath);
        void ConfigurePythonEnvironment(string pythonHome, string sitePackagesPath);
        string GetDirectoryName(string? path);
        bool IsValidDirectory(string? path);
        bool IsFileExist(string? path);
    }
}
