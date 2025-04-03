using LangSharp.Core.Abstractions;

namespace LangSharp.Core.Models
{
    public class LLMScriptModel : AbstractScript
    {
        public LLMScriptModel(string name, string moduleName, string functionName, object[] argsFunction) : base(name, moduleName, functionName, argsFunction)
        {
        }

        public override dynamic ProcessMethod(dynamic method)
        {
            string? apiKey = ArgsFunction[1] as string;
            string? prompt = ArgsFunction[0] as string;

            return method(apiKey, prompt);
        }
    }
}
