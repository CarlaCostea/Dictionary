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

        [Fact]
        public void TestGetValueFromIndex()
        {
            var dictionary = new ManualDictionary<int, string>
            {
                { 3, "correctAnswer" },
            };

            Assert.Equal("correctAnswer", dictionary[3]);
        }

        [Fact]
        public void GetValueFromIndexShouldThrowAnErrorIfKeyIsNull()
        {
            var dictionary = new ManualDictionary<string, string>
            {
                { "yes", "correctAnswer" },
            };

            Assert.Throws<ArgumentNullException>(() => dictionary[null]);
        }

        [Fact]
        public void GetValueFromIndexShouldThrowAnErrorIfKeyIsNotFound()
        {
            var dictionary = new ManualDictionary<int, string>
            {
                { 3, "correctAnswer" },
            };

            Assert.Throws<KeyNotFoundException>(() => dictionary[2]);
        }

        [Fact]
        public void TestSetValueAtIndex()
        {
            var dictionary = new ManualDictionary<int, string>
            {
                { 3, "correctAnswer" },
            };

            dictionary[3] = "wrongAnswer";
            Assert.False(dictionary[3] == "correctAnswer");
            Assert.Equal("wrongAnswer", dictionary[3]);
        }

        [Fact]
        public void TestClear()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            dictionary.Clear();
            Assert.Empty(dictionary);
        }

        [Fact]
        public void ContainsKeyShouldReturnTrueIfKeyIsInDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            Assert.True(dictionary.ContainsKey(2));
        }

        [Fact]
        public void ContainsKeyShouldReturnFalseIfKeyIsNotInDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            dictionary.Clear();
            Assert.False(dictionary.ContainsKey(2));
        }

        [Fact]
        public void IfKeyIsNullContainsKeyShouldThrowAmError()
        {
            var dictionary = new ManualDictionary<string, string>();
            dictionary.Add("1", "a");
            dictionary.Add("2", "b");

            Assert.Throws<ArgumentNullException>(() => dictionary.ContainsKey(null));
        }

        [Fact]
        public void ContainsKeyValuePairShouldReturnTrueIfKeyValuePairIsInDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            Assert.True(dictionary.Contains(new KeyValuePair<int, string>(1, "a")));
            Assert.True(dictionary.Contains(new KeyValuePair<int, string>(10, "c")));
        }

        [Fact]
        public void ContainsKeyValuePairShouldReturnFalseIfKeyValuePairIsNotInDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            dictionary.Clear();
            Assert.False(dictionary.Contains(new KeyValuePair<int, string>(3, "d")));
            Assert.False(dictionary.Contains(new KeyValuePair<int, string>(4, "f")));
        }

        [Fact]
        public void ContainsKeyValuePairShouldReturnFalseIfKeyIsInDictionaryButValueIsDifferent()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            dictionary.Clear();
            Assert.False(dictionary.Contains(new KeyValuePair<int, string>(2, "d")));
            Assert.False(dictionary.Contains(new KeyValuePair<int, string>(1, "f")));
        }

        [Fact]
        public void TryGetValueShouldReturnTrueIfKeyIsInDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            Assert.True(dictionary.TryGetValue(2, out string value));
        }

        [Fact]
        public void TryGetValueShouldReturnFalseIfKeyIsNotInDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            Assert.False(dictionary.TryGetValue(3, out string value));
        }

        [Fact]
        public void TryGetValueShouldThrowErrorIfKeyIsNull()
        {
            var dictionary = new ManualDictionary<string, string>();
            dictionary.Add("1", "a");
            dictionary.Add("2", "b");

            Assert.Throws<ArgumentNullException>(() => dictionary.TryGetValue(null, out string value));
        }

        [Fact]
        public void CopyToArrayWithAvailableSpace()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "b");

            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[dictionary.Count];
            dictionary.CopyTo(array, 0);

            Assert.Equal(new KeyValuePair<int, string>(11, "c"), array[1]);
            Assert.Equal(new KeyValuePair<int, string>(2, "b"), array[4]);
        }

        [Fact]
        public void TryingtoCopyToArrayWhenCountOfDictionaryIsGreaterThanTheAvailableSpaceFromIndexToArrayLengthShouldThrowAnError()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");

            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[dictionary.Count];

            Assert.Throws<ArgumentException>(() => dictionary.CopyTo(array, 1));
        }

        [Fact]
        public void TryingtoCopyDictionaryToAnEmptyArrayShouldThrowAnError()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");

            Assert.Throws<ArgumentNullException>(() => dictionary.CopyTo(null, 1));
        }

        [Fact]
        public void RemoveKeyShouldReturnTrueIfKeyIsDeletedFromDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            Assert.True(dictionary.ContainsKey(2));
            Assert.True(dictionary.Remove(2));
            Assert.False(dictionary.ContainsKey(2));
        }

        [Fact]
        public void RemoveKeyShouldReturnTrueIfKeysInConflictAreDeletedFromDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            Assert.True(dictionary.ContainsKey(2));
            Assert.True(dictionary.ContainsKey(12));
            Assert.True(dictionary.Remove(2));
            Assert.False(dictionary.ContainsKey(2));
            Assert.True(dictionary.Remove(12));
            Assert.False(dictionary.ContainsKey(12));
        }

        [Fact]
        public void RemoveKeyShouldReturnFalseIfKeyIsNotInDictionary()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            Assert.True(dictionary.ContainsKey(2));
            Assert.False(dictionary.Remove(3));
            Assert.False(dictionary.Remove(13));
            Assert.False(dictionary.Remove(33));
        }

        [Fact]
        public void RemoveKeyShoulThrowAnErrorIfKeyIsNull()
        {
            var dictionary = new ManualDictionary<string, string>()
            {
                { "first", "element" },
                { "second", "element" },
            };

            Assert.False(dictionary.Remove("third"));
            Assert.Throws<ArgumentNullException>(() => dictionary.Remove(null));
        }

        [Fact]
        public void RemoveKeyValuePairShoulReturnTrueIfPairIsInDictionary()
        {
            var dictionary = new ManualDictionary<string, string>()
            {
                { "first", "element" },
                { "second", "element" },
            };

            var firstPair = new KeyValuePair<string, string>("first", "element");
            var secondPair = new KeyValuePair<string, string>("second", "element");
            Assert.True(dictionary.Remove(firstPair));
            Assert.True(dictionary.Remove(secondPair));
            Assert.Empty(dictionary);
        }

        [Fact]
        public void RemoveKeyValuePairShoulReturnFalseIfKeyIsInDictionaryWithDifferentValue()
        {
            var dictionary = new ManualDictionary<string, string>()
            {
                { "first", "element" },
                { "second", "elements" },
            };

            var firstPair = new KeyValuePair<string, string>("first", "element");
            var secondPair = new KeyValuePair<string, string>("second", "element");
            Assert.True(dictionary.Remove(firstPair));
            Assert.False(dictionary.Remove(secondPair));
            Assert.NotEmpty(dictionary);
        }

        [Fact]
        public void TestAddingElementAfterRemoveKeyValuePairAtFreeIndex()
        {
            var dictionary = new ManualDictionary<int, string>();
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(11, "c");
            dictionary.Add(12, "c");
            Assert.True(dictionary.ContainsKey(10));
            Assert.True(dictionary.Remove(10));
            Assert.False(dictionary.Remove(13));
            Assert.False(dictionary.Remove(33));
            dictionary.Add(3, "b");

            var enumerator = dictionary.GetEnumerator();

            Assert.True(enumerator.MoveNext());
            Assert.Equal(11, enumerator.Current.Key);
            Assert.Equal("c", enumerator.Current.Value);

            Assert.True(enumerator.MoveNext());
            Assert.Equal(1, enumerator.Current.Key);
            Assert.Equal("a", enumerator.Current.Value);

            Assert.True(enumerator.MoveNext());
            Assert.Equal(12, enumerator.Current.Key);
            Assert.Equal("c", enumerator.Current.Value);

            Assert.True(enumerator.MoveNext());
            Assert.Equal(2, enumerator.Current.Key);
            Assert.Equal("b", enumerator.Current.Value);

            Assert.True(enumerator.MoveNext());
            Assert.Equal(3, enumerator.Current.Key);
            Assert.Equal("b", enumerator.Current.Value);

            Assert.False(enumerator.MoveNext());
            Assert.Equal(new[] { 11, 1, 12, 2, 3 }, dictionary.Keys);
        }
    }
}
