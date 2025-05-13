namespace LangSharp.Core.Interfaces.Services
{
    public interface IEnvironmentService
    {
        void ConfigurePythonEnvironment(string pythonHome, string sitePackagesPath, string pythonDllPath);
        void ConfigurePythonEnvironment(string pythonHome, string sitePackagesPath);
    }
}
