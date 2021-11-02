using System;
using System.Drawing;
using static System.Math;

namespace AlgorithmVisualizer.Arrays
{
	public class ArrayAlgorithms : ArrayVisualizer
	{
		// Extends the SortingVisualizer for visuals

		public ArrayAlgorithms(int[] arr, Graphics g, int maxVal, double entryWidth, bool sortedFlag)
			: base(arr, g, maxVal, entryWidth, sortedFlag) { }

		#region Sorting algorithms
		// Insertion sort
		public void InsertionSort() => InsertionSort(arr, 0, arr.Length - 1);
		private void InsertionSort(int[] arr, int left, int right)
		{
			// from left + 1 to right inclusive
			for (int i = left + 1; i <= right; i++)
			{
				// save i'th value, i - 1 idx as j
				int val = arr[i], j = i - 1;
				// from j to left inclusive, if j'th value > val shift right once
				for (; j >= left && arr[j] > val; j--)
				{
					arr[j + 1] = arr[j];
					DrawValueAt(j + 1);
				}
				// arr[i] may have been overwritten, use val instead
				arr[j + 1] = val;
				DrawValueAt(j + 1);
			}
		}

		//Selection sort
		public void SelectionSort() => SelectionSort(arr, 0, arr.Length - 1);
		private void SelectionSort(int[] arr, int left, int right)
		{
			// from left to right inclusive
			for (int i = left; i <= right; i++)
			{
				// Assume current value is the min value of the current sub-array (from i to right)
				int minIdx = i;
				// Find min value's index in the current sub-array
				for (int j = i + 1; j <= right; j++)
					if (arr[j] < arr[minIdx]) minIdx = j;
				// If minimal value is not the i'th value swap with i'th val
				if (i != minIdx) Swap(arr, i, minIdx);

			}
		}

		// Bubble sort
		public void BubbleSort() => BubbleSort(arr, 0, arr.Length - 1);
		private void BubbleSort(int[] arr, int left, int right)
		{
			// Optimization flag - a full iteration of the inner loop without swaps
			// implies the array is already sorted
			bool swapped = true;
			// loop from left to right inclusive
			for (int i = left; i <= right && swapped; i++)
			{
				// assume no swapping has been done
				swapped = false;
				// left added to avoid sorting offset from right side (needed in case left > 0)
				// i subtracted to account for already sorted values.
				for (int j = left; j < right + left - i; j++)
				{
					if (arr[j] > arr[j + 1])
					{
						Swap(arr, j, j + 1);
						swapped = true;
					}
				}
				// element at ith index is the ith max
			}
		}

		// Quick sort (Lomuto)
		public void QuickSortLomuto() => QuickSortLomuto(arr, 0, arr.Length - 1);
		private void QuickSortLomuto(int[] arr, int left, int right)
		{
			if (left < right)
			{
				int partitionIdx = PartitionLomuto(arr, left, right);
				QuickSortLomuto(arr, left, partitionIdx - 1);
				QuickSortLomuto(arr, partitionIdx + 1, right);
			}
		}
		private int PartitionLomuto(int[] arr, int left, int right)
		{
			int pivot = arr[right];
			int i = left - 1;
			// Try find and swap elements out of order with respect to pivot
			for (int j = left; j < right; j++)
				if (arr[j] <= pivot) Swap(arr, ++i, j);
			Swap(arr, ++i, right);
			return i;
		}
		// Quick sort (Hoare)
		public void QuickSortHoare() => QuickSortHoare(arr, 0, arr.Length - 1);
		private void QuickSortHoare(int[] arr, int left, int right)
		{
			if (left < right)
			{
				int partitionIdx = PartitionHoare(arr, left, right);
				QuickSortHoare(arr, left, partitionIdx);
				QuickSortHoare(arr, partitionIdx + 1, right);
			}
		}
		private int PartitionHoare(int[] arr, int left, int right)
		{
			int pivot = arr[(left + right) / 2];
			while (true)
			{
				// Try find elements out of order with respect to pivot
				while (arr[left] < pivot) left++;
				while (arr[right] > pivot) right--;
				// If left >= right then partitioning is done
				if (left >= right) return right;
				// Otherwise elements out of order found
				Swap(arr, left++, right--);
			}
		}

		// Merge sort
		public void MergeSort() => MergeSort(arr, 0, arr.Length - 1);
		private void MergeSort(int[] arr, int left, int right)
		{
			if (left < right)
			{
				int mid = (left + right) / 2;
				MergeSort(arr, left, mid);
				MergeSort(arr, mid + 1, right);
				MergeSortedArrays(arr, left, mid, right);
			}
		}
		private void MergeSortedArrays(int[] arr, int left, int mid, int right)
		{
			// arr can be thought of as 2 arrays: arr[left : mid], arr[mid + 1 : right]
			int leftIdx = left, rightIdx = mid + 1, i = 0;
			int[] res = new int[right - left + 1];
			// Sorted merge of arr[left : mid], arr[mid + 1 : right] into res[]
			while (leftIdx <= mid && rightIdx <= right)
				res[i++] = arr[leftIdx] <= arr[rightIdx] ? arr[leftIdx++] : arr[rightIdx++];
			while (leftIdx <= mid)
				res[i++] = arr[leftIdx++];
			while (rightIdx <= right)
				res[i++] = arr[rightIdx++];
			// Copy res[] into arr[left : right]
			for (i = 0; i < res.Length; i++)
			{
				arr[i + left] = res[i];
				DrawValueAt(i + left);
			}
		}

		// Heap sort
		public void HeapSort() => HeapSort(arr, 0, arr.Length - 1);
		private void HeapSort(int[] arr, int left, int right)
		{
			CreateMaxHeap(arr, left, right);
			// note that element at index left will be at its sorted position in the last
			// iteration (can be skipped).
			for (int i = right; i > left; i--)
			{
				// swap current max with ith last element of array
				Swap(arr, left, i);
				// Decrease heap size by 1 and heapify down (restore heap)
				HeapifyDown(arr, left, left, --right);
			}
		}
		private void CreateMaxHeap(int[] arr, int left, int right)
		{
			// Create max heap from the sub-array arr[left:right]
			for (int i = (left + right) / 2; i >= left; i--)
				HeapifyDown(arr, i, left, right);
		}
		private void HeapifyDown(int[] arr, int i, int left, int right)
		{
			// Heapify down starting starting at i for the sub-array arr[left : right]
			for (int j = MaxChildIdx(i); j != -1; j = MaxChildIdx(i))
			{
				Swap(arr, i, j);
				i = j;
			}

			int MaxChildIdx(int p)
			{
				// Helper method to find a child larger then arr[j] if exists
				int maxChildIdx = -1;
				// Note that left is used to account for the offset if exists
				int[] children = new int[] { LeftChildIdx(p - left) + left,
											RightChildIdx(p - left) + left };
				foreach (int child in children)
					if (child <= right && arr[child] > arr[p]) maxChildIdx = p = child;
				return maxChildIdx;
			}
		}
		private int ParentIdx(int i) => (i - 1) / 2;
		private int LeftChildIdx(int i) => i * 2 + 1;
		private int RightChildIdx(int i) => i * 2 + 2;

		// Counting sort
		public void CountingSort()
		{
			// Finding min/max values in array
			(int Min, int Max) minMax = GetMinMax(arr);
			// Starting counting sort
			CountingSort(arr, minMax.Min, minMax.Max);
		}
		private void CountingSort(int[] arr, int minVal, int maxVal)
		{
			// Init counter array (per value)
			int N = maxVal - minVal + 1;
			int[] counts = new int[N];
			// Count each value. offset by minVal because the value may be nagtive
			for (int i = 0; i < arr.Length; i++) counts[arr[i] - minVal]++;
			// Compute prefix sum for counts
			for (int i = 1; i < N; i++) counts[i] += counts[i - 1];
			// Loop in reverse to keep the sort stable, put values in thier sorted pos.
			// Note that the prefix sum indexing is 1 based!
			int[] res = new int[arr.Length];
			for (int i = arr.Length - 1; i >= 0; i--)
			{
				// The sorted index of a value is given by lookup in counts by value
				// After placing a value from counts into res - decrement that value
				int idx = counts[arr[i] - minVal]-- - 1;
				res[idx] = arr[i];
				// Visualization method #1. use only 1 method!
				DrawValueAt(idx, res[idx]);
			}
			// Copy res into arr
			for (int i = 0; i < res.Length; ++i)
			{
				arr[i] = res[i];
				// Visualization method #2. use only 1 method!
				// DrawValueAt(i);
			}
		}

		// Radix sort
		public void RadixSort()
		{
			// LSB Radix sort for non negative numbers
			// Find max value in arr
			int max = GetMinMax(arr).Max;
			// Run counting sort for each exp in
			for (int exp = 1; max / exp > 0; exp *= 10) CountingSort(arr, exp);
		}
		private void CountingSort(int[] arr, int exp)
		{
			// Count each digit. exp denotes the digit's 10s palce used for counting, i.e,
			// exp = 1 for counting the 1's place, exp = 2 for counting the 2's place...
			int[] counts = new int[10];
			for (int i = 0; i < arr.Length; i++) counts[arr[i] / exp % 10]++;

			// Compute prefix sum for 'counts'. In the prefix sum the index denotes the
			// value, and holds 1 based indices denoting the sorted position of a value.
			for (int i = 1; i < counts.Length; i++) counts[i] += counts[i - 1];

			// Building the output array
			int[] res = new int[arr.Length];
			// Loop in reverse to keep the sort stable
			for (int i = arr.Length - 1; i >= 0; i--)
			{
				int idx = counts[arr[i] / exp % 10]-- - 1;
				res[idx] = arr[i];
				// Visualization method #1. use only 1 method!
				DrawValueAt(idx, res[idx]);
			}

			// Copy res into arr
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = res[i];
				// Visualization method #2. use only 1 method!
				// DrawValueAt(i);
			}
		}

		// Introspective sort
		public void IntroSort()
		{
			// Compute max recursive depth to limit quicksort's recursive tree depth.
			int maxDepth = 2 * (int)Log(arr.Length - 1, 2);
			IntroSort(arr, 0, arr.Length - 1, maxDepth);
		}
		private void IntroSort(int[] arr, int left, int right, int maxDepth)
		{
			// For arrays of size <= 16 use insertion sort
			int N = right - left;
			if (N < 16) InsertionSort(arr, left, right);
			// Otherwise, if max depth exceeded use heap sort
			else if (maxDepth == 0) HeapSort(arr, left, right);
			// Otherwise resume using quick sort
			else
			{
				int piv = PartitionHoare(arr, left, right);
				IntroSort(arr, left, piv, maxDepth - 1);
				IntroSort(arr, piv + 1, right, maxDepth - 1);
			}
		}
		#endregion

		#region Searching algorithms
		// The following searching algos assume the array is sorted in ascending order.
		public void BinarySearch() => AbstractSearch("BinarySearch");
		public void TernarySearch() => AbstractSearch("TernarySearch");
		public void ExponentialSearch() => AbstractSearch("ExponentialSearch");
		public void FibonacciSearch() => AbstractSearch("FibonacciSearch");
		private void AbstractSearch(string algoName)
		{
			int N = arr.Length;
			if (N > 0)
			{
				int searchValIdx = rnd.Next(N), searchVal = arr[searchValIdx];
				Console.WriteLine($"Searching for {searchVal} located at {searchValIdx} using {algoName}:");
				// Mark search val
				DrawValueAt(searchValIdx, Brushes.Red);
				Sleep(Delay.Long);
				// Unmark search val
				DrawValueAt(searchValIdx, Brushes.White);
				int idx = -1;
				// Chose and run a searching algo
				switch (algoName)
				{
					case "BinarySearch":
						idx = BinarySearch(arr, 0, N - 1, searchVal);
						break;
					case "TernarySearch":
						idx = TernarySearch(arr, 0, N - 1, searchVal);
						break;
					case "ExponentialSearch":
						idx = ExponentialSearch(arr, N, searchVal);
						break;
					case "FibonacciSearch":
						idx = FibonacciSearch(arr, N, searchVal);
						break;
				}
				if (idx != -1) Console.WriteLine($"Found value {searchVal} at {idx}");
				else Console.WriteLine($"The value {searchVal} could not be found");
				// Clear visuals
				Sleep(Delay.VeryLong);
				DrawValues(0, N - 1);
			}
		}
		private int BinarySearch(int[] arr, int l, int r, int val)
		{
			// if l > r then value is not in arr
			if (l > r) return -1;
			int mid = (l + r) / 2;
			DrawValueAt(mid, Brushes.Blue);
			// if arr[mid] is val return mid (its index)
			if (arr[mid] == val)
			{
				DrawValueAt(mid, Brushes.Red);
				return mid;
			}
			DrawValueAt(mid, Brushes.White);
			// if val is smaller go left, otherwise go right
			if (arr[mid] > val)
			{
				DrawValues(mid, r, Brushes.DimGray);
				r = mid - 1;
			}
			else
			{
				DrawValues(l, mid, Brushes.DimGray);
				l = mid + 1;
			}
			return BinarySearch(arr, l, r, val);
		}
		private int TernarySearch(int[] arr, int l, int r, int val)
		{
			// Base case
			if (l > r) return -1;
			// Find 2 middles (splitting array into 3 thirds)
			int d = r - l, mid1 = l + d / 3, mid2 = r - d / 3;
			DrawValueAt(mid1, Brushes.Blue);
			DrawValueAt(mid2, Brushes.Blue);
			// Base cases (arr[mid1] or arr[mid2] = val)
			foreach (int i in new int[] { mid1, mid2 })
			{
				if (arr[i] == val)
				{
					DrawValueAt(i, Brushes.Red);
					return i;
				}
			}
			DrawValueAt(mid1, Brushes.White);
			DrawValueAt(mid2, Brushes.White);
			// Value is within third 1(left)
			if (val < arr[mid1])
			{
				DrawValues(mid1, r, Brushes.DimGray);
				return TernarySearch(arr, l, mid1 - 1, val);
			}
			// Value is within third 3(right)
			if (val > arr[mid2])
			{
				DrawValues(l, mid2, Brushes.DimGray);
				return TernarySearch(arr, mid2 + 1, r, val);
			}	
			// Value is within third 2(middle)
			DrawValues(l, mid1, Brushes.DimGray);
			DrawValues(mid2, r, Brushes.DimGray);
			return TernarySearch(arr, mid1 + 1, mid2 - 1, val);
		}
		private int ExponentialSearch(int[] arr, int N, int val)
		{
			// Basic case
			if (arr[0] == val)
			{
				DrawValueAt(0, Brushes.Red);
				return 0;
			}
			int i = 1;
			// Bound searching space within array by doubling i - the search start index
			while (i < N && arr[i] <= val)
			{
				DrawValues(i / 2, i - 1, Brushes.DimGray);
				i *= 2;
			}
			DrawValues(Min(i, N - 1) + 1, N - 1, Brushes.DimGray); // "Unmark" values not in search sapce
			Sleep(Delay.VeryLong);
			// Use binary serach in the selected searching space
			return BinarySearch(arr, i / 2, Min(i, N - 1), val);
		}
		private int FibonacciSearch(int[] arr, int N, int val)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Utils
		private void Swap(int[] arr, int i, int j)
		{
			// The actual swap
			int tmp = arr[i];
			arr[i] = arr[j];
			arr[j] = tmp;

			// Visualizing the swap:
			// Undraw ith value, draw new value
			UndrawValueAt(i);
			DrawValueAt(i);
			// Undraw jth value, draw new value
			UndrawValueAt(j);
			DrawValueAt(j);
		}
		private (int Min, int Max) GetMinMax(int[] arr)
		{
			// Finds and returns a tuple containing the min/max values of the array.
			// If the array has 1 element then min = max = arr[0]
			// Assumes arr has at least 1 element
			int min = arr[0], max = arr[0];
			for (int i = 1; i < arr.Length; i++)
			{
				if (arr[i] < min) min = arr[i];
				if (arr[i] > max) max = arr[i];
			}
			return (min, max);
		}
		private bool IsSortedAsc(int[] arr, int left, int right)
		{
			// Given a non null array determines if it is sorted in ascending order
			if (left < right)
				for (int i = left + 1; i < right; i++)
					if (arr[i] < arr[i - 1]) return false;
			return true;
		}
		#endregion
	}
}
