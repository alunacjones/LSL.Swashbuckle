using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Http;
using LSL.Swashbuckle.CustomData;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="httpConfiguration"></param>
        /// <param name="configurator"></param>
        /// <returns></returns>
        public static SwaggerUiConfig EnableCustomData(this SwaggerUiConfig source, HttpConfiguration httpConfiguration, Action<CustomDataBuilder> configurator) 
        {
            var configuration = new CustomDataBuilder();
            configurator?.Invoke(configuration);
            const string routeTemplate = "lsl-swaggerui-custom-data";

            httpConfiguration.Routes.MapHttpRoute(routeTemplate, routeTemplate, null, null, new CustomDataHandler(configuration));
            source.InjectJavaScript(typeof(SwashbuckleExtensions).Assembly, "lsl-swaggerui-custom-data.js");

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