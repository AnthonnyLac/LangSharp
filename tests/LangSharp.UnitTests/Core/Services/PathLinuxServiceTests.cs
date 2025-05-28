using LangSharp.Core.Services;
using LangSharp.Utils;

namespace LangSharp.UnitTests.Core.Services
{
    public class PathLinuxServiceTests
    {
        [Fact]
        public void GetNuggetPath_ShouldReturnExpectedPath()
        {
            var service = new PathLinuxService();
            var path = service.GetNuggetPath();
            Assert.Contains(".nuget", path);
            Assert.Contains("packages", path);
        }

        [Fact]
        public void GetPythonPath_ShouldReturnUsr()
        {
            var service = new PathLinuxService();
            Assert.Equal("/usr", service.GetPythonPath());
        }

        [Fact]
        public void GetPythonDllPath_ShouldReturnLibPythonPath()
        {
            var service = new PathLinuxService();
            var path = service.GetPythonDllPath();
            Assert.Contains("libpython3.11.so.1.0", path);
            Assert.StartsWith("/usr/lib", path);
        }

        [Fact]
        public void GetPythonVenvPath_ShouldReturnSameAsGetVenvPath()
        {
            var service = new PathLinuxService();
            Assert.Equal(service.GetVenvPath(), service.GetPythonVenvPath());
        }

        [Fact]
        public void GetPythonPathExecutable_ShouldReturnVenvOrSystemPath()
        {
            var service = new PathLinuxService();

            // Test system python
            Environment.SetEnvironmentVariable("LANGSHARP_IS_VENV", null, EnvironmentVariableTarget.Process);
            var sysPath = service.GetPythonPathExecutable();
            Assert.Contains(Path.Combine("bin", "python3.11"), sysPath);

            // Test venv python
            Environment.SetEnvironmentVariable("LANGSHARP_IS_VENV", "true", EnvironmentVariableTarget.Process);
            var venvPath = service.GetPythonPathExecutable();
            Assert.Contains(Path.Combine("bin", "python3.11"), venvPath);
        }

        [Fact]
        public void GetScriptsPath_ShouldReturnPathWithScriptName()
        {
            var service = new PathLinuxService();
            var scriptName = "test_script.py";
            var path = service.GetScriptsPath(scriptName);
            Assert.EndsWith(System.IO.Path.Combine("Scripts", scriptName), path);
        }

        [Fact]
        public void GetScriptsPathByPackageDir_ShouldReturnPathWithScriptName()
        {
            var service = new PathLinuxService();
            var scriptName = "test_script.py";
            var path = service.GetScriptsPathByPackageDir(scriptName);
            Assert.EndsWith(System.IO.Path.Combine("Scripts", scriptName), path);
            Assert.Contains("langsharp", path);
        }

        [Fact]
        public void GetSitePackagesPath_WithBasePath_ShouldReturnExpected()
        {
            var service = new PathLinuxService();
            var basePath = "/usr";
            var path = service.GetSitePackagesPath(basePath);
            Assert.Contains("site-packages", path);
            Assert.Contains("python3.11", path);
        }

        [Fact]
        public void GetSitePackagesPath_ShouldReturnVenvOrSystemPath()
        {
            var service = new PathLinuxService();

            // System path
            Environment.SetEnvironmentVariable("LANGSHARP_IS_VENV", null, EnvironmentVariableTarget.Process);
            var sysPath = service.GetSitePackagesPath();
            Assert.Contains("site-packages", sysPath);

            // Venv path
            Environment.SetEnvironmentVariable("LANGSHARP_IS_VENV", "true", EnvironmentVariableTarget.Process);
            var venvPath = service.GetSitePackagesPath();
            Assert.Contains("site-packages", venvPath);
        }

        [Fact]
        public void GetVenvPath_ShouldContainVirtualEnvironment()
        {
            var service = new PathLinuxService();
            var path = service.GetVenvPath();
            Assert.Contains("python", path);
            Assert.Contains(EnvironmentConsts.VirtualEnvironment, path);
        }

        [Fact]
        public void GetDirectoryName_ShouldReturnDirectoryName()
        {
            var service = new PathLinuxService();
            var filePath = "/some/path/to/file.txt";
            var dir = service.GetDirectoryName(filePath);
            Assert.Equal("/some/path/to", dir.Replace('\\', '/'));
        }

        [Fact]
        public void GetDirectoryName_ShouldReturnEmpty_WhenNull()
        {
            var service = new PathLinuxService();
            var dir = service.GetDirectoryName(null);
            Assert.Equal(string.Empty, dir);
        }

        [Fact]
        public void GetEmbeddedScriptsPath_ShouldReturnPathWithScriptName()
        {
            var service = new PathLinuxService();
            var scriptName = "llm.py";
            var path = service.GetEmbeddedScriptsPath(scriptName);
            Assert.EndsWith(scriptName, path);
        }
    }
}
