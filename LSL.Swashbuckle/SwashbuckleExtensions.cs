using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static InfoBuilder AddAssemblyVersionDescription(this InfoBuilder source)
        {            
            var version = GetCallingAssembly()
                .GetName()
                .Version;

            return source.Description($"AssemblyVersion: {version}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static SwaggerUiConfig DocumentTitleFromCurrentAssembly(this SwaggerUiConfig source) 
        {
            source.DocumentTitle($"Swagger UI - {GetCallingAssembly().GetName().Name}");
            return source;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static Assembly GetCallingAssembly()
        {
            var stackTrace = new StackTrace();

            return stackTrace
                .GetFrame(2)
                .GetMethod()
                .DeclaringType
                .Assembly;
        }
    }
}