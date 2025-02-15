using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSharp.Out.Services
{
    /// <summary>
    /// Interface para autenticação em serviços de nuvem.
    /// </summary>
    public interface ICloudAuth
    {
        /// <summary>
        /// Autentica no serviço de nuvem usando uma chave de API.
        /// </summary>
        /// <param name="apiKey">Chave de API do provedor.</param>
        /// <returns>Status da autenticação.</returns>
        string Authenticate(string apiKey);
    }
}
