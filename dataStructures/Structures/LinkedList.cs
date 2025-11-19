using System;
using System.Collections;
using System.Collections.Generic;

namespace EstructurasDeDatos_CS.DataStructures
{
    /// <summary>
    /// Lista enlazada simple (singly linked list) con operaciones básicas.
    /// Soporta AddFirst, AddLast, Contains, Remove (primer ocurrencia), InsertAt, enumeración y Count.
    /// </summary>
    public class MyLinkedList<T> : IEnumerable<T>
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

        public MyLinkedList()
        {
            _head = _tail = null;
            _count = 0;
        }

        public int Count => _count;

        public void AddFirst(T value)
        {
            var node = new Node(value);
            node.Next = _head;
            _head = node;
            if (_tail is null)
                _tail = _head;
            _count++;
        }

        public void AddLast(T value)
        {
            var node = new Node(value);
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

        public bool Contains(T value)
        {
            var comparer = EqualityComparer<T>.Default;
            for (var node = _head; node is not null; node = node.Next)
            {
                if (comparer.Equals(node.Value, value))
                    return true;
            }
            return false;
        }

        public bool Remove(T value)
        {
            var comparer = EqualityComparer<T>.Default;
            Node? prev = null;
            for (var node = _head; node is not null; prev = node, node = node.Next)
            {
                if (comparer.Equals(node.Value, value))
                {
                    // Remover node
                    if (prev is null)
                    {
                        // quitar cabeza
                        _head = node.Next;
                        if (_head is null)
                            _tail = null;
                    }
                    else
                    {
                        prev.Next = node.Next;
                        if (prev.Next is null)
                            _tail = prev;
                    }
                    node.Next = null!;
                    _count--;
                    return true;
                }
            }
            return false;
        }

        public void InsertAt(int index, T value)
        {
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index), "Índice fuera de rango.");

            if (index == 0)
            {
                AddFirst(value);
                return;
            }

            if (index == _count)
            {
                AddLast(value);
                return;
            }

            Node? prev = _head;
            for (int i = 0; i < index - 1; i++)
                prev = prev!.Next;

            var node = new Node(value);
            node.Next = prev!.Next;
            prev.Next = node;
            _count++;
        }

        public void Clear()
        {
            while (_head is not null)
            {
                var next = _head.Next;
                _head.Next = null!;
                _head = next;
            }
            _tail = null;
            _count = 0;
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
            private Node? _start;
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
                else if (_current is not null)
                {
                    _current = _current.Next;
                }

                return _current is not null;
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
