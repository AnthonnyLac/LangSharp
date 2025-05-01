using LangSharp.Core.Models;

namespace LangSharp.UnitTests.Core.Models
{
    public class QueryScriptModelTests
    {
        [Fact]
        public void ProcessMethod_ShouldInvokeMethodWithCorrectArgumentsAndReturnOutput()
        {
            // Arrange
            var name = "TestQueryScript";
            var moduleName = "TestModule";
            var functionName = "TestFunction";
            var argsFunction = new object[] { "test-api-key", "test-query", "test-model", "test-db-uri" };

            var scriptModel = new QueryScriptModel(name, moduleName, functionName, argsFunction);

            Func<string, string, string, string, dynamic> mockMethod = (apiKey, query, model, dbUri) =>
            {
                Assert.Equal("test-api-key", apiKey);
                Assert.Equal("test-query", query);
                Assert.Equal("test-model", model);
                Assert.Equal("test-db-uri", dbUri);
                return new { output = "Query Result" };
            };

            // Act
            var result = scriptModel.ProcessMethod(mockMethod);

            // Assert
            Assert.Equal("Query Result", result.output);
        }

        [Fact]
        public void ProcessMethod_ShouldReturnRawResult_WhenOutputKeyIsMissing()
        {
            // Arrange
            var name = "TestQueryScript";
            var moduleName = "TestModule";
            var functionName = "TestFunction";
            var argsFunction = new object[] { "test-api-key", "test-query", "test-model", "test-db-uri" };

            var scriptModel = new QueryScriptModel(name, moduleName, functionName, argsFunction);

            Func<string, string, string, string, dynamic> mockMethod = (apiKey, query, model, dbUri) =>
            {
                Assert.Equal("test-api-key", apiKey);
                Assert.Equal("test-query", query);
                Assert.Equal("test-model", model);
                Assert.Equal("test-db-uri", dbUri);
                return new { otherKey = "Other Result" };
            };

            // Act
            var result = scriptModel.ProcessMethod(mockMethod);

            // Assert
            Assert.Equal(new { otherKey = "Other Result" }, result);
        }

        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var name = "TestQueryScript";
            var moduleName = "TestModule";
            var functionName = "TestFunction";
            var argsFunction = new object[] { "test-api-key", "test-query", "test-model", "test-db-uri" };

            // Act
            var scriptModel = new QueryScriptModel(name, moduleName, functionName, argsFunction);

            // Assert
            Assert.Equal(name, scriptModel.Name);
            Assert.Equal(moduleName, scriptModel.ModuleName);
            Assert.Equal(functionName, scriptModel.FunctionName);
            Assert.Equal(argsFunction, scriptModel.ArgsFunction);
        }
    }
}
