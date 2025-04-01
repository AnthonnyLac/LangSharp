namespace LangSharp.Core.Interfaces.Handlers.@base
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }
}
