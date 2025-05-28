using LangSharp.Core.Services;
using LangSharp.Utils;

namespace LangSharp.UnitTests.Core.Services
{

    public class PathServiceTests
    {
        [Fact]
        public void GetNuggetPath_ShouldReturnNonEmptyPath()
        {
            var service = new PathService();
            var path = service.GetNuggetPath();
            Assert.False(string.IsNullOrWhiteSpace(path));
        }

        [Fact]
        public void GetPythonPath_ShouldReturnPathWithPythonVersion()
        {
            var service = new PathService();
            var path = service.GetPythonPath();
            Assert.Contains("python", path, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void GetPythonDllPath_ShouldEndWithDllVersionName()
        {
            var service = new PathService();
            var path = service.GetPythonDllPath();
            Assert.EndsWith(EnvironmentConsts.DllVersionName, path);
        }

        [Fact]
        public void GetPythonVenvPath_ShouldContainVirtualEnvironment()
        {
            var service = new PathService();
            var path = service.GetPythonVenvPath();
            Assert.Contains(EnvironmentConsts.VirtualEnvironment, path);
        }

        [Fact]
        public void GetPythonPathExecutable_ShouldReturnExecutablePath()
        {
            var service = new PathService();

            // Test standard installation
            Environment.SetEnvironmentVariable("LANGSHARP_IS_VENV", null, EnvironmentVariableTarget.Process);
            var exePath = service.GetPythonPathExecutable();
            Assert.EndsWith("python.exe", exePath);

            // Test venv
            Environment.SetEnvironmentVariable("LANGSHARP_IS_VENV", "true", EnvironmentVariableTarget.Process);
            var venvExePath = service.GetPythonPathExecutable();
            Assert.EndsWith("python.exe", venvExePath);
        }

        [Fact]
        public void GetScriptsPath_ShouldReturnPathWithScriptName()
        {
            var service = new PathService();
            var scriptName = "test_script.py";
            var path = service.GetScriptsPath(scriptName);
            Assert.EndsWith(scriptName, path);
        }

        [Fact]
        public void GetScriptsPathByPackageDir_ShouldReturnPathWithScriptName()
        {
            var service = new PathService();
            var scriptName = "test_script.py";
            var path = service.GetScriptsPathByPackageDir(scriptName);
            Assert.EndsWith(scriptName, path);
        }

        [Fact]
        public void GetSitePackagesPath_WithBasePath_ShouldReturnSitePackages()
        {
            var service = new PathService();
            var basePath = "/usr/lib/python3.11";
            var path = service.GetSitePackagesPath(basePath);
            Assert.Contains("site-packages", path);
        }

        [Fact]
        public void GetSitePackagesPath_ShouldReturnEmpty_WhenPythonHomeNotSet()
        {
            var service = new PathService();
            Environment.SetEnvironmentVariable("PYTHONHOME", null, EnvironmentVariableTarget.Process);
            var path = service.GetSitePackagesPath();
            Assert.Equal(string.Empty, path);
        }

        [Fact]
        public void GetSitePackagesPath_ShouldReturnVenvPath_WhenIsVenv()
        {
            var service = new PathService();
            Environment.SetEnvironmentVariable("PYTHONHOME", "C:\\Python", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("LANGSHARP_IS_VENV", "true", EnvironmentVariableTarget.Process);
            var path = service.GetSitePackagesPath();
            Assert.Contains(EnvironmentConsts.VirtualEnvironment, path);
        }

        [Fact]
        public void GetVenvPath_ShouldContainVirtualEnvironment()
        {
            var service = new PathService();
            var path = service.GetVenvPath();
            Assert.Contains(EnvironmentConsts.VirtualEnvironment, path);
        }

        [Fact]
        public void GetDirectoryName_ShouldReturnDirectoryName()
        {
            var service = new PathService();
            var filePath = "/some/path/to/file.txt";
            var dir = service.GetDirectoryName(filePath);
            Assert.Equal("/some/path/to", dir.Replace('\\', '/'));
        }

        [Fact]
        public void GetDirectoryName_ShouldReturnEmpty_WhenNull()
        {
            var service = new PathService();
            var dir = service.GetDirectoryName(null);
            Assert.Equal(string.Empty, dir);
        }

        [Fact]
        public void GetEmbeddedScriptsPath_ShouldReturnPathWithScriptName()
        {
            var service = new PathService();
            var scriptName = "llm.py";
            var path = service.GetEmbeddedScriptsPath(scriptName);
            Assert.EndsWith(scriptName, path);
        }
    }
}
