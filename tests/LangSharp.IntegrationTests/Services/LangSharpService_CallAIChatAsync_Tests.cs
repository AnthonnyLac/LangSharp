using LangSharp.Core.Interfaces.Services;
using LangSharp.IntegrationTests.Fixtures;

namespace LangSharp.IntegrationTests.Services
{
    public class LangSharpService_CallAIChatAsync_Tests : IClassFixture<LangSharpServiceFixture>
    {
        private readonly ILangSharpService _service;

        public LangSharpService_CallAIChatAsync_Tests(LangSharpServiceFixture fixture)
        {
            _service = fixture.Service;
        }

        [Fact(DisplayName = "Null prompt returns error from RequestValidatorHandler")]
        public async Task CallAIChatAsync_NullPrompt_ReturnsError()
        {
            var result = await _service.CallAIChatAsync(null!);
            var resultString = result as string;

            Assert.NotNull(resultString);
            Assert.Contains("Error", resultString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact(DisplayName = "Empty prompt returns error from RequestValidatorHandler")]
        public async Task CallAIChatAsync_EmptyPrompt_ReturnsError()
        {
            var result = await _service.CallAIChatAsync(string.Empty);
            var resultString = result as string;

            Assert.NotNull(resultString);
            Assert.Contains("Error: Request parameter is empty.", resultString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact(DisplayName = "Valid prompt triggers full handler chain and returns AI response")]
        public async Task CallAIChatAsync_ValidPrompt_ReturnsAIResponse()
        {
            var result = await _service.CallAIChatAsync("Say hello");
            var resultString = result as string;

            Assert.NotNull(resultString);
            Assert.Contains("hello", resultString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact(DisplayName = "Portuguese prompt returns Portuguese response")]
        public async Task CallAIChatAsync_PortuguesePrompt_ReturnsPortugueseResponse()
        {
            var prompt = "Diga olá em português";

            var result = await _service.CallAIChatAsync(prompt);
            var resultString = result as string;

            Assert.NotNull(resultString);
            Assert.Contains("olá", resultString, StringComparison.OrdinalIgnoreCase);
        }
    }
}
