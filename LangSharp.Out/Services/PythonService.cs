using LangSharp.Out.Consts;
using LangSharp.Out.Models.Base;
using LangSharp.Out.Utils;
using Python.Runtime;
using System.Diagnostics;

namespace LangSharp.Out.Services
{
    /// <summary>
    /// Serviço responsável por executar comandos Python no ambiente configurado.
    /// </summary>

    public static class PythonService
    {
        public static void InitializePython()
        {
            if (PythonEngine.IsInitialized)
                return;

            PythonEngine.Initialize();
            var threadState = PythonEngine.BeginAllowThreads();

            PythonThread.SetThreadState(threadState);
        }

        public static void SetEnvironmentPath()
        {
            var pythonHome = EnvironmentUtils.GetPythonBasePath();

            if (string.IsNullOrEmpty(pythonHome) || !Directory.Exists(pythonHome))
                throw new DirectoryNotFoundException($"O diretório do Python não foi encontrado: {pythonHome}");

            var sitePackagesPath = EnvironmentUtils.GetSitePackagesPath(pythonHome);
            var pythonDllPath = EnvironmentUtils.GetPythonDllPath();

            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDllPath);
            Environment.SetEnvironmentVariable("PYTHONHOME", pythonHome);
            Environment.SetEnvironmentVariable("PYTHONPATH", sitePackagesPath);
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

        public static void DisposePython()
        {
            var threadState = PythonThread.GetThreadState();

            if(threadState == default)
                return;

            PythonEngine.EndAllowThreads(threadState);
            PythonEngine.Shutdown();
        }

        public static string ExecutePythonScript(BaseScriptModel scriptModel)

        {
            var pythonScriptPath = EnvironmentUtils.GetScriptsPath(scriptModel.Name);

            if (!File.Exists(pythonScriptPath))
            {
                throw new FileNotFoundException($"O script Python não foi encontrado: {pythonScriptPath}");
            }


            using (Py.GIL())  
            {
                // Adiciona o diretório do script ao sys.path para que o módulo possa ser encontrado
                dynamic sys = Py.Import("sys");
                sys.path.append(Path.GetDirectoryName(pythonScriptPath));

                // Importa o módulo Python
                dynamic module = Py.Import(scriptModel.ModuleName);

                // Obtém a função desejada
                dynamic method = module.GetAttr(scriptModel.FunctionName);

                // Chama a função com os argumentos passados usando Invoke
                dynamic result = scriptModel.ProcessMethod(method);

                // (Opcional) Processar o resultado conforme necessário
                return $"Resultado da chamada: {result}";
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