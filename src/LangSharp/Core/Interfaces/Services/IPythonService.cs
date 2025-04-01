using System.Diagnostics;

namespace LangSharp.Core.Interfaces.Services
{
    public interface IPythonService
    {
        void InitializePython();
        void SetEnvironmentPath();
        bool ArePythonNetVariablesSet();
        string ExecuteCommand(string command);
        void DisposePython();
        string ExecutePythonScript(object scriptModel);
        string CallPythonFunction(string moduleName, string functionName, params object[] args);

        bool IsPythonInstalled();
       
    }
}
