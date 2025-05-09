using LangSharp.Utils;

namespace LangSharp.UnitTests.Utils
{
    public class PythonPackageTests
    {
        [Fact]
        public void PythonDotEnv_ShouldBeCorrect()
        {
            Assert.Equal("python-dotenv", PythonPackage.PythonDotEnv);
        }

        [Fact]
        public void LangChainCommunity_ShouldBeCorrect()
        {
            Assert.Equal("langchain-community", PythonPackage.LangChainCommunity);
        }

        [Fact]
        public void LangChainOpenai_ShouldBeCorrect()
        {
            Assert.Equal("langchain-openai", PythonPackage.LangChainOpenai);
        }
    }
}
