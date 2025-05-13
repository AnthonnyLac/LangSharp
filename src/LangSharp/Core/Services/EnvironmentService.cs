using LangSharp.Core.Interfaces.Services;

namespace LangSharp.Core.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        public void ConfigurePythonEnvironment(string pythonHome, string sitePackagesPath, string pythonDllPath)
        {
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDllPath, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", pythonHome, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", sitePackagesPath, EnvironmentVariableTarget.Process);
        }

        public void ConfigurePythonEnvironment(string pythonHome, string sitePackagesPath)
        {
            Environment.SetEnvironmentVariable("PYTHONHOME", pythonHome, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", sitePackagesPath, EnvironmentVariableTarget.Process);

        }
    }
}
