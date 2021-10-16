using System;

namespace AlgorithmVisualizer.DataStructures
{
	// Also called union-find or merge–find set
	public class DisjointSet
	{
		/*
		 * Time complexity for operations:
		 * Construction - O(n)
		 * Union - α(n)
		 * Find - α(n)
		 * Get component size - α(n)
		 * Check if connected - α(n)
		 * Get component count - O(1)
		 * 
		 * Note: n is the number of elements in the heap.
		 * α(n) is amortized constant time, in other words almost constant time but not quite.
		 * A good example for amortized time complexity would be a dynamic array
		 * doubling its size when full by copying itself into a new array double the size.
		 */

		// the size of the arrays; also the number of elements in the disjoint set.
		private readonly int size;
		// the number of components in the disjoint set. initially the same as size
		private int numComponents;
		// given an index i, the value in the array at index i is its parent.
		// if a value at index i is i, then the node in this location is a root node
		private int[] rootMap;
		// array to track the size of each component. initially each component is of size 1
		private int[] sizes;

		public int Count { get { return size; } }
		public int NumComponents { get { return numComponents; } }

		public DisjointSet(int _size)
		{
			if (_size <= 0) throw new ArgumentException("size must be > 0");
			size = numComponents = _size;
			// Init both arrays as previously described
			rootMap = new int[size];
			sizes = new int[size];
			for (int i = 0; i < size; i++)
			{
				rootMap[i] = i;
				sizes[i] = 1;
			}
		}

		public int Find(int i)
		{
			/* Finds and returns the index of the root of the component as well as
			 * applying path compression to achieve amortized(near constant) time complexity
			 * in the Unify operation.
			 * Nodes traversed along the path will all point to the root node
			 */
			int rootIdx = i;
			// as long as value at index rootIdx is not a self loop(root node)
			while (rootIdx != rootMap[rootIdx]) rootIdx = rootMap[rootIdx];
			// Path compression
			while (i != rootIdx)
			{
				int parentIdx = rootMap[i];
				rootMap[i] = rootIdx;
				i = parentIdx;
			}
			return rootIdx;
		}
		public void Unify(int i, int j)
		{
			// If both nodes are already connected (same component) - return
			if (Connected(i, j)) return;
			// Find root nodes of i, j
			int rootI = Find(i), rootJ = Find(j);

			// Merge smaller component into the larger component
			// If rootI's size >= rootJ's size: swap rootI, rootJ
			if (sizes[rootI] >= sizes[rootJ]) Swap(ref rootI, ref rootJ);
			sizes[rootJ] += sizes[rootI];
			rootMap[rootI] = rootJ;
			sizes[rootI] = 0;
			// After unifying 2 components into 1 the number of components decreases by 1
			numComponents--;
		}
		public bool Connected(int i, int j)
		{
			// Checks if both components with ids i, j, are connected (in same component)
			return Find(i) == Find(j);
		}

		public int ComponentSize(int i)
		{
			// Returns the size of the component i belongs to
			return sizes[Find(i)];
		}

		// Util method to swap contents of 2 int vars via reference
		private void Swap(ref int i, ref int j)
		{
			int tmp = i;
			i = j;
			j = tmp;
		}
	}
}
