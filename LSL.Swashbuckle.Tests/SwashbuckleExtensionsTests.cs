using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using Swashbuckle.Application;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using LSL.Swashbuckle.CustomData;

namespace LSL.Swashbuckle.Tests
{
    public class SwashbuckleExtensionsTests
    {
        [Test]
        public void WhenCallingAddAssemblyVersionDescriptionItShouldSetupTheAssemblyVersionDescription()
        {
            var ib = new InfoBuilder("1", "title").AddAssemblyVersionDescription();
            var expectedVersion = GetType().Assembly.GetName().Version;

            ib.GetType().GetField("_description", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(ib).Should().Be($"AssemblyVersion: {expectedVersion}");            
        }

        [Test]
        public void WhenCallingDocumentTitleFromCurrentAssemblyItShouldSetupCorrectDocumentTitle()
        {
            var sc = new SwaggerUiConfig(new string[0], _ => "");
            sc.DocumentTitleFromCurrentAssembly();
            var templateParams = (Dictionary<string, string>)sc.GetType().GetField("_templateParams", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sc);
            templateParams["%(DocumentTitle)"].Should().Be("Swagger UI - LSL.Swashbuckle.Tests");
        }

        [Test]
        public void WhenCallingEnableCustomDataItShouldSetupTheExpectedRouteAndHandler()
        {
            var httpConfiguration = new HttpConfiguration();
            var sc = new SwaggerUiConfig(new string[0], _ => "");
            sc.EnableCustomData(httpConfiguration, null);
            
            httpConfiguration.Routes.Count.Should().Be(1);
            var route = httpConfiguration.Routes[0];
            route.RouteTemplate.Should().Be("lsl-swaggerui-custom-data");
            route.Handler.Should().BeOfType<CustomDataHandler>();
        }
    }
}