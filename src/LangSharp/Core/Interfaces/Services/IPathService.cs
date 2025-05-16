namespace LangSharp.Core.Interfaces.Services
{
    public interface IPathService
    {
        string GetNuggetPath();
        string GetPythonPath();
        string GetPythonDllPath();
        string GetPythonPathExecutable();
        string GetSitePackagesPath(string basePath);
        string GetVenvPath();
        string GetScriptsPath(string scriptName);
        string GetScriptsPathByPackageDir(string scriptName);
        string GetSitePackagesPath();
        string GetDirectoryName(string? path);
    }
}
