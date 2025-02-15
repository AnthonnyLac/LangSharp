using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSharp.Out.Utils
{
    /// <summary>
    /// Classe utilitária para manipulação de variáveis de ambiente.
    /// </summary>
    public static class EnvironmentUtils
    {
        public static bool IsPythonInstalled()
        {
            return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PYTHONHOME"));
        }
    }
}
