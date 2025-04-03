using LangSharp.Core.Interfaces.Providers;

namespace LangSharp.Core.Providers
{
    public class GoogleCloudAIProvider : ICloudAIProvider
    {
        public string ExecuteDatabaseQuery(string query)
        {
            return "Google Cloud AI response";
        }

        public string GetResponse(string prompt)
        {
            return "Google Cloud AI response";
        }
    }
}
