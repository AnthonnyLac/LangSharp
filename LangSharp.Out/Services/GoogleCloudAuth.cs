using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSharp.Out.Services
{
    /// <summary>
    /// Implementação de autenticação para o Google Cloud.
    /// </summary>
    public class GoogleCloudAuth : ICloudAuth
    {
        /// <summary>
        /// Autentica no Google Cloud utilizando um script Python.
        /// </summary>
        /// <param name="apiKey">Chave de API do Google Cloud.</param>
        /// <returns>Status da autenticação.</returns>
        public string Authenticate(string apiKey)
        {
            return PythonService.CallPythonFunction("cloud_auth", "autenticar_google", apiKey);
        }
    }
}
