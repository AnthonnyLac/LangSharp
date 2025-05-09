using LangSharp.Core.Interfaces.Providers;
using LangSharp.Core.Interfaces.Services;
using LangSharp.Core.Models;
using LangSharp.Utils;

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

            var scriptModel = new QueryScriptModel("llm_sql.py", "llm_sql", "call_llm_sql_langsharp", [apiKey, query, model, dbUri]);

            return _pythonService.ExecuteScript(scriptModel);
        }

        public string GetResponse(string prompt)
        {
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")!;
            string model = Environment.GetEnvironmentVariable("OPENAI_MODEL")!;

            var scriptModel = new LLMScriptModel("llm.py", "llm", "call_llm_langsharp", [apiKey, prompt, model]);

            return _pythonService.ExecuteScript(scriptModel);
        }

        public bool InstallDependencies()
        {
            try
            {
                _pythonService.InstallPackage(PythonPackage.LangChainOpenai);
                _pythonService.InstallPackage(PythonPackage.LangChainCommunity);
                _pythonService.InstallPackage(PythonPackage.PythonDotEnv);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
