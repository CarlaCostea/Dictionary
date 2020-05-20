using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DictionaryTests
{
    public class DictionaryTests
    {
        [Fact]
        public void AddNewElementsInDictionary()
        {
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            Assert.Equal();
        }
    }
}
