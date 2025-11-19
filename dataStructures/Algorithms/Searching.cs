using System;
using System.Collections.Generic;

namespace DataStructures.Algorithms
{
    public static class Searching
    {
        // Búsqueda lineal (genérica)
        public static int LinearSearch<T>(T[] arr, T value)
        {
            if (arr is null) throw new ArgumentNullException(nameof(arr));
            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < arr.Length; i++)
                if (comparer.Equals(arr[i], value))
                    return i;
            return -1;
        }

        // Sobrecarga específica para int (útil para llamadas en Program.cs)
        public static int LinearSearch(int[] arr, int value)
        {
            if (arr is null) throw new ArgumentNullException(nameof(arr));
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == value) return i;
            return -1;
        }

        // Búsqueda binaria (requiere array ordenado)
        public static int BinarySearch(int[] arr, int value)
        {
            if (arr is null) throw new ArgumentNullException(nameof(arr));
            int low = 0, high = arr.Length - 1;
            while (low <= high)
            {
                int mid = low + ((high - low) >> 1);
                int cmp = arr[mid].CompareTo(value);
                if (cmp == 0) return mid;
                if (cmp < 0) low = mid + 1;
                else high = mid - 1;
            }
            return -1;
        }

        // Búsqueda binaria genérica (requiere IComparer o IComparable)
        public static int BinarySearch<T>(T[] arr, T value, IComparer<T>? comparer = null)
        {
            if (arr is null) throw new ArgumentNullException(nameof(arr));
            comparer ??= Comparer<T>.Default;
            int low = 0, high = arr.Length - 1;
            while (low <= high)
            {
                int mid = low + ((high - low) >> 1);
                int cmp = comparer.Compare(arr[mid], value);
                if (cmp == 0) return mid;
                if (cmp < 0) low = mid + 1;
                else high = mid - 1;
            }
            return -1;
        }
    }
}
