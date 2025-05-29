using LangSharp.Core.Enums;

namespace LangSharp.IntegrationTests.Fixtures
{

    public class LangChainGpt4oMiniLangSharpServiceFixture : LangSharpServiceFixtureBase
    {
        public LangChainGpt4oMiniLangSharpServiceFixture()
            : base(model: "gpt-4o-mini", provider: AIProviderType.LangChain) { }
    }

}
