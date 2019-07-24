using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LSL.Swashbuckle.CustomData;
using NUnit.Framework;

namespace LSL.Swashbuckle.Tests.CustomData
{
    public class CustomDataBuilderTests
    {
        private CustomDataBuilder BuildSut()
        {
            return new CustomDataBuilder();
        }

        [TestCase(new[] { "als=123;ewq=12" }, "als=123;ewq=12")]
        [TestCase(new[] { "als=123;ewq=12", "ewq=123" }, "als=123;ewq=123")]
        public void GivenSomeDictionariesItShouldBuildTheExpectedDictionary(IEnumerable<string> dictionaries, string expectedResult) 
        {
            IDictionary<string, string> BuildDictionary(string dictionary) => dictionary.Split(';')
                    .Select(item =>
                    {
                        var keyAndValue = item.Split('=');
                        return new KeyValuePair<string, string>(keyAndValue[0].Trim(), keyAndValue[1].Trim());
                    })
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var sut = BuildSut();

            dictionaries
                .Select(BuildDictionary)
                .Aggregate(sut, (_, dictionary) => _.AddDictionary(dictionary));

            sut.BuildDictionary().Should().BeEquivalentTo(BuildDictionary(expectedResult));
        }
    }
}