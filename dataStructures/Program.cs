using DataStructures;
using DataStructures.Algorithms;
using DataStructures.Structures;
using EstructurasDeDatos_CS.DataStructures;
using System;
using System.Collections.Generic;

namespace EstructurasDeDatos_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== PROYECTO: ESTRUCTURAS DE DATOS EN C# =====\n");

            // ----------------------------------------------------
            // 1. STACK (PILA)
            // ----------------------------------------------------
            Console.WriteLine(">>> Probando MyStack<int>:");
            var stack = new MyStack<int>();

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            Console.WriteLine("Elementos en la pila (foreach):");
            foreach (var item in stack)
                Console.WriteLine(item);

            Console.WriteLine("Tope del stack: " + stack.Peek());
            Console.WriteLine("Pop: " + stack.Pop());
            Console.WriteLine("Nuevo tope: " + stack.Peek());
            Console.WriteLine("Count: " + stack.Count);
            Console.WriteLine("¿Está vacío?: " + stack.IsEmpty());

            Console.WriteLine();


            // ----------------------------------------------------
            // 2. QUEUE (COLA)
            // ----------------------------------------------------
            Console.WriteLine(">>> Probando MyQueue<string>:");
            var queue = new MyQueue<string>();

            queue.Enqueue("A");
            queue.Enqueue("B");
            queue.Enqueue("C");

            Console.WriteLine("Elementos en la cola (foreach):");
            foreach (var item in queue)
                Console.WriteLine(item);

            Console.WriteLine("Primero en la cola: " + queue.Peek());
            Console.WriteLine("Dequeue: " + queue.Dequeue());
            Console.WriteLine("Nuevo primero: " + queue.Peek());
            Console.WriteLine("Count: " + queue.Count);

            Console.WriteLine();


            // ----------------------------------------------------
            // 3. LINKED LIST (LISTA ENLAZADA)
            // ----------------------------------------------------
            Console.WriteLine(">>> Probando MyLinkedList<int>:");
            var list = new MyLinkedList<int>();

            list.AddFirst(50);
            list.AddFirst(40);
            list.AddLast(60);

            Console.WriteLine("Elementos de la lista (foreach):");
            foreach (var item in list)
                Console.WriteLine(item);

            Console.WriteLine("¿Existe 40?: " + list.Contains(40));
            Console.WriteLine("¿Existe 999?: " + list.Contains(999));

            Console.WriteLine("Eliminando 40...");
            list.Remove(40);

            Console.WriteLine("Lista actualizada (foreach):");
            foreach (var item in list)
                Console.WriteLine(item);

            Console.WriteLine("Insertando 999 en índice 1...");
            list.InsertAt(1, 999);

            Console.WriteLine("Lista después del insert (foreach):");
            foreach (var item in list)
                Console.WriteLine(item);

            Console.WriteLine();


            // ----------------------------------------------------
            // 4. SORTING (ORDENAMIENTO)
            // ----------------------------------------------------
            Console.WriteLine(">>> Probando algoritmos de ordenamiento:");

            int[] array1 = { 5, 2, 9, 1, 3 };
            Console.WriteLine("Array original:");
            PrintArray(array1);

            Sorting.BubbleSort(array1);
            Console.WriteLine("BubbleSort:");
            PrintArray(array1);

            int[] array2 = { 7, 4, 1, 8, 2 };
            Sorting.InsertionSort(array2);
            Console.WriteLine("InsertionSort:");
            PrintArray(array2);

            Console.WriteLine();


            // ----------------------------------------------------
            // 5. SEARCHING (BÚSQUEDAS)
            // ----------------------------------------------------
            Console.WriteLine(">>> Probando algoritmos de búsqueda:");
            int[] sortedArray = { 1, 3, 5, 7, 9, 11 };

            Console.WriteLine("Array ordenado:");
            PrintArray(sortedArray);

            int pos1 = Searching.LinearSearch(sortedArray, 7);
            Console.WriteLine($"Búsqueda lineal de 7: índice {pos1}");

            int pos2 = Searching.BinarySearch(sortedArray, 9);
            Console.WriteLine($"Búsqueda binaria de 9: índice {pos2}");

            Console.WriteLine();


            // ----------------------------------------------------
            // 6. HASH (TABLA HASH)
            // ----------------------------------------------------
            Console.WriteLine(">>> Probando HashTable<string,int>:");
            var hash = new HashTable<string, int>();

            hash.Add("uno", 1);
            hash.Add("dos", 2);
            hash.Add("tres", 3);

            Console.WriteLine("Contenido de la hash (foreach KeyValuePair):");
            foreach (var kv in hash)
                Console.WriteLine($"{kv.Key} => {kv.Value}");

            if (hash.TryGetValue("dos", out var valDos))
                Console.WriteLine($"Valor asociado a 'dos': {valDos}");

            Console.WriteLine("¿Contiene 'cuatro'?: " + hash.ContainsKey("cuatro"));

            Console.WriteLine("Eliminando 'dos'...");
            hash.Remove("dos");
            Console.WriteLine("¿Contiene 'dos' después de Remove?: " + hash.ContainsKey("dos"));
            Console.WriteLine("Count: " + hash.Count);

            Console.WriteLine();


            // ----------------------------------------------------
            // 7. BINARY TREE (ÁRBOL BINARIO DE BÚSQUEDA)
            // ----------------------------------------------------
            Console.WriteLine(">>> Probando BinarySearchTree<int>:");
            var bst = new BinarySearchTree<int>();
            int[] values = { 50, 30, 70, 20, 40, 60, 80 };

            foreach (var v in values)
                bst.Add(v);

            // Usar explícitamente los recorridos implementados
            Console.WriteLine("Recorrido in-order (InOrder()) (debería salir ordenado):");
            foreach (var v in bst.InOrder())
                Console.Write(v + " ");
            Console.WriteLine();

            Console.WriteLine("Recorrido pre-order (PreOrder()):");
            foreach (var v in bst.PreOrder())
                Console.Write(v + " ");
            Console.WriteLine();

            Console.WriteLine("Recorrido post-order (PostOrder()):");
            foreach (var v in bst.PostOrder())
                Console.Write(v + " ");
            Console.WriteLine();

            Console.WriteLine("¿Contiene 60?: " + bst.Contains(60));
            Console.WriteLine("¿Contiene 100?: " + bst.Contains(100));

            Console.WriteLine("Eliminando 70 (nodo con dos hijos)...");
            bst.Remove(70);

            Console.WriteLine("Recorridos después del Remove:");

            Console.WriteLine("In-order:");
            foreach (var v in bst.InOrder())
                Console.Write(v + " ");
            Console.WriteLine();

            Console.WriteLine("Pre-order:");
            foreach (var v in bst.PreOrder())
                Console.Write(v + " ");
            Console.WriteLine();

            Console.WriteLine("Post-order:");
            foreach (var v in bst.PostOrder())
                Console.Write(v + " ");
            Console.WriteLine();

            if (bst.TryGetMin(out var min)) Console.WriteLine("Mínimo: " + min);
            if (bst.TryGetMax(out var max)) Console.WriteLine("Máximo: " + max);

            Console.WriteLine();


            //// FIN DEL PROGRAMA
            Console.WriteLine("===== FIN DE LA DEMO =====");
        }


        // Función auxiliar para imprimir arrays
        static void PrintArray(int[] arr)
        {
            Console.Write("[ ");
            foreach (var n in arr)
                Console.Write(n + " ");
            Console.WriteLine("]");
        }
    }
}
