using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Dictionary
{
    public class ManualDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        const int BucketLength = 5;
        const int ElementLength = 5;
        private readonly int[] buckets;
        private readonly Element<TKey, TValue>[] elements;
        private int freeIndex;

        public ManualDictionary()
        {
            buckets = new int[BucketLength];
            freeIndex = -1;
            Array.Fill(buckets, freeIndex);
            elements = new Element<TKey, TValue>[ElementLength];
        }

        public ICollection<TKey> Keys
        {
            get
            {
                var keys = new List<TKey>();
                foreach (var element in this)
                {
                    keys.Add(element.Key);
                }

                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var values = new List<TValue>();
                foreach (var element in this)
                {
                    values.Add(element.Value);
                }

                return values;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; }

        public TValue this[TKey key]
        {
           get
            {
                int current = GetElementPosition(key);
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key), "Key is null");
                }

                if (current == -1)
                {
                    throw new KeyNotFoundException("Key {key} is not in Dictionary");
                }

                return elements[current].Value;
            }

           set
                {
                int current = GetElementPosition(key);
                if (current != -1)
                {
                    elements[current].Value = value;
                    return;
                }

                Add(key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException("Dictionary is readonly");
            }

            if (ContainsKey(key))
            {
                throw new ArgumentException("Key {key} is already taken");
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key is null");
            }

            int elementIndex = GetFreeIndex();
            int bucketIndex = GetHash(key);
            elements[elementIndex] = new Element<TKey, TValue>(key, value, buckets[bucketIndex]);

            buckets[bucketIndex] = elementIndex;
            Count++;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            Array.Fill(buckets, -1);
            Count = 0;
            freeIndex = -1;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return FindElement(item.Key, item.Value) != -1;
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key is null");
            }

            return GetElementPosition(key) != -1;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Array is null");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Array index can't be smaller than zero");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("The number of elements in dictionary is greater than the available space from arrayIndex to the end of the destination array ");
            }

            var enumerator = GetEnumerator();
            for (int i = arrayIndex; i < Count + arrayIndex; i++)
            {
                enumerator.MoveNext();
                array[i] = enumerator.Current;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < BucketLength; i++)
            {
                for (int j = buckets[i]; j != -1; j = elements[j].Next)
                {
                    yield return GetKeyValuePair(elements[j]);
                }
            }
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "key is null");
            }

            int current = GetElementPosition(key);

            if (current == -1)
            {
                return false;
            }

            int previous = GetPreviousPosition(key);

            if (previous == -1)
            {
                buckets[GetHash(key)] = elements[current].Next;
            }
            else
            {
                elements[previous].Next = elements[current].Next;
            }

            Count--;
            elements[current].Next = freeIndex;
            freeIndex = current;
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (FindElement(item.Key, item.Value) == -1)
            {
                return false;
            }

            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "key is null");
            }

            if (GetElementPosition(key) != -1)
            {
                value = elements[GetElementPosition(key)].Value;
                return true;
            }

            value = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private KeyValuePair<TKey, TValue> GetKeyValuePair(Element<TKey, TValue> element)
        {
            return KeyValuePair.Create(element.Key, element.Value);
        }

        private int GetElementPosition(TKey key)
        {
            for (int i = buckets[GetHash(key)]; i != -1; i = elements[i].Next)
            {
                if (elements[i].Key.Equals(key))
                {
                    return i;
                }
            }

            return -1;
        }

        private int FindElement(TKey key, TValue value)
        {
            int position = GetElementPosition(key);
            if (position != -1 && elements[position].Value.Equals(value))
            {
                return position;
            }

            return -1;
        }

        private int GetFreeIndex()
        {
            if (freeIndex != -1)
            {
                var aux = freeIndex;
                freeIndex = elements[freeIndex].Next;
                return aux;
            }

            return Count;
        }

        private int GetHash(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % buckets.Length;
        }

        private int GetPreviousPosition(TKey key)
        {
            int i = buckets[GetHash(key)];
            int previous = -1;
            while (!elements[i].Key.Equals(key))
            {
                previous = i;
                i = elements[i].Next;
            }

            return previous;
        }
    }
}
