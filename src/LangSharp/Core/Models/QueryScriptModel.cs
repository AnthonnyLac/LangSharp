using LangSharp.Core.Abstractions;

namespace LangSharp.Core.Models
{
    public class QueryScriptModel : AbstractScript
    {
        public QueryScriptModel(string name, string moduleName, string functionName, object[] argsFunction) : base(name, moduleName, functionName, argsFunction)
        {
        }

        public override dynamic ProcessMethod(dynamic method)
        {
            string? apiKey = ArgsFunction[0] as string;
            string? query = ArgsFunction[1] as string;
            string? model = ArgsFunction[2] as string;
            string? db_uri = ArgsFunction[3] as string;

            var result = method(apiKey, query, model, db_uri);

            try
            {
                return result["output"];
            }
            catch
            {
                return result;
            }
        }
    }
}
