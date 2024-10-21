using Python.Runtime;
using System.IO;
using System.Reflection;

namespace LangSharp.Out
{
    public class PythonInterop  : IDisposable
    {
        private static IntPtr threadState;

        public PythonInterop()
        {
            // Chama o método para configurar o caminho do Python

        }


        public void InitializePython()
        {
            //Adiciona path DLL Python
            SetPythonPath();

            if (!PythonEngine.IsInitialized)
            {
                PythonEngine.Initialize();
                threadState = PythonEngine.BeginAllowThreads();
            }

        }

        public void ExecutePythonCode(string code)
        {
            using (Py.GIL())  // Obtém o Global Interpreter Lock
            {
                // Executa um código Python
                PythonEngine.Exec(code);
            }
        }

        public void ExecutePythonScript(string scriptName, string moduleName, string methodName, params object[] args )
        {


            // Define o caminho para o script Python
            var pythonScriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"scripts", scriptName);

            // Verifica se o arquivo do script existe
            if (!File.Exists(pythonScriptPath))
            {
                throw new FileNotFoundException($"O script Python não foi encontrado: {pythonScriptPath}");
            }


            using (Py.GIL())  // Obtém o Global Interpreter Lock
            {
                // Adiciona o diretório do script ao sys.path para que o módulo possa ser encontrado
                dynamic sys = Py.Import("sys");
                sys.path.append(Path.GetDirectoryName(pythonScriptPath));

                // Importa o módulo Python
                dynamic module = Py.Import(moduleName);

                // Obtém a função desejada
                dynamic method = module.GetAttr(methodName);

                // Chama a função com os argumentos passados usando Invoke
                dynamic result = method(args[0], args[1]);

                // (Opcional) Processar o resultado conforme necessário
                Console.WriteLine($"Resultado da chamada: {result}");

            }
        }


        //Será só isso pra evitar vazamento de memoria?
        public void Dispose()
        {
            // Libera recursos
            PythonEngine.EndAllowThreads(threadState);
            PythonEngine.Shutdown();
        }

        internal string CallPythonFunction()
        {
            throw new NotImplementedException();
        }

        private string GetPythonBasePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"py-dll\python\3.11.7\tools");
        }

        private void SetPythonPath()
        {
            var pythonHome = GetPythonBasePath();

            // Verifica se o diretório existe
            if (!string.IsNullOrEmpty(pythonHome) && Directory.Exists(pythonHome))
            {
                string sitePackagesPath = Path.Combine(pythonHome, "Lib", "site-packages");

                // Especificando a DLL do Python
                string pythonDllPath = Path.Combine(pythonHome, "python311.dll"); // Ajuste conforme sua versão do Python
                Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDllPath);
            }
            else
            {
                throw new DirectoryNotFoundException($"O diretório do Python não foi encontrado: {pythonHome}");
            }
        }

    }
}
