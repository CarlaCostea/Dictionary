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
                foreach (var element in elements)
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
                foreach (var element in elements)
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
                return elements[GetElementPosition(key)].Value;
            }

           set
            {
                if (GetElementPosition(key) == -1)
                {
                    Count++;
                }

                elements[GetElementPosition(key)].Value = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            int elementIndex = GetFreeIndex();
            int bucketIndex = GetHash(key);
            elements[elementIndex] = new Element<TKey, TValue>(key, value, -1);

            if (buckets[bucketIndex] != -1)
            {
                elements[elementIndex].Next = buckets[bucketIndex];
            }

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
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return FindElement(item.Key, item.Value) != -1;
        }

        public bool ContainsKey(TKey key)
        {
            return GetElementPosition(key) != -1;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
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
            return GetElementPosition(key, out int previousIndex);
        }

        private int GetElementPosition(TKey key, out int previousIndex)
        {
            previousIndex = -1;
            for (int i = buckets[GetHash(key)]; i != -1; i = elements[i].Next)
            {
                if (elements[i].Key.Equals(key))
                {
                    return i;
                }

                previousIndex = i;
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
    }
}
