using LangSharp.Core.Interfaces.Providers;

namespace LangSharp.Core.Providers
{
    public class OpenAIProvider : ICloudAIProvider
    {
        public string GetResponse(string prompt)
        {
            return "OpenAI response";
        }
    }
}
