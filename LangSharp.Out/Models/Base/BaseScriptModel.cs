namespace LangSharp.Out.Models.Base
{
    public abstract class BaseScriptModel
    {
        protected BaseScriptModel(string name, string moduleName, string functionName, object[] argsFunction)
        {
            Name = name;
            ModuleName = moduleName;
            FunctionName = functionName;
            ArgsFunction = argsFunction;
        }

        public string Name { get; private set; }
        public string ModuleName { get; private set; }
        public string FunctionName { get; private set; }
        public object[] ArgsFunction { get; private set; }

        public abstract dynamic ProcessMethod(dynamic method);
    }
}
