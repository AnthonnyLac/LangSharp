namespace LangSharp.Out.Demo
{
    public static class InityPy
    {
        public static void StartDemo() 
        {

            Console.WriteLine("Aperte [s] para iniciar a demo");
            var start = Console.ReadLine();

            if (start == null || !start.Equals("s", StringComparison.OrdinalIgnoreCase))
                return;

            // Cria uma instância da classe PythonInterop
            using var pythonInterop = new PythonInterop();


            try
            {
                // Inicializa o ambiente Python
                pythonInterop.InitializePython();

                // Código Python para testar
                string pythonCode = "print('sim')";

                // Executa o código Python
                pythonInterop.ExecutePythonCode(pythonCode);

                //Dados base
                var scriptName = "LangSharp.py";
                var moduleName = "LangSharp";
                var methodName = "my_function";
                var argsPython = new object[] { 1, 2 };


                // Executa o código Python
                pythonInterop.ExecutePythonScript(scriptName, moduleName, methodName, argsPython);

                //Teste

                pythonInterop.CreateVirtualEnvironment();
                pythonInterop.InstallPythonPackage("langchain");


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
            finally
            {
                // Libera recursos do Python
                pythonInterop.Dispose();
            }
        }


    }
}
