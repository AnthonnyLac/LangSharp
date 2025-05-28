using LangSharp.Core.Interfaces.Services;
using LangSharp.Utils;

namespace LangSharp.Core.Services
{
    public class FileSystemService : IFileSystemService
    {
        public bool IsValidDirectory(string? path)
        {
            return !string.IsNullOrWhiteSpace(path) && Directory.Exists(path);
        }

        public bool IsFileExist(string? path)
        {
            return !string.IsNullOrWhiteSpace(path) && File.Exists(path);
        }

        public string? WriteEmbeddedPythonScriptToProjectRoot(string scriptName)
        {
            try
            {
                // Reads the embedded Python script from the specified resource name
                var embeddedScript = ResourceHelper.ReadEmbeddedPythonScript(scriptName);

                if (string.IsNullOrWhiteSpace(embeddedScript))
                    return null;

                // Uses the project root (bin output directory) as the destination
                var projectRoot = AppContext.BaseDirectory;
                var scriptPath = Path.Combine(projectRoot, scriptName);

                // Writes the embedded script content to the file in the project root
                File.WriteAllText(scriptPath, embeddedScript);

                // Returns the path to the created script file, or null if failed
                return scriptPath;
            }
            catch 
            {
                return null;
            }
        }
    }
}
