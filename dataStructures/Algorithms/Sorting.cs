using System;

namespace DataStructures.Algorithms
{
    public static class Sorting
    {
        public static void BubbleSort(int[] arr)
        {
            if (arr is null) throw new ArgumentNullException(nameof(arr));
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int tmp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tmp;
                        swapped = true;
                    }
                }
                if (!swapped) break; // ya está ordenado
            }
        }

        public static void InsertionSort(int[] arr)
        {
            if (arr is null) throw new ArgumentNullException(nameof(arr));
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }

        public static void SelectionSort(int[] arr)
        {
            if (arr is null) throw new ArgumentNullException(nameof(arr));
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                    if (arr[j] < arr[minIdx]) minIdx = j;
                if (minIdx != i)
                {
                    int tmp = arr[i];
                    arr[i] = arr[minIdx];
                    arr[minIdx] = tmp;
                }
            }
        }

        public static void QuickSort(int[] arr)
        {
            if (arr is null) throw new ArgumentNullException(nameof(arr));
            QuickSort(arr, 0, arr.Length - 1);
        }

        private static void QuickSort(int[] arr, int low, int high)
        {
            if (low >= high) return;
            int p = Partition(arr, low, high);
            QuickSort(arr, low, p - 1);
            QuickSort(arr, p + 1, high);
        }

        private static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    int tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;
                }
            }
            int tmp2 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = tmp2;
            return i + 1;
        }

        public static void MergeSort(int[] arr)
        {
            if (arr is null) throw new ArgumentNullException(nameof(arr));
            if (arr.Length <= 1) return;
            var aux = new int[arr.Length];
            MergeSort(arr, aux, 0, arr.Length - 1);
        }

        private static void MergeSort(int[] arr, int[] aux, int left, int right)
        {
            if (left >= right) return;
            int mid = left + (right - left) / 2;
            MergeSort(arr, aux, left, mid);
            MergeSort(arr, aux, mid + 1, right);
            Merge(arr, aux, left, mid, right);
        }

        private static void Merge(int[] arr, int[] aux, int left, int mid, int right)
        {
            int i = left, j = mid + 1, k = left;
            while (i <= mid && j <= right)
            {
                if (arr[i] <= arr[j]) aux[k++] = arr[i++];
                else aux[k++] = arr[j++];
            }
            while (i <= mid) aux[k++] = arr[i++];
            while (j <= right) aux[k++] = arr[j++];
            for (int idx = left; idx <= right; idx++) arr[idx] = aux[idx];
        }
    }
}
