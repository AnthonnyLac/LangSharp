using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSharp.Out.Handlers
{
    /// <summary>
    /// Interface para os handlers da cadeia de responsabilidade.
    /// </summary>
    public interface IHandler
    {
        void SetNext(IHandler nextHandler);
        string Handle(string command);
    }
}
