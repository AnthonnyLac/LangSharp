using LangSharp.Core.Interfaces.Providers;
using LangSharp.Core.Interfaces.Services;

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
            return _pythonService.ExecuteCommand(prompt);
        }
    }
}
