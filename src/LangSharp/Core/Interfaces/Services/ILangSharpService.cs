namespace LangSharp.Core.Interfaces.Services
{
    public interface ILangSharpService
    {
        Task<object> CallAIChatAsync(string prompt);
        Task<object> ExecuteDatabaseQueryAsync(string query);
    }
}
