using LangSharp.Core.Handlers;
using LangSharp.Core.Interfaces.Handlers.Base;
using LangSharp.Core.Interfaces.Services;
using Moq;

namespace LangSharp.UnitTests.Core.Handlers
{
    public class VirtualEnvironmentHandlerTests
    {
        [Fact]
        public void Handle_ShouldCreateAndActivateVirtualEnv_WhenVirtualEnvIsNotCreated()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            pythonServiceMock
                .Setup(p => p.IsVirtualEnvCreated())
                .Returns(false);

            var handler = new VirtualEnvironmentHandler(pythonServiceMock.Object);

            // Act
            handler.Handle(new object());

            // Assert
            pythonServiceMock.Verify(p => p.IsVirtualEnvCreated(), Times.Once);
            pythonServiceMock.Verify(p => p.CreateVirtualEnv(), Times.Once);
            pythonServiceMock.Verify(p => p.ActivateVirtualEnv(), Times.Once);
        }

        [Fact]
        public void Handle_ShouldOnlyActivateVirtualEnv_WhenVirtualEnvIsAlreadyCreated()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            pythonServiceMock
                .Setup(p => p.IsVirtualEnvCreated())
                .Returns(true);

            var handler = new VirtualEnvironmentHandler(pythonServiceMock.Object);

            // Act
            handler.Handle(new object());

            // Assert
            pythonServiceMock.Verify(p => p.IsVirtualEnvCreated(), Times.Once);
            pythonServiceMock.Verify(p => p.CreateVirtualEnv(), Times.Never);
            pythonServiceMock.Verify(p => p.ActivateVirtualEnv(), Times.Once);
        }

        [Fact]
        public void Handle_ShouldPassRequestToNextHandler()
        {
            // Arrange
            var pythonServiceMock = new Mock<IPythonService>();
            pythonServiceMock
                .Setup(p => p.IsVirtualEnvCreated())
                .Returns(true);

            var nextHandlerMock = new Mock<IHandler>();
            nextHandlerMock
                .Setup(h => h.Handle(It.IsAny<object>()))
                .Returns("Next Handler Result");

            var handler = new VirtualEnvironmentHandler(pythonServiceMock.Object);
            handler.SetNext(nextHandlerMock.Object);

            // Act
            var result = handler.Handle(new object());

            // Assert
            Assert.Equal("Next Handler Result", result);
            pythonServiceMock.Verify(p => p.IsVirtualEnvCreated(), Times.Once);
            pythonServiceMock.Verify(p => p.ActivateVirtualEnv(), Times.Once);
            nextHandlerMock.Verify(h => h.Handle(It.IsAny<object>()), Times.Once);
        }
    }
}
