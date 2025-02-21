using LangSharp.Out.Core;
using LangSharp.Out.Models;

namespace LangSharp.Out
{
    class Program
    {
        static void Main(string[] args)
        {
            using var sdkFacade = new SDKFacade();

            //Commando Python
            string command = "print('Hello World From Python :p')";

            string result = sdkFacade.ExecutePythonCommand(command);

            //Script Python
            var scriptDto = new SomaScript("LangSharp.py", "LangSharp", "somar", [1, 2]);
            var resultScript = sdkFacade.ExecutePythonScript(scriptDto);

            Console.WriteLine($"Script: {resultScript}");
        }
    }
}
