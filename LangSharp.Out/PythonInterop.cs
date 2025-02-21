using Python.Runtime;

namespace LangSharp.Out
{
    public class PythonInterop  : IDisposable
    {

        private static IntPtr threadState;

        public PythonInterop()
        {
          
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

        public void InstallPythonPackage(string packageName)
        {
            // Define o caminho para o ambiente virtual
            string venvPath = GetVenvPath();

            // Verifica se o ambiente virtual existe
            if (!Directory.Exists(venvPath))
            {
                Console.WriteLine("O ambiente virtual não existe. Crie um primeiro.");
                return;
            }

            using (Py.GIL()) // GIL - Global Interpreter Lock
            {
                dynamic subprocess = Py.Import("subprocess");

                // Caminho para o executável do Python dentro do venv
                string pythonExecutable = Path.Combine(venvPath, "Scripts", "python.exe");

                // Comando para instalar o pacote no ambiente virtual
                subprocess.check_call(new[] { pythonExecutable, "-m", "pip", "install", packageName });
            }
        }

        public void CreateVirtualEnvironment()
        {
            // Define o caminho para o ambiente virtual
            string venvPath = GetVenvPath();

            // Cria o ambiente virtual se ele não existir
            if (!Directory.Exists(venvPath))
            {
                using (Py.GIL()) // GIL - Global Interpreter Lock
                {
                    dynamic subprocess = Py.Import("subprocess");
                    // Executa o comando para criar o ambiente virtual
                    subprocess.check_call(new[] { "python", "-m", "venv", venvPath });
                }
            }
            else
            {
                Console.WriteLine("O ambiente virtual já existe: " + venvPath);
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
            string nugetPackageDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages");

            return Path.Combine(nugetPackageDir, "python", "3.11.7", "tools");
        }

        private string GetVenvPath()
        {
            var currentDirectory = GetRootPath();

            return Path.Combine(currentDirectory, "myenv");
        }


        private string GetRootPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            // Encontra o índice da última ocorrência de "LangSharp.Out"
            var index = currentDirectory.IndexOf("LangSharp.Out");

            // Se a substring foi encontrada, retorna a parte até e incluindo "LangSharp.Out"
            if (index != -1)
            {
                // Adiciona o comprimento de "LangSharp.Out" para incluir na substring
                return currentDirectory.Substring(0, index + "LangSharp.Out".Length);
            }

            // Retorna o diretório atual se "LangSharp.Out" não foi encontrado
            return currentDirectory;
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
                Environment.SetEnvironmentVariable("PYTHONHOME", pythonHome);
                Environment.SetEnvironmentVariable("PYTHONPATH", sitePackagesPath);

            }
            else
            {
                throw new DirectoryNotFoundException($"O diretório do Python não foi encontrado: {pythonHome}");
            }
        }

    }
}
