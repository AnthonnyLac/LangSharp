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

        public string GetResponse(string prompt)
        {
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")!;
            var scriptModel = new LLMScriptModel("llm.py", "llm", "CallOpenIALangSharp", [prompt, apiKey]);
            return _pythonService.ExecutePythonScript(scriptModel);
        }
    }
}
