using LangSharp.Core.Enums;

namespace LangSharp.IntegrationTests.Fixtures
{
    public class LangChainGemini20FlashLangSharpServiceFixture : LangSharpServiceFixtureBase
    {
        public LangChainGemini20FlashLangSharpServiceFixture()
            : base(model: "gemini-2.0-flash", provider: AIProviderType.LangChain, apiKey: "") 
        {
        }
    }
}
