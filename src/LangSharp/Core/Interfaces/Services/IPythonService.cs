using LangSharp.Core.Abstractions;

namespace LangSharp.Core.Interfaces.Services
{
    public interface IPythonService
    {
        void InitializePythonEngine();
        void InstallPackage(string packageName);
        bool IsPackageInstalled(string packageName);
        bool IsPythonEnvironmentInstalled();
        void ConfigureEnvironmentPaths();
        string ExecuteScript(AbstractScript scriptModel);
        void CreateVirtualEnv();
        bool IsVirtualEnvCreated();
        void ActivateVirtualEnv();
    }
}
