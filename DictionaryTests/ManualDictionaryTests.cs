using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Dictionary;

namespace DictionaryTests
{
    public class ManualDictionaryTests
    {
        [Fact]
        public void AddNewElementsInDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");

            var enumerator = dictionary.GetEnumerator();

            Assert.True(enumerator.MoveNext());
            Assert.Equal(10, enumerator.Current.Key);
            Assert.Equal("c", enumerator.Current.Value);
            Assert.True(enumerator.MoveNext());
            Assert.Equal(1, enumerator.Current.Key);
            Assert.Equal("a", enumerator.Current.Value);
            Assert.True(enumerator.MoveNext());
            Assert.Equal(2, enumerator.Current.Key);
            Assert.Equal("b", enumerator.Current.Value);
            Assert.False(enumerator.MoveNext());
        }
    }
}
