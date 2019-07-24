using System;
using System.Collections.Generic;
using System.Linq;

namespace LSL.Swashbuckle.CustomData
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomDataBuilder
    {
        internal IList<IDictionary<string, string>> _dictionaries = new List<IDictionary<string, string>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public CustomDataBuilder AddDictionary(IDictionary<string, string> dictionary) 
        {
            _dictionaries.Add(dictionary);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> BuildDictionary()
        {
            return _dictionaries
                .Aggregate(new Dictionary<string, string>(), (result, dictionary) => {
                    foreach (var kvp in dictionary) {
                        result[kvp.Key] = kvp.Value;
                    }
                    return result;
                });
        }
    }
}