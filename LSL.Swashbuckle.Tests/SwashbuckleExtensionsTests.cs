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
            ib.GetType().GetField("_description", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(ib).Should().Be("AssemblyVersion: 1.0.0.0");            
        }
    }
}