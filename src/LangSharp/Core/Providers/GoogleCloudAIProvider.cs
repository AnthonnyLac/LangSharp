using LangSharp.Core.Interfaces.Providers;

namespace LangSharp.Core.Providers
{
    public class GoogleCloudAIProvider : ICloudAIProvider
    {
        public string GetResponse(string prompt)
        {
            return "Google Cloud AI response";
        }
    }
}
