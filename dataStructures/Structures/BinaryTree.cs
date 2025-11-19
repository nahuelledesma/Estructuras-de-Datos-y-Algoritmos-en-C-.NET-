using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Structures
{
    /// <summary>
    /// Árbol binario de búsqueda (BST) genérico.
    /// Soporta Add, Contains, Remove, recorridos (in-order, pre-order, post-order), Count y utilidad TryGetMin/TryGetMax.
    /// Usa IComparer<T> o Comparer<T>.Default para comparar nodos.
    /// </summary>
    internal class BinarySearchTree<T> : IEnumerable<T>
    {
        private class Node
        {
            public T Value;
            public Node? Left;
            public Node? Right;
            public Node(T value) { Value = value; Left = Right = null; }
        }

        private Node? _root;
        private int _count;
        private readonly IComparer<T> _comparer;

        public BinarySearchTree(IComparer<T>? comparer = null)
        {
            _comparer = comparer ?? Comparer<T>.Default;
        }

        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public void Add(T value)
        {
            if (_root is null)
            {
                _root = new Node(value);
                _count = 1;
                return;
            }

            Node parent = _root;
            Node? current = _root;
            while (current is not null)
            {
                parent = current;
                int cmp = _comparer.Compare(value, current.Value);
                if (cmp < 0) current = current.Left;
                else current = current.Right;
            }

            int cmpParent = _comparer.Compare(value, parent.Value);
            if (cmpParent < 0) parent.Left = new Node(value);
            else parent.Right = new Node(value);
            _count++;
        }

        public bool Contains(T value)
        {
            var node = FindNode(value);
            return node is not null;
        }

        public bool Remove(T value)
        {
            Node? parent = null;
            var current = _root;

            // Buscar nodo a eliminar
            while (current is not null && _comparer.Compare(value, current.Value) != 0)
            {
                parent = current;
                current = _comparer.Compare(value, current.Value) < 0 ? current.Left : current.Right;
            }

            if (current is null) return false; // no encontrado

            // Caso 1: nodo con dos hijos -> encontrar sucesor (mínimo en subárbol derecho)
            if (current.Left is not null && current.Right is not null)
            {
                var succParent = current;
                var succ = current.Right;
                while (succ.Left is not null)
                {
                    succParent = succ;
                    succ = succ.Left;
                }
                // copiar valor del sucesor y eliminar sucesor
                current.Value = succ.Value;
                // ahora eliminamos 'succ' que tiene a lo más un hijo derecho
                parent = succParent;
                current = succ;
            }

            // Ahora current tiene como máximo un hijo
            Node? child = current.Left ?? current.Right;

            if (parent is null)
            {
                // eliminar la raíz
                _root = child;
            }
            else if (parent.Left == current)
            {
                parent.Left = child;
            }
            else
            {
                parent.Right = child;
            }

            _count--;
            return true;
        }

        public bool TryGetMin(out T value)
        {
            if (_root is null) { value = default!; return false; }
            var node = _root;
            while (node.Left is not null) node = node.Left;
            value = node.Value;
            return true;
        }

        public bool TryGetMax(out T value)
        {
            if (_root is null) { value = default!; return false; }
            var node = _root;
            while (node.Right is not null) node = node.Right;
            value = node.Value;
            return true;
        }

        // Enumerator in-order (iterativo para evitar desbordamiento de pila en árboles grandes)
        public IEnumerator<T> GetEnumerator()
        {
            var stack = new Stack<Node>();
            var current = _root;
            while (stack.Count > 0 || current is not null)
            {
                while (current is not null)
                {
                    stack.Push(current);
                    current = current.Left;
                }

                current = stack.Pop();
                yield return current.Value;
                current = current.Right;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private Node? FindNode(T value)
        {
            var current = _root;
            while (current is not null)
            {
                int cmp = _comparer.Compare(value, current.Value);
                if (cmp == 0) return current;
                current = cmp < 0 ? current.Left : current.Right;
            }
            return null;
        }

        // --------------------------
        // Traversals públicos
        // --------------------------

        /// <summary>
        /// Recorre el árbol en orden (left, node, right) de forma iterativa.
        /// </summary>
        public IEnumerable<T> InOrder()
        {
            var stack = new Stack<Node>();
            var current = _root;
            while (stack.Count > 0 || current is not null)
            {
                while (current is not null)
                {
                    stack.Push(current);
                    current = current.Left;
                }

                current = stack.Pop();
                yield return current.Value;
                current = current.Right;
            }
        }

        /// <summary>
        /// Recorre el árbol en pre-orden (node, left, right) de forma iterativa.
        /// </summary>
        public IEnumerable<T> PreOrder()
        {
            if (_root is null) yield break;
            var stack = new Stack<Node>();
            stack.Push(_root);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                yield return node.Value;
                // push right primero para procesar left antes
                if (node.Right is not null) stack.Push(node.Right);
                if (node.Left is not null) stack.Push(node.Left);
            }
        }

        /// <summary>
        /// Recorre el árbol en post-orden (left, right, node) usando recursión y yield.
        /// </summary>
        public IEnumerable<T> PostOrder()
        {
            foreach (var v in PostOrderRecursive(_root))
                yield return v;
        }

        private IEnumerable<T> PostOrderRecursive(Node? node)
        {
            if (node is null) yield break;
            foreach (var v in PostOrderRecursive(node.Left)) yield return v;
            foreach (var v in PostOrderRecursive(node.Right)) yield return v;
            yield return node.Value;
        }
    }
}
