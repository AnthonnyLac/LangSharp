using LangSharp.Core.Interfaces.Providers;
using LangSharp.Core.Interfaces.Services;
using LangSharp.Core.Models;

namespace LangSharp.Core.Providers
{
    public class OpenAIProvider : ICloudAIProvider
    {
        private readonly IPythonService _pythonService;

        public OpenAIProvider(IPythonService pythonService)
        {
            _pythonService = pythonService;
        }

        public string ExecuteDatabaseQuery(string query)
        {
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")!;
            string model = Environment.GetEnvironmentVariable("OPENAI_MODEL")!;
            string dbUri = Environment.GetEnvironmentVariable("OPENAI_DATABASE_URI")!;

            var scriptModel = new QueryScriptModel("LLMQuery.py", "LLMQuery", "CallOpenIAQueryLangSharp", [apiKey, query, model, dbUri]);

            return _pythonService.ExecutePythonScript(scriptModel);
        }

        public string GetResponse(string prompt)
        {
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")!;
            string model = Environment.GetEnvironmentVariable("OPENAI_MODEL")!;

            var scriptModel = new LLMScriptModel("llm.py", "llm", "CallOpenIALangSharp", [apiKey, prompt, model]);

            return _pythonService.ExecutePythonScript(scriptModel);
        }
    }
}
