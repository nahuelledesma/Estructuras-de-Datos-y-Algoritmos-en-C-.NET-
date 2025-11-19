using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Structures
{
    /// <summary>
    /// Tabla hash genérica simple usando chaining (listas enlazadas por cubeta).
    /// Soporta Add (upsert), TryGetValue, ContainsKey, Remove, Clear y enumeración.
    /// </summary>
    internal class HashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private class Node
        {
            public TKey Key;
            public TValue Value;
            public Node? Next;
            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
                Next = null;
            }
        }

        private Node?[] _buckets;
        private int _count;
        private int _threshold;
        private const float LoadFactor = 0.75f;
        private readonly IEqualityComparer<TKey> _comparer;

        public HashTable(int capacity = 16, IEqualityComparer<TKey>? comparer = null)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            _buckets = new Node?[capacity];
            _comparer = comparer ?? EqualityComparer<TKey>.Default;
            _threshold = (int)(capacity * LoadFactor);
        }

        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public void Add(TKey key, TValue value)
        {
            if (key is null) throw new ArgumentNullException(nameof(key));
            EnsureCapacityIfNeeded();

            int idx = GetBucketIndex(key);
            for (var node = _buckets[idx]; node is not null; node = node.Next)
            {
                if (_comparer.Equals(node.Key, key))
                {
                    // Upsert: reemplaza el valor existente
                    node.Value = value;
                    return;
                }
            }

            var newNode = new Node(key, value) { Next = _buckets[idx] };
            _buckets[idx] = newNode;
            _count++;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key is null) throw new ArgumentNullException(nameof(key));
            int idx = GetBucketIndex(key);
            for (var node = _buckets[idx]; node is not null; node = node.Next)
            {
                if (_comparer.Equals(node.Key, key))
                {
                    value = node.Value;
                    return true;
                }
            }
            value = default!;
            return false;
        }

        public bool ContainsKey(TKey key) => TryGetValue(key, out _);

        public bool Remove(TKey key)
        {
            if (key is null) throw new ArgumentNullException(nameof(key));
            int idx = GetBucketIndex(key);
            Node? prev = null;
            for (var node = _buckets[idx]; node is not null; prev = node, node = node.Next)
            {
                if (_comparer.Equals(node.Key, key))
                {
                    if (prev is null)
                        _buckets[idx] = node.Next;
                    else
                        prev.Next = node.Next;
                    node.Next = null;
                    _count--;
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            Array.Clear(_buckets, 0, _buckets.Length);
            _count = 0;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < _buckets.Length; i++)
            {
                for (var node = _buckets[i]; node is not null; node = node.Next)
                {
                    yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int GetBucketIndex(TKey key)
        {
            int hash = _comparer.GetHashCode(key) & 0x7FFFFFFF;
            return hash % _buckets.Length;
        }

        private void EnsureCapacityIfNeeded()
        {
            if (_count + 1 <= _threshold) return;
            Resize(_buckets.Length * 2);
        }

        private void Resize(int newSize)
        {
            var newBuckets = new Node?[newSize];
            foreach (var kv in this)
            {
                var key = kv.Key;
                var val = kv.Value;
                int idx = (_comparer.GetHashCode(key) & 0x7FFFFFFF) % newSize;
                var node = new Node(key, val) { Next = newBuckets[idx] };
                newBuckets[idx] = node;
            }
            _buckets = newBuckets;
            _threshold = (int)(newSize * LoadFactor);
        }
    }
}
