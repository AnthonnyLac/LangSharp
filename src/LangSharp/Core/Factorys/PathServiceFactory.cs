using LangSharp.Core.Interfaces.Services;
using LangSharp.Core.Services;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace LangSharp.Core.Factorys
{
    [ExcludeFromCodeCoverage]
    public static class PathServiceFactory
    {
        public static IPathService CreateForCurrentEnvironment()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return new PathService();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return new PathLinuxService();

            throw new NotSupportedException("The current platform is not supported.");
        }
    }
}
