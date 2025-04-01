namespace LangSharp.Core.Interfaces.Providers
{
    public interface ICloudAIProvider
    {
        string GetResponse(string prompt);
    }
}
