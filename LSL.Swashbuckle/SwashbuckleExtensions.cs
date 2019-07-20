using System.Diagnostics;
using Swashbuckle.Application;

namespace LSL.Swashbuckle
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class SwashbuckleExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns>The source</returns>
        public static InfoBuilder AddAssemblyVersionDescription(this InfoBuilder source)
        {
            var stackTrace = new StackTrace();
            var version = stackTrace
                .GetFrame(1)
                .GetMethod()
                .DeclaringType
                .Assembly
                .GetName()
                .Version;

            return source.Description($"AssemblyVersion: {version}");
        }
    }
}