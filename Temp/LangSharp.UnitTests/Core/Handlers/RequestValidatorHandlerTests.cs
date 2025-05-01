using LangSharp.Core.Commands;
using LangSharp.Core.Enums;
using LangSharp.Core.Handlers;
using LangSharp.Core.Interfaces.Handlers.Base;
using Moq;

namespace LangSharp.UnitTests.Core.Handlers
{
    public class RequestValidatorHandlerTests
    {
        [Fact]
        public void Handle_ShouldReturnErrorMessage_WhenRequestIsNull()
        {
            // Arrange
            var handler = new RequestValidatorHandler();

            // Act
            var result = handler.Handle(null!);

            // Assert
            Assert.Equal("Error: Request is null.", result);
        }

        [Fact]
        public void Handle_ShouldReturnErrorMessage_WhenRequestIsNotCommandRequest()
        {
            // Arrange
            var handler = new RequestValidatorHandler();

            // Act
            var result = handler.Handle(new object());

            // Assert
            Assert.Equal("Error: Request is not a CommandRequest.", result);
        }

        [Fact]
        public void Handle_ShouldReturnErrorMessage_WhenRequestParameterIsEmpty()
        {
            // Arrange
            var handler = new RequestValidatorHandler();
            var commandRequest = new CommandRequest(TypeCommand.GetResponse, string.Empty);

            // Act
            var result = handler.Handle(commandRequest);

            // Assert
            Assert.Equal("Error: Request parameter is empty.", result);
        }

        [Fact]
        public void Handle_ShouldPassRequestToNextHandler_WhenRequestIsValid()
        {
            // Arrange
            var nextHandlerMock = new Moq.Mock<IHandler>();
            nextHandlerMock
                .Setup(h => h.Handle(It.IsAny<object>()))
                .Returns("Next Handler Result");

            var handler = new RequestValidatorHandler();
            handler.SetNext(nextHandlerMock.Object);

            var commandRequest = new CommandRequest(TypeCommand.GetResponse, "Valid Parameter");

            // Act
            var result = handler.Handle(commandRequest);

            // Assert
            Assert.Equal("Next Handler Result", result);
            nextHandlerMock.Verify(h => h.Handle(commandRequest), Times.Once);
        }
    }
}
