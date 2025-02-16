using System;
using System.Diagnostics;
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
            if (!PythonEngine.IsInitialized)
            {
                string pythonHome = Environment.GetEnvironmentVariable("PYTHONHOME") ?? @"C:\Python39";
                string pythonDll = GetPythonDllPath(pythonHome);

                if (!System.IO.File.Exists(pythonDll))
                {
                    throw new FileNotFoundException($"Erro: Python DLL não encontrada em {pythonDll}. Verifique a instalação do Python.");
                }

                Runtime.PythonDLL = pythonDll;
                PythonEngine.Initialize();
            }
        }

        public static string GetPythonDllPath(string pythonHome)
        {
            string version = GetPythonVersion();
            if (string.IsNullOrEmpty(version))
            {
                throw new InvalidOperationException("Não foi possível detectar a versão do Python.");
            }

            string majorVersion = version.Split('.')[0] + version.Split('.')[1]; // Ex: "39" para Python 3.9
            return System.IO.Path.Combine(pythonHome, $"python{majorVersion}.dll");
        }

        public static string GetPythonVersion()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = "-c \"import sys; print(sys.version)\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    if (process == null) return null;

                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd().Trim();
                    return output.Split(' ')[0]; // Retorna a versão principal (ex: "3.9.0")
                }
            }
            catch
            {
                return null;
            }
        }


        public static bool ArePythonNetVariablesSet()
        {
            string pythonHome = Environment.GetEnvironmentVariable("PYTHONHOME");
            string pythonPath = Environment.GetEnvironmentVariable("PYTHONPATH");

            if (string.IsNullOrEmpty(pythonHome) || string.IsNullOrEmpty(pythonPath))
            {
                try
                {
                    string pythonExecutable = GetPythonExecutablePath();
                    if (!string.IsNullOrEmpty(pythonExecutable))
                    {
                        pythonHome = System.IO.Path.GetDirectoryName(pythonExecutable);
                        Environment.SetEnvironmentVariable("PYTHONHOME", pythonHome);
                        Environment.SetEnvironmentVariable("PYTHONPATH", pythonHome + @"\Lib;" + pythonHome + @"\DLLs");
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        public static string GetPythonExecutablePath()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = "-c \"import sys; print(sys.executable)\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    if (process == null) return null;

                    process.WaitForExit();
                    return process.StandardOutput.ReadToEnd().Trim();
                }
            }
            catch
            {
                return null;
            }
        }


        public static string ExecuteCommand(string command)
        {
            try
            {
                using (Py.GIL())
                {
                    dynamic pyScope = Py.CreateScope();
                    pyScope.Exec(command);
                    return "Comando executado com sucesso.";
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao executar comando Python: {ex.Message}");
                return $"Erro ao executar comando Python: {ex.Message}";
            }
        }

        public static string CallPythonFunction(string moduleName, string functionName, params object[] args)
        {
            try
            {
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
                Console.Error.WriteLine($"Erro ao chamar função Python: {ex.Message}");
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