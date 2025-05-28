using LangSharp.Core.Enums;
using LangSharp.Core.Factorys;
using LangSharp.Core.Interfaces.Services;
using LangSharp.Core.Providers;
using Moq;

namespace LangSharp.UnitTests.Core.Factorys
{
    public class CloudAIProviderFactoryTests
    {
        [Fact]
        public void CreateProvider_ShouldThrowNotImplementedException_WhenProviderTypeIsGoogleCloud()
        {
            // Arrange
            var providerType = AIProviderType.GoogleCloud;
            var pythonServiceMock = new Mock<IPythonService>();

            // Act & Assert
            var exception = Assert.Throws<NotImplementedException>(() =>
                CloudAIProviderFactory.CreateProvider(providerType, pythonServiceMock.Object));

            Assert.Equal("Google Cloud AI provider will be available in a future release.", exception.Message);
        }

        [Fact]
        public void CreateProvider_ShouldReturnOpenAIProvider_WhenProviderTypeIsOpenAI()
        {
            // Arrange
            var providerType = AIProviderType.LangChain;
            var pythonServiceMock = new Mock<IPythonService>();

            // Act
            var provider = CloudAIProviderFactory.CreateProvider(providerType, pythonServiceMock.Object);

            // Assert
            Assert.IsType<LangChainProvider>(provider);
        }

        [Fact]
        public void CreateProvider_ShouldThrowArgumentException_WhenProviderTypeIsUnknown()
        {
            // Arrange
            var providerType = (AIProviderType)999; //Unknown provider type
            var pythonServiceMock = new Mock<IPythonService>();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                CloudAIProviderFactory.CreateProvider(providerType, pythonServiceMock.Object));

            Assert.Equal("Unknown AI provider (Parameter 'providerType')", exception.Message);
        }
    }
}
