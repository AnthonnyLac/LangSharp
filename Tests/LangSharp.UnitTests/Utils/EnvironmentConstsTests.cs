using LangSharp.Utils;

namespace LangSharp.UnitTests.Utils
{
    public class EnvironmentConstsTests
    {
        [Fact]
        public void PythonVersion_ShouldBeCorrect()
        {
            Assert.Equal("3.11.7", EnvironmentConsts.PythonVersion);
        }

        [Fact]
        public void DllVersionName_ShouldBeCorrect()
        {
            Assert.Equal("python311.dll", EnvironmentConsts.DllVersionName);
        }

        [Fact]
        public void VirtualEnvironment_ShouldBeCorrect()
        {
            Assert.Equal("langsharp", EnvironmentConsts.VirtualEnvironment);
        }
    }
}
