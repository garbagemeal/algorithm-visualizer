using System;
using System.Collections.Generic;

namespace AlgorithmVisualizer.DataStructures.Heap
{
	public class BinaryMinHeap<T> where T : IComparable
	{
		// Implementation of a priority queue unsing a binary min heap

		/*
		 * Time complexities for operations:
		 * Construction - O(1) for an empty heap, O(n) for non empty heaps
		 * Create max heap - O(n)
		 * Heapify up - O(log(n))
		 * Heapify down -  O(log(n))
		 * Peek min value - O(1)
		 * Check if full - O(1)
		 * Enqueue value -  O(log(n))
		 * Dequeue value -  O(log(n))
		 * 
		 * n is the length of the given array/collection
		 */
		private T[] arr;
		private int size, maxSize;
		public int Count { get { return size; } }

		#region Constructors
		public BinaryMinHeap() : this(1)
		{
		}
		public BinaryMinHeap(int _size)
		{
			if (_size <= 0) throw new ArgumentException("Size must be > 0");
			maxSize = _size;
			arr = new T[maxSize];
			size = 0;
		}
		public BinaryMinHeap(T[] _arr)
		{
			if (_arr.Length == 0) throw new ArgumentException("Size of array must be > 0");
			// Set sizes and copy arr into heap
			size = maxSize = _arr.Length;
			arr = new T[size];
			for (int i = 0; i < size; i++) arr[i] = _arr[i];
			Heapify();
		}
		public BinaryMinHeap(List<T> list)
		{
			if (list.Count == 0) throw new ArgumentException("Size of list must be > 0");
			// Set sizes and copy list into heap
			size = maxSize = list.Count;
			arr = new T[size];
			for (int i = 0; i < size; i++) arr[i] = list[i];
			Heapify();
		}
		#endregion

		#region Main operations
		public T Peek()
		{
			// Peek heap's root node
			if (size == 0) throw new InvalidOperationException("Heap is empty, cannot peek!");
			return arr[0];
		}
		public void Enqueue(T val)
		{
			// Add value into heap end, apply heaify up to maintain heap
			DoubleSizeIfFull();
			arr[size++] = val;
			HeapifyUp(size - 1);
		}
		public T Dequeue()
		{
			// Remove value from heap's root by swapping it with last element,
			// decreasing size of heap by 1, applying heapify down on new root
			// to maintain heap.
			Swap(0, --size);
			HeapifyDown(0);
			// return the last element (no longer considered part of the heap)
			return arr[size];
		}
		#endregion

		#region Indexing
		private int Parent(int i) => (i - 1) / 2;
		private int LeftChild(int i) => i * 2 + 1;
		private int RightChild(int i) => i * 2 + 2;
		#endregion

		#region Comparison
		private int Compare(int i, int j) => arr[i].CompareTo(arr[j]);
		private bool Less(int i, int j) => Compare(i, j) < 0;
		private bool LessOrEqual(int i, int j) => Compare(i, j) <= 0;
		#endregion

		#region Utilities
		private int MinChild(int i)
		{
			// If node i has a child with a smaller value, returns the smallest child idx,
			// otherwise returns -1
			int minChildIdx = -1;
			int[] children = new int[] { LeftChild(i), RightChild(i) };
			foreach (int j in children) if (j < size && Less(j, i)) minChildIdx = i = j;
			return minChildIdx;
		}
		private void Heapify()
		{
			// Heapify the array(heap), O(n), n is size

			// Note that HeapifyDown is invoked only on the first half of the array
			// starting from the end of the half, the other half already conforms
			// to the heap invariant

			for (int i = Math.Max(0, size / 2 - 1); i >= 0; i--)
				HeapifyDown(i);
		}
		private void HeapifyUp(int i)
		{
			// Heapify up starting at i
			while (Less(i, Parent(i)))
			{
				int iParent = Parent(i);
				Swap(i, iParent);
				i = iParent;
			}
		}
		private void HeapifyDown(int i)
		{
			// Heapify down starting at i
			// HeapifyDown can only be applied on a node where both left and right
			// sub-trees conform to the Heap invariant!
			for (int j = MinChild(i); j != -1; j = MinChild(i))
			{
				Swap(i, j);
				i = j;
			}
		}
		private void Swap(int i, int j)
		{
			// Swap values at 2 given indices
			T tmp = arr[i];
			arr[i] = arr[j];
			arr[j] = tmp;
		}
		public bool IsFull()
		{
			// Check if heap is full
			return size == maxSize;
		}
		public bool IsMinHeap()
		{
			return IsMinHeap(0);
		}
		private bool IsMinHeap(int i)
		{
			int[] children = new int[] { LeftChild(i), RightChild(i) };
			foreach (int child in children)
			{
				// If i does not conform to the heap invariant
				if (!LessOrEqual(i, child)) return false;
				// If child does not conform to the heap invariant
				if (!IsMinHeap(child)) return false;
			}
			return true;
		}
		private void DoubleSizeIfFull()
		{
			// If heap is full
			if (IsFull())
			{
				// Double the size of the heap
				maxSize *= 2;
				T[] newHeap = new T[maxSize];
				for (int i = 0; i < size; i++) newHeap[i] = arr[i];
				arr = newHeap;
			}
		}

		public T[] ToArray()
		{
			T[] heapCP = new T[size];
			Array.Copy(arr, heapCP, size);
			return heapCP;
		}
		#endregion
	}
}
