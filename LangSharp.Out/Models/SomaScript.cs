using LangSharp.Out.Models.Base;

namespace LangSharp.Out.Models
{
    public class SomaScript : BaseScriptModel
    {
        public SomaScript(string name, string moduleName, string functionName, object[] argsFunction) : base(name, moduleName, functionName, argsFunction)
        {
        }

        public override dynamic ProcessMethod(dynamic method)
        {
            return method(ArgsFunction[0], ArgsFunction[1]);
        }
    }
}
