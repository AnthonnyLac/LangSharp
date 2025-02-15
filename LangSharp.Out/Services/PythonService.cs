using System;
using LangSharp.Out.Utils;
using Python.Runtime;

namespace LangSharp.Out.Services
{
    /// <summary>
    /// Serviço responsável por executar comandos Python no ambiente configurado.
    /// </summary>

    public static class PythonService
    {
        public static void InitializePython()
        {
            if (!EnvironmentUtils.IsPythonInstalled())
                throw new Exception("Python não está instalado.");

            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append("caminho_do_seu_script_python"); // Define caminhos necessários.

                dynamic os = Py.Import("os");
                os.environ["PYTHONNET_PYDLL"] = "caminho_para_python.dll"; // Define variáveis.
            }
        }

        public static string CallPythonFunction(string moduleName, string functionName, params object[] args)
        {
            try
            {
                InitializePython();
                using (Py.GIL())
                {
                    dynamic pyModule = Py.Import(moduleName);
                    dynamic pyFunction = pyModule.GetAttr(functionName);
                    dynamic result = pyFunction.Invoke(args);
                    return result?.ToString() ?? "Execução concluída sem retorno.";
                }
            }
            catch (Exception ex)
            {
                return $"Erro ao chamar função Python: {ex.Message}";
            }
        }

        public static string AuthenticateCloud(string provider, string apiKey)
        {
            return CloudAuthFactory.Create(provider).Authenticate(apiKey);
        }

        public static string ProcessLLMRequest(string input)
        {
            return CallPythonFunction("llm_module", "processar_solicitacao", input);
        }
    }
}