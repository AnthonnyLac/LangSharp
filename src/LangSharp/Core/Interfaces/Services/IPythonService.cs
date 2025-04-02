using System.Diagnostics;

namespace LangSharp.Core.Interfaces.Services
{
    public interface IPythonService
    {
        void InitializePython();
        bool IsPythonInstalled();
        void SetEnvironmentPath();
        bool ArePythonNetVariablesSet();
        string ExecuteCommand(string command);
        string ExecutePythonScript(object scriptModel);
        string CallPythonFunction(string moduleName, string functionName, params object[] args);
    }
}
