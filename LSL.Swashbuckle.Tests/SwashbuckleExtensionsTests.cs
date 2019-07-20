using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using Swashbuckle.Application;

namespace LSL.Swashbuckle.Tests
{
    public class SwashbuckleExtensionsTests
    {
        [Test]
        public void ItShouldSetupTheAssemblyVersionDescription()
        {
            var ib = new InfoBuilder("1", "title").AddAssemblyVersionDescription();
            var expectedVersion = GetType().Assembly.GetName().Version;
            
            ib.GetType().GetField("_description", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(ib).Should().Be($"AssemblyVersion: {expectedVersion}");            
        }
    }
}