namespace LangSharp.Core.Interfaces.Services
{
    public  interface IFileSystemService
    {
        bool IsValidDirectory(string? path);
        bool IsFileExist(string? path);
    }
}
