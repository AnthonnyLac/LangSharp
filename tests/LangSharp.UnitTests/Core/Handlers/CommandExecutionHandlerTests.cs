using LangSharp.Core.Commands;
using LangSharp.Core.Enums;
using LangSharp.Core.Handlers;
using LangSharp.Core.Interfaces.Handlers.Base;
using LangSharp.Core.Interfaces.Providers;
using LangSharp.Core.Interfaces.Services;
using Moq;

namespace LangSharp.UnitTests.Core.Handlers
{
    public class CommandExecutionHandlerTests
    {
        [Fact]
        public void Handle_ShouldExecuteDatabaseQuery_WhenCommandTypeIsExecuteDatabaseQuery()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            var cloudAIProviderMock = new Mock<ICloudAIProvider>();
            cloudAIProviderMock
                .Setup(p => p.ExecuteDatabaseQuery("SELECT * FROM Users"))
                .Returns("Query Result");

            var handler = new CommandExecutionHandler(pythonServiceMock.Object, cloudAIProviderMock.Object);
            var commandRequest = new CommandRequest(TypeCommand.ExecuteDatabaseQuery, "SELECT * FROM Users");

            // Act
            var result = handler.Handle(commandRequest);

            // Assert
            Assert.Equal("Query Result", result);
            cloudAIProviderMock.Verify(p => p.ExecuteDatabaseQuery("SELECT * FROM Users"), Times.Once);
        }

        [Fact]
        public void Handle_ShouldGetResponse_WhenCommandTypeIsGetResponse()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            var cloudAIProviderMock = new Mock<ICloudAIProvider>();
            cloudAIProviderMock
                .Setup(p => p.GetResponse("Hello"))
                .Returns("Response Result");

            var handler = new CommandExecutionHandler(pythonServiceMock.Object, cloudAIProviderMock.Object);
            var commandRequest = new CommandRequest(TypeCommand.GetResponse, "Hello");

            // Act
            var result = handler.Handle(commandRequest);

            // Assert
            Assert.Equal("Response Result", result);
            cloudAIProviderMock.Verify(p => p.GetResponse("Hello"), Times.Once);
        }

        [Fact]
        public void Handle_ShouldReturnUnknownCommand_WhenCommandTypeIsInvalid()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            var cloudAIProviderMock = new Mock<ICloudAIProvider>();

            var handler = new CommandExecutionHandler(pythonServiceMock.Object, cloudAIProviderMock.Object);
            var commandRequest = new CommandRequest((TypeCommand)999, "Invalid Command");

            // Act
            var result = handler.Handle(commandRequest);

            // Assert
            Assert.Equal("Unknown command", result);
        }

        [Fact]
        public void Handle_ShouldPassRequestToNextHandler_WhenRequestIsNotCommandRequest()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            var cloudAIProviderMock = new Mock<ICloudAIProvider>();
            var nextHandlerMock = new Mock<IHandler>();

            nextHandlerMock
                .Setup(h => h.Handle(It.IsAny<object>()))
                .Returns("Next Handler Result");

            var handler = new CommandExecutionHandler(pythonServiceMock.Object, cloudAIProviderMock.Object);
            handler.SetNext(nextHandlerMock.Object);

            // Act
            var result = handler.Handle("Non-Command Request");

            // Assert
            Assert.Equal("Next Handler Result", result);
            nextHandlerMock.Verify(h => h.Handle("Non-Command Request"), Times.Once);
        }
    }
}
