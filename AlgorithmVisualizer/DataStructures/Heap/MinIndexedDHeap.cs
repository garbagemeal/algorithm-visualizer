using System;

namespace AlgorithmVisualizer.DataStructures.Heap
{
	public class MinIndexedDHeap<T> where T : IComparable
	{
		// Important node: adding a null object into the heap will likely cause issues!

		/*
		 * Time complexity for operations:
		 * Construction - O(n)
		 * Contains - O(1)
		 * ValueAt - O(1)
		 * InsertAt - O(logD(n))
		 * RemoveAt - O(logD(n))
		 * UpdateAt - O(logD(n))
		 * Peek min key index - O(1)
		 * Peek min value - O(1)
		 * Dequeue min key index - O(logD(n))
		 * Dequeue min value - O(logD(n))
		 * DecreaseKey - O(logD(n))
		 * IncreaseKey - O(logD(n))
		 * 
		 * Note: n is the number of elements in the heap, D is the degree of the heap
		 * i.e for a binary heap (D = 2) RemoveAt will take O(log2(n)) time.
		 */

		// count is the current number of elements in the heap,
		// size is the current max heap size (capacity)
		private int count, size;
		public int Count { get { return count; } }
		public int Size { get { return size; } }

		// Degree of the heap. i.e: 2 for a binary heap
		private readonly int D;
		/*
		 * vals - index: keyIndex, value: key (value by which heap is maintained)
		 * Stores the key value pairs, i.e: (node id, min dist), indexed using the keyIndex
		 * ****************************************************************************
		 * pm   - index: keyIndex, value: position
		 * Maps a position in the heap for each keyIndex, indexed using the keyIndex
		 * ****************************************************************************
		 * im   - index: position, value: keyIndex
		 * Maps a keyIndex for each position in the heap, indexed using the position
		 * ****************************************************************************
		 * Note: pm and im are inversions of each other, and so: pm[im[i]] = im[pm[i]] = i
		 */
		private T[] vals;
		private int[] pm, im;

		public MinIndexedDHeap(int degree, int _size)
		{
			if (_size <= 0) throw new ArgumentException("Size must be > 0");
			size = _size;
			if (degree < 2) Console.WriteLine("Given degree less than 2, defaulting degree to 2");
			D = Math.Max(2, degree);
			pm = new int[size];
			im = new int[size];
			vals = new T[size];

			for (int i = 0; i < size; i++) pm[i] = im[i] = -1;
		}

		private bool Less(int i, int j)
		{
			// Returns true if value at pos i is smaller than value at pos j. i and j are
			// positions, use im[i], im[j] to get a key index to index into vals.
			return vals[im[i]].CompareTo(vals[im[j]]) < 0;
		}
		private bool LessOrEqual(int i, int j)
		{
			// Returns true if value at pos i is smaller than value at pos j. i and j are
			// positions, use im to get a key index to index into vals.
			return vals[im[i]].CompareTo(vals[im[j]]) <= 0;
		}
		private bool Less(T v1, T v2)
		{
			// Returns true if v1 < v2, false otherwise
			return v1.CompareTo(v2) < 0;
		}
		private void Swap(int i, int j)
		{
			// i and j are positions, im[i] and im[j] evaluate to key indices
			// swap positions in pm via key indices
			pm[im[j]] = i;
			pm[im[i]] = j;
			// swap key indices in im via positions
			int tmp = im[i];
			im[i] = im[j];
			im[j] = tmp;
		}
		private int GetParentIdx(int i)
		{
			// Returns the parent position for the given node's position
			return (i - 1) / D;
		}
		private int GetFirstChildIdx(int i)
		{
			// Returns the first child's position for the given node's position
			return i * D + 1;
		}
		private int GetMinChildIdx(int i)
		{
			// Returns the position of the smallest child of parent node i
			// Returns -1 if i has no children or no child is smaller than i
			int minChildIdx = -1, from = GetFirstChildIdx(i), to = Math.Min(from + D, count);
			for (int j = from; j < to; j++) if (Less(j, i)) minChildIdx = i = j;
			return minChildIdx;
		}

		public bool Contains(int ki)
		{
			// Returns true if ki(key index) exists in the heap
			// Note: the existance of a key implies the key is in bounds.
			KeyInBoundsOrThrow(ki);
			return pm[ki] != -1;
		}
		public T ValueAt(int ki)
		{
			// Returns value mapped to given ki IFF ki in use
			KeyInUseOrThrow(ki);
			return vals[ki];
		}
		public void InsertAt(int ki, T val)
		{
			// Map given value to given ki IFF ki is not in use and in bounds
			KeyNotInUseOrThrow(ki);
			//DoubleSizeIfFull();
			pm[ki] = count;
			im[count] = ki;
			vals[ki] = val;
			HeapifyUp(count++);
		}
		public T RemoveAt(int ki)
		{
			// Remove mapping to ki IFF ki in use
			KeyInUseOrThrow(ki);
			int i = pm[ki];
			Swap(i, --count);
			// After replacing the removed value might need to restore heap property
			HeapifyDown(i);
			HeapifyUp(i);
			T val = vals[ki];
			vals[ki] = default;
			pm[ki] = im[count] = -1;
			return val;
		}
		public void UpdateAt(int ki, T val)
		{
			// Update value mapped to ki
			KeyInUseOrThrow(ki);
			int i = pm[ki];
			vals[ki] = val;
			// After updating the value might need to restore heap property
			HeapifyDown(i);
			HeapifyUp(i);
		}
		private void DoubleSizeIfFull()
		{
			// Doubles the size for this IPQ if it is full
			if (count == size)
			{
				// create new arrays double the size
				size *= 2;
				int[] newPM = new int[size];
				int[] newIM = new int[size];
				T[] newVals = new T[size];
				// copy old arrays into the begining of the new arrays
				for (int i = 0; i < count; i++)
				{
					newPM[i] = pm[i];
					newIM[i] = im[i];
					newVals[i] = vals[i];
				}
				// fill remainder of newPM/newIM with -1
				for (int i = count; i < size; i++) newPM[i] = newIM[i] = -1;
				// update references to the new arrays
				pm = newPM;
				im = newIM;
				vals = newVals;
			}
		}

		private void HeapifyUp(int i)
		{
			// Method to heapify up starting at i
			while (Less(i, GetParentIdx(i)))
			{
				int iParent = GetParentIdx(i);
				Swap(i, iParent);
				i = iParent;
			}
		}
		private void HeapifyDown(int i)
		{
			// Method to heapify down starting at i
			for (int j = GetMinChildIdx(i); j != -1;)
			{
				Swap(i, j);
				i = j;
				j = GetMinChildIdx(i);
			}
		}
		public bool IsMinHeap()
		{
			// Returns true if this IPQ is a min heap, false otherwise
			return IsMinHeap(0);
		}
		public bool IsMinHeap(int i)
		{
			int from = GetFirstChildIdx(i), to = Math.Min(from + D, count);
			for (int j = from; j < to; j++)
			{
				// if parent i's value > child j's value return false
				if (!LessOrEqual(i, j)) return false;
				// Recur on child, if child does't conform to heap invariant return false
				if (!IsMinHeap(j)) return false;
			}
			// Tree rooted at i is a min heap
			return true;
		}

		public int PeekMinKeyIndex()
		{
			// Returns the key index of the heap's root node
			NonEmptyOrThrow();
			return im[0];
		}
		public T PeekMinValue()
		{
			// Returns the value of the heap's root node
			NonEmptyOrThrow();
			return vals[im[0]];
		}
		public int DequeueMinKeyIndex()
		{
			// Removes and returns the key index of the heap's root node
			int minKI = PeekMinKeyIndex();
			RemoveAt(minKI);
			return minKI;
		}
		public T DequeueMinValue()
		{
			// Removes and returns the value of the heap's root node
			T minVal = PeekMinValue();
			RemoveAt(im[0]);
			return minVal;
		}
		public void IncreaseKey(int ki, T val)
		{
			// Increase value associated with ki (key index) if val larger
			KeyInUseOrThrow(ki);
			if (Less(vals[ki], val))
			{
				vals[ki] = val;
				HeapifyDown(pm[ki]);
			}
		}
		public void DecreaseKey(int ki, T val)
		{
			// Decrease value associated with ki (key index) if val smaller
			KeyInUseOrThrow(ki);
			if (Less(val, vals[ki]))
			{
				vals[ki] = val;
				HeapifyUp(pm[ki]);
			}
		}

		// Helper methods for exception raising
		private void KeyInUseOrThrow(int ki)
		{
			if (!Contains(ki))
				throw new InvalidOperationException("Given key index does not exist!");
		}
		private void KeyNotInUseOrThrow(int ki)
		{
			if (Contains(ki))
				throw new InvalidOperationException("Given key index already exists!");
		}
		private void KeyInBoundsOrThrow(int ki)
		{
			if (ki < 0 || ki >= size)
				throw new IndexOutOfRangeException("Given key index is out of bounds!");
		}
		private void NonEmptyOrThrow()
		{
			if (count == 0)
				throw new InvalidOperationException("The priority queue is empty!");
		}

		public (int id, T val)[] ToArray()
		{
			(int id, T val)[] idValArr = new (int, T)[count];
			for (int i = 0; i < count; i++) idValArr[i] = (im[i], vals[im[i]]);
			return idValArr;
		}
	}
}
