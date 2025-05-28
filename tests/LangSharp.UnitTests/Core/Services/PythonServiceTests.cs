using LangSharp.Core.Interfaces.Infrastructure;
using LangSharp.Core.Interfaces.Services;
using LangSharp.Core.Services;
using Moq;

namespace LangSharp.UnitTests.Core.Services
{

    public class PythonServiceTests
    {
        [Fact]
        public void InitializePythonEngine_ShouldCallInitialize_WhenNotInitialized()
        {
            var pythonRuntimeMock = new Mock<IPythonRuntime>();
            pythonRuntimeMock.Setup(r => r.IsInitialized).Returns(false);

            var service = new PythonService(
                pythonRuntimeMock.Object,
                Mock.Of<IEnvironmentService>(),
                Mock.Of<IPathService>(),
                Mock.Of<IFileSystemService>());

            service.InitializePythonEngine();

            pythonRuntimeMock.Verify(r => r.Initialize(), Times.Once);
        }

        [Fact]
        public void InitializePythonEngine_ShouldNotCallInitialize_WhenAlreadyInitialized()
        {
            var pythonRuntimeMock = new Mock<IPythonRuntime>();
            pythonRuntimeMock.Setup(r => r.IsInitialized).Returns(true);

            var service = new PythonService(
                pythonRuntimeMock.Object,
                Mock.Of<IEnvironmentService>(),
                Mock.Of<IPathService>(),
                Mock.Of<IFileSystemService>());

            service.InitializePythonEngine();

            pythonRuntimeMock.Verify(r => r.Initialize(), Times.Never);
        }

        [Fact]
        public void ConfigureEnvironmentPaths_ShouldThrow_WhenPythonHomeIsInvalid()
        {
            var pathServiceMock = new Mock<IPathService>();
            pathServiceMock.Setup(p => p.GetPythonPath()).Returns((string?)null);

            var fileSystemServiceMock = new Mock<IFileSystemService>();
            fileSystemServiceMock.Setup(f => f.IsValidDirectory(It.IsAny<string>())).Returns(false);

            var service = new PythonService(
                Mock.Of<IPythonRuntime>(),
                Mock.Of<IEnvironmentService>(),
                pathServiceMock.Object,
                fileSystemServiceMock.Object);

            Assert.Throws<DirectoryNotFoundException>(() => service.ConfigureEnvironmentPaths());
        }

        [Fact]
        public void ConfigureEnvironmentPaths_ShouldCallConfigurePythonEnvironment_WhenValid()
        {
            var pathServiceMock = new Mock<IPathService>();
            pathServiceMock.Setup(p => p.GetPythonPath()).Returns("pythonHome");
            pathServiceMock.Setup(p => p.GetSitePackagesPath("pythonHome")).Returns("sitePackages");
            pathServiceMock.Setup(p => p.GetPythonDllPath()).Returns("pythonDll");

            var fileSystemServiceMock = new Mock<IFileSystemService>();
            fileSystemServiceMock.Setup(f => f.IsValidDirectory("pythonHome")).Returns(true);

            var envServiceMock = new Mock<IEnvironmentService>();

            var service = new PythonService(
                Mock.Of<IPythonRuntime>(),
                envServiceMock.Object,
                pathServiceMock.Object,
                fileSystemServiceMock.Object);

            service.ConfigureEnvironmentPaths();

            envServiceMock.Verify(e => e.ConfigurePythonEnvironment("pythonHome", "sitePackages", "pythonDll"), Times.Once);
        }

        [Fact]
        public void IsPythonEnvironmentInstalled_ShouldReturnFalse_WhenDllNotFound()
        {
            var pathServiceMock = new Mock<IPathService>();
            pathServiceMock.Setup(p => p.GetPythonDllPath()).Returns("dllPath");

            var fileSystemServiceMock = new Mock<IFileSystemService>();
            fileSystemServiceMock.Setup(f => f.IsFileExist("dllPath")).Returns(false);

            var service = new PythonService(
                Mock.Of<IPythonRuntime>(),
                Mock.Of<IEnvironmentService>(),
                pathServiceMock.Object,
                fileSystemServiceMock.Object);

            var result = service.IsPythonEnvironmentInstalled();

            Assert.False(result);
        }

        [Fact]
        public void IsPythonEnvironmentInstalled_ShouldReturnFalse_WhenPythonHomeNotFound()
        {
            var pathServiceMock = new Mock<IPathService>();
            pathServiceMock.Setup(p => p.GetPythonDllPath()).Returns("dllPath");
            pathServiceMock.Setup(p => p.GetPythonPath()).Returns("pythonHome");

            var fileSystemServiceMock = new Mock<IFileSystemService>();
            fileSystemServiceMock.Setup(f => f.IsFileExist("dllPath")).Returns(true);
            fileSystemServiceMock.Setup(f => f.IsValidDirectory("pythonHome")).Returns(false);

            var service = new PythonService(
                Mock.Of<IPythonRuntime>(),
                Mock.Of<IEnvironmentService>(),
                pathServiceMock.Object,
                fileSystemServiceMock.Object);

            var result = service.IsPythonEnvironmentInstalled();

            Assert.False(result);
        }

        [Fact]
        public void IsPythonEnvironmentInstalled_ShouldReturnTrue_WhenAllValid()
        {
            var pathServiceMock = new Mock<IPathService>();
            pathServiceMock.Setup(p => p.GetPythonDllPath()).Returns("dllPath");
            pathServiceMock.Setup(p => p.GetPythonPath()).Returns("pythonHome");

            var fileSystemServiceMock = new Mock<IFileSystemService>();
            fileSystemServiceMock.Setup(f => f.IsFileExist("dllPath")).Returns(true);
            fileSystemServiceMock.Setup(f => f.IsValidDirectory("pythonHome")).Returns(true);

            var service = new PythonService(
                Mock.Of<IPythonRuntime>(),
                Mock.Of<IEnvironmentService>(),
                pathServiceMock.Object,
                fileSystemServiceMock.Object);

            var result = service.IsPythonEnvironmentInstalled();

            Assert.True(result);
        }

        [Fact]
        public void IsVirtualEnvCreated_ShouldReturnTrue_WhenDirectoryExists()
        {
            var pathServiceMock = new Mock<IPathService>();
            pathServiceMock.Setup(p => p.GetVenvPath()).Returns("venvPath");

            var fileSystemServiceMock = new Mock<IFileSystemService>();
            fileSystemServiceMock.Setup(f => f.IsValidDirectory("venvPath")).Returns(true);

            var service = new PythonService(
                Mock.Of<IPythonRuntime>(),
                Mock.Of<IEnvironmentService>(),
                pathServiceMock.Object,
                fileSystemServiceMock.Object);

            Assert.True(service.IsVirtualEnvCreated());
        }

        [Fact]
        public void IsVirtualEnvCreated_ShouldReturnFalse_WhenDirectoryDoesNotExist()
        {
            var pathServiceMock = new Mock<IPathService>();
            pathServiceMock.Setup(p => p.GetVenvPath()).Returns("venvPath");

            var fileSystemServiceMock = new Mock<IFileSystemService>();
            fileSystemServiceMock.Setup(f => f.IsValidDirectory("venvPath")).Returns(false);

            var service = new PythonService(
                Mock.Of<IPythonRuntime>(),
                Mock.Of<IEnvironmentService>(),
                pathServiceMock.Object,
                fileSystemServiceMock.Object);

            Assert.False(service.IsVirtualEnvCreated());
        }

        [Fact]
        public void ActivateVirtualEnv_ShouldCallConfigurePythonVirtualEnvironment()
        {
            var pathServiceMock = new Mock<IPathService>();
            pathServiceMock.Setup(p => p.GetVenvPath()).Returns("venvPath");
            pathServiceMock.Setup(p => p.GetSitePackagesPath("venvPath")).Returns("sitePackages");

            var envServiceMock = new Mock<IEnvironmentService>();

            var service = new PythonService(
                Mock.Of<IPythonRuntime>(),
                envServiceMock.Object,
                pathServiceMock.Object,
                Mock.Of<IFileSystemService>());

            service.ActivateVirtualEnv();

            envServiceMock.Verify(e => e.ConfigurePythonVirtualEnvironment("sitePackages", true), Times.Once);
        }
    }
}
