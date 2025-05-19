using LangSharp.Core.Interfaces.Services;

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
    }
}
