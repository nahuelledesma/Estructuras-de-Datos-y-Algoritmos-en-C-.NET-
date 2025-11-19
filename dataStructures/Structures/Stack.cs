using System;
using System.Collections;
using System.Collections.Generic;

namespace EstructurasDeDatos_CS.DataStructures
{
    /// <summary>
    /// Pila genérica simple basada en lista enlazada (LIFO).
    /// Provee Push, Pop, Peek, Count, IsEmpty() y enumeración para foreach.
    /// </summary>
    public class MyStack<T> : IEnumerable<T>
    {
        private class Node
        {
            public T Value;
            public Node? Next;
            public Node(T value) { Value = value; Next = null; }
        }

        private Node? _top;
        private int _count;

        public MyStack() { _top = null; _count = 0; }

        public int Count => _count;

        public void Push(T item)
        {
            var node = new Node(item);
            node.Next = _top;
            _top = node;
            _count++;
        }

        public T Pop()
        {
            if (_top is null)
                throw new InvalidOperationException("La pila está vacía.");

            var value = _top.Value;
            _top = _top.Next;
            _count--;
            return value;
        }

        public bool TryPop(out T? value)
        {
            if (_top is null)
            {
                value = default;
                return false;
            }
            value = Pop();
            return true;
        }

        public T Peek()
        {
            if (_top is null)
                throw new InvalidOperationException("La pila está vacía.");
            return _top.Value;
        }

        public bool IsEmpty() => _count == 0;

        public void Clear()
        {
            while (_top is not null)
            {
                var next = _top.Next;
                _top.Next = null!;
                _top = next;
            }
            _count = 0;
        }

        public IEnumerator<T> GetEnumerator() => new Enumerator(_top);
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
