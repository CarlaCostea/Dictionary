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
            Assert.Equal(new KeyValuePair<int, string>(10, "c"), enumerator.Current);
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

        [Fact]
        public void AddNewElementsInDictionaryAndTestValuesProperty()
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
            Assert.Equal(new[] { "c", "a", "b"}, dictionary.Values);
        }

        [Fact]
        public void AddNewElementsInDictionaryAndTestKeysProperty()
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
            Assert.Equal(new[] { 10, 1, 2 }, dictionary.Keys);
        }

        [Fact]
        public void TryingToAddTheSameKeyShouldTrowAnError()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            Assert.Throws<ArgumentException>(() => dictionary.Add(2, "b"));
        }

        [Fact]
        public void TryingToAddTheSameValueWithDiferentKeyShouldBeOk()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
        }

        [Fact]
        public void TestKeysPropertyForEmptyDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();

            Assert.Equal(Array.Empty<int>(), dictionary.Keys);
        }

        [Fact]
        public void TestValuesPropertyForEmptyDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();

            Assert.Equal(Array.Empty<string>(), dictionary.Values);
        }
    }
}
