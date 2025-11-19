using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// Cola genérica simple basada en una lista enlazada.
    /// Soporta operaciones O(1) Enqueue y Dequeue, enumeración y utilidades comunes.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos</typeparam>
    public class MyQueue<T> : IEnumerable<T>
    {
        private class Node
        {
            public T Value;
            public Node? Next;
            public Node(T value) { Value = value; Next = null; }
        }

        private Node? _head;
        private Node? _tail;
        private int _count;

        public MyQueue()
        {
            _head = _tail = null;
            _count = 0;
        }

        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public void Enqueue(T item)
        {
            var node = new Node(item);
            if (_tail is null)
            {
                _head = _tail = node;
            }
            else
            {
                _tail.Next = node;
                _tail = node;
            }
            _count++;
        }

        public T Dequeue()
        {
            if (_head is null)
                throw new InvalidOperationException("La cola está vacía.");

            var value = _head.Value;
            _head = _head.Next;
            if (_head is null)
                _tail = null;
            _count--;
            return value;
        }

        public bool TryDequeue(out T? value)
        {
            if (_head is null)
            {
                value = default;
                return false;
            }
            value = Dequeue();
            return true;
        }

        public T Peek()
        {
            if (_head is null)
                throw new InvalidOperationException("La cola está vacía.");
            return _head.Value;
        }

        public bool TryPeek(out T? value)
        {
            if (_head is null)
            {
                value = default;
                return false;
            }
            value = _head.Value;
            return true;
        }

        public void Clear()
        {
            // Romper referencias para ayudar al recolector
            while (_head is not null)
            {
                var next = _head.Next;
                _head.Next = null!;
                _head = next;
            }
            _tail = null;
            _count = 0;
        }

        public bool Contains(T item)
        {
            var comparer = EqualityComparer<T>.Default;
            for (var node = _head; node is not null; node = node.Next)
            {
                if (comparer.Equals(node.Value, item))
                    return true;
            }
            return false;
        }

        public T[] ToArray()
        {
            var arr = new T[_count];
            int i = 0;
            for (var node = _head; node is not null; node = node.Next)
                arr[i++] = node.Value;
            return arr;
        }

        public IEnumerator<T> GetEnumerator() => new Enumerator(_head);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private struct Enumerator : IEnumerator<T>
        {
            private Node? _current;
            private readonly Node? _start;
            private bool _started;

            public Enumerator(Node? start)
            {
                _start = start;
                _current = null;
                _started = false;
            }

            public T Current => _current!.Value;

            object IEnumerator.Current => Current!;

            public bool MoveNext()
            {
                if (!_started)
                {
                    _current = _start;
                    _started = true;
                }
                else if (_current != null)
                {
                    _current = _current.Next;
                }

                return _current != null;
            }

            public void Reset()
            {
                _current = null;
                _started = false;
            }

            public void Dispose() { }
        }
    }
}