using LangSharp.Core.Interfaces.Services;
using LangSharp.IntegrationTests.Fixtures;

namespace LangSharp.IntegrationTests.Services
{
    public class LangSharpService_CallAIChat_LangChainGpt4oMini_Tests : IClassFixture<LangChainGpt4oMiniLangSharpServiceFixture>
    {
        private readonly ILangSharpService _service;

        public LangSharpService_CallAIChat_LangChainGpt4oMini_Tests(LangChainGpt4oMiniLangSharpServiceFixture fixture)
        {
            _service = fixture.Service;
        }

        [Fact(DisplayName = "Null prompt returns error from RequestValidatorHandler")]
        public void CallAIChatAsync_NullPrompt_ReturnsError()
        {
            var result = _service.CallAIChat(null!);
            var resultString = result as string;

            Assert.NotNull(resultString);
            Assert.Contains("Error", resultString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact(DisplayName = "Empty prompt returns error from RequestValidatorHandler")]
        public void CallAIChatAsync_EmptyPrompt_ReturnsError()
        {
            var result = _service.CallAIChat(string.Empty);
            var resultString = result as string;

            Assert.NotNull(resultString);
            Assert.Contains("Error: Request parameter is empty.", resultString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact(DisplayName = "Valid prompt triggers full handler chain and returns AI response")]
        public void  CallAIChatAsync_ValidPrompt_ReturnsAIResponse()
        {
            var result = _service.CallAIChat("Say hello");
            var resultString = result as string;

            Assert.NotNull(resultString);
            Assert.Contains("hello", resultString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact(DisplayName = "Portuguese prompt returns Portuguese response")]
        public void CallAIChatAsync_PortuguesePrompt_ReturnsPortugueseResponse()
        {
            var prompt = "Diga olá em português";

            var result = _service.CallAIChat(prompt);
            var resultString = result as string;

            Assert.NotNull(resultString);
            Assert.Contains("olá", resultString, StringComparison.OrdinalIgnoreCase);
        }
    }
}
