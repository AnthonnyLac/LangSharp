using LangSharp.Core.Enums;

namespace LangSharp.IntegrationTests.Fixtures
{
    public class DefaultLangSharpServiceFixture : LangSharpServiceFixtureBase
    {
        public DefaultLangSharpServiceFixture() : base() { }
    }

    public class CustomApiKeyLangSharpServiceFixture : LangSharpServiceFixtureBase
    {
        public CustomApiKeyLangSharpServiceFixture() : base(apiKey: "SUA_API_KEY_AQUI") { }
    }

    public class LangChainGpt4oMiniLangSharpServiceFixture : LangSharpServiceFixtureBase
    {
        public LangChainGpt4oMiniLangSharpServiceFixture()
            : base(model: "gpt-4o-mini", provider: AIProviderType.LangChain) { }
    }

}
