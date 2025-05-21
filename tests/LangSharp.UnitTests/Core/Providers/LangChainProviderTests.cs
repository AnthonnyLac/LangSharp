using LangSharp.Core.Interfaces.Services;
using LangSharp.Core.Models;
using LangSharp.Core.Providers;
using Moq;

namespace LangSharp.UnitTests.Core.Providers
{
    public class LangChainProviderTests
    {
        [Fact]
        public void ExecuteDatabaseQuery_ShouldCallExecuteScriptWithCorrectArguments()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            pythonServiceMock
                .Setup(p => p.ExecuteScript(It.IsAny<QueryScriptModel>()))
                .Returns("Query Result");

            Environment.SetEnvironmentVariable("OPENAI_API_KEY", "test-api-key");
            Environment.SetEnvironmentVariable("OPENAI_MODEL", "test-model");
            Environment.SetEnvironmentVariable("OPENAI_DATABASE_URI", "test-db-uri");

            var provider = new LangChainProvider(pythonServiceMock.Object);

            // Act
            var result = provider.ExecuteDatabaseQuery("SELECT * FROM Users");

            // Assert
            Assert.Equal("Query Result", result);
            pythonServiceMock.Verify(p => p.ExecuteScript(It.Is<QueryScriptModel>(
                script => script.ArgsFunction[0].ToString() == "test-api-key" &&
                          script.ArgsFunction[1].ToString() == "SELECT * FROM Users" &&
                          script.ArgsFunction[2].ToString() == "test-model" &&
                          script.ArgsFunction[3].ToString() == "test-db-uri"
            )), Times.Once);
        }

        [Fact]
        public void GetResponse_ShouldCallExecuteScriptWithCorrectArguments()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            pythonServiceMock
                .Setup(p => p.ExecuteScript(It.IsAny<LLMScriptModel>()))
                .Returns("Response Result");

            Environment.SetEnvironmentVariable("OPENAI_API_KEY", "test-api-key");
            Environment.SetEnvironmentVariable("OPENAI_MODEL", "test-model");

            var provider = new LangChainProvider(pythonServiceMock.Object);

            // Act
            var result = provider.GetResponse("Hello, OpenAI!");

            // Assert
            Assert.Equal("Response Result", result);
            pythonServiceMock.Verify(p => p.ExecuteScript(It.Is<LLMScriptModel>(
                script => script.ArgsFunction[0].ToString() == "test-api-key" &&
                          script.ArgsFunction[1].ToString() == "Hello, OpenAI!" &&
                          script.ArgsFunction[2].ToString() == "test-model"
            )), Times.Once);
        }

        [Fact]
        public void InstallDependencies_ShouldReturnTrue_WhenAllDependenciesAreInstalledSuccessfully()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            pythonServiceMock.Setup(p => p.InstallPackage(It.IsAny<string>()));

            var provider = new LangChainProvider(pythonServiceMock.Object);

            // Act
            var result = provider.InstallDependencies();

            // Assert
            Assert.True(result);
            pythonServiceMock.Verify(p => p.InstallPackage("langchain-openai"), Times.Once);
            pythonServiceMock.Verify(p => p.InstallPackage("langchain-community"), Times.Once);
            pythonServiceMock.Verify(p => p.InstallPackage("python-dotenv"), Times.Once);
        }

        [Fact]
        public void InstallDependencies_ShouldReturnFalse_WhenAnExceptionOccurs()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            pythonServiceMock
                .Setup(p => p.InstallPackage(It.IsAny<string>()))
                .Throws(new Exception("Installation failed"));

            var provider = new LangChainProvider(pythonServiceMock.Object);

            // Act
            var result = provider.InstallDependencies();

            // Assert
            Assert.False(result);
            pythonServiceMock.Verify(p => p.InstallPackage(It.IsAny<string>()), Times.Once);
        }
    }
}
