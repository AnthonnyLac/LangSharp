using LangSharp.Core.Abstractions;
using LangSharp.Core.Configuration;

namespace LangSharp.Core.Interfaces.Services
{
    public interface IPythonService
    {
        void InitializePython();
        void InstallPythonPackage(string packageName);
        bool IsPythonPackageInstalled(string packageName);
        bool IsPythonInstalled();
        void SetEnvironmentPath();
        void SetEnvironmentConfigs(LangSharpConfiguration configuration);
        string ExecuteCommand(string command);
        string ExecutePythonScript(AbstractScript scriptModel);
        string CallPythonFunction(string moduleName, string functionName, params object[] args);
        void InstallOpenAIDependencies();
        void CreateVirtualEnvironment();
        bool IsVirtualEnvironmentCreated();

    }
}
