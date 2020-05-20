﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;

namespace Dictionary
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        const int BucketLength = 5;
        const int ElementLength = 5;
        private readonly int[] buckets;
        private readonly Element<TKey, TValue>[] elements;
        private int freeIndex;

        public Dictionary()
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

        public bool IsReadOnly { get; private set; }

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
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private int GetElementPosition(TKey key)
        {
            return GetElementPosition(key, out int previousIndex);
        }

        private int GetElementPosition(TKey key, out int previousIndex)
        {
            previousIndex = -1;
            for (int i = buckets[GetBucketPosition(key)]; i != -1; i = elements[i].Next)
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

        private int GetBucketPosition(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % buckets.Length;
        }
    }
}
