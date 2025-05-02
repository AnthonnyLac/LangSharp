using System.Reflection;

namespace LangSharp.Utils
{
    public static class EnvironmentConsts
    {
        public const string PythonVersion = "3.11.7";
        public const string DllVersionName = "python311.dll";
        public const string VirtualEnvironment = "langsharp";

        public static string GetLangSharpAssemblyVersion()
        {
            var versionAttribute = Assembly
                .GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            // Remove build metadata (anything after '+')
            var fullVersion = versionAttribute!.InformationalVersion;
            var mainVersion = fullVersion.Split('+')[0]; // Keep only the part before '+'

            return mainVersion;
        }
    }
}
