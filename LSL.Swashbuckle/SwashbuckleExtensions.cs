using System.Diagnostics;
using Swashbuckle.Application;

namespace LSL.Swashbuckle
{
    /// <summary>
    /// 
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
            var version = new StackTrace()
                .GetFrames()[1]
                .GetMethod()
                .GetType()
                .Assembly
                .GetName()
                .Version;

            return source.Description($"AssemblyVersion: {version}");
        }
    }
}