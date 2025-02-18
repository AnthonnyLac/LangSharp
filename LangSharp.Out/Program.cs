using LangSharp.Out.Core;
using LangSharp.Out.Demo;

namespace LangSharp.Out
{
    class Program
    {
        static void Main(string[] args)
        {
            ISDKService sdkFacade = new SDKFacade();

            Console.WriteLine("Digite um comando Python:");
            string command = Console.ReadLine();

            string result = sdkFacade.ExecutePythonCommand(command);
            Console.WriteLine($"Resultado: {result}");

            //Inicia demonstraçao Python No c#

            InityPy.StartDemo();
        }
    }
}
