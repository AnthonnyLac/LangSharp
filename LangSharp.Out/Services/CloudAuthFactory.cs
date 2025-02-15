using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSharp.Out.Services
{
    /// <summary>
    /// Factory para criar instâncias de autenticação de serviços na nuvem.
    /// </summary>
    public class CloudAuthFactory
    {
        /// <summary>
        /// Cria uma instância de autenticação para um provedor de nuvem específico.
        /// </summary>
        /// <param name="provider">Nome do provedor (ex: "google").</param>
        /// <returns>Instância de <see cref="ICloudAuth"/> correspondente ao provedor.</returns>
        /// <exception cref="Exception">Lançado se o provedor não for suportado.</exception>
        public static ICloudAuth Create(string provider)
        {
            return provider.ToLower() switch
            {
                "google" => new GoogleCloudAuth(),
                _ => throw new Exception("Provedor não suportado.")
            };
        }
    }
}
