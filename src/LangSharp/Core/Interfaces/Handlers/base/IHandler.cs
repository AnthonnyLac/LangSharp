namespace LangSharp.Core.Interfaces.Handlers.Base
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }
}
