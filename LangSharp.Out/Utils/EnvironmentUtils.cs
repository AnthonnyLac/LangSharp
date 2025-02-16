using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = "--version",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    if (process == null) return false;

                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    return !string.IsNullOrEmpty(output) || !string.IsNullOrEmpty(error);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
