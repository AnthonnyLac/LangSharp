namespace LangSharp.Core.Interfaces.Services
{
    public interface ILangSharpService
    {
        object CallAIChat(string prompt);
        object ExecuteDatabaseQuery(string query);
    }
}
