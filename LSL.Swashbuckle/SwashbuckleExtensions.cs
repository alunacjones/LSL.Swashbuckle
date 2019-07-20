using System.Diagnostics;
using System.Reflection;
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
        public static SwaggerUiConfig DocumentTitleFromCurrentAssembly(this SwaggerUiConfig source) 
        {
            source.DocumentTitle($"Swagger UI - {GetCallingAssembly().GetName().Name}");
            return source;
        }

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