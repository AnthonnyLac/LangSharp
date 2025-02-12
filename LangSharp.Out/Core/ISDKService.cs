using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSharp.Out.Core
{
    /// <summary>
    /// Interface principal do SDK, definindo os métodos disponíveis para os usuários do SDK.
    /// </summary>
    public interface ISDKService
    {
        /// <summary>
        /// Executa um comando no ambiente Python e retorna o resultado.
        /// </summary>
        /// <param name="command">Comando a ser executado.</param>
        /// <returns>Resultado da execução.</returns>
        string ExecutePythonCommand(string command);
    }
}
