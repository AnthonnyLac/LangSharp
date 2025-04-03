using LangSharp.Core.Enums;

namespace LangSharp.Core.Configuration
{
    public class LangSharpConfiguration
    {
        public AIProviderType AIProvider { get; set; }
        public string? ApiKey { get; set; }
        public string? Model { get; set; }
        public string? DatabaseUri { get; set; }
    }
}
