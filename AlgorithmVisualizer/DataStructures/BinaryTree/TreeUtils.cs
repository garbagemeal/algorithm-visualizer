using System;
using System.Collections.Generic;


namespace AlgorithmVisualizer.DataStructures.BinaryTree
{
	public static class TreeUtils<T> where T : IComparable
	{
		// Note to self: use a dict to improve runtime of tree construction methods

		#region DFS
		// Depth first searches (pre/in/level)
		// Recursive
		public static void PrintPreOrderRec(BinNode<T> node)
		{
			if (node == null) return;
			PrintNode(node);
			PrintPreOrderRec(node.Left);
			PrintPreOrderRec(node.Right);
		}
		public static void PrintInOrderRec(BinNode<T> node)
		{
			if (node == null) return;
			PrintInOrderRec(node.Left);
			PrintNode(node);
			PrintInOrderRec(node.Right);
		}
		public static void PrintPostOrderRec(BinNode<T> node)
		{
			if (node == null) return;
			PrintPostOrderRec(node.Left);
			PrintPostOrderRec(node.Right);
			PrintNode(node);
		}
		
		// Iterative using a stack replacing the recusion call stack
		public static void PrintPreOrderItr(BinNode<T> node)
		{
			Stack<BinNode<T>> stk = new Stack<BinNode<T>>();
			stk.Push(node);
			while(stk.Count > 0)
			{
				BinNode<T> curNode = stk.Pop();
				if(curNode != null)
				{
					PrintNode(curNode);
					if (curNode.Right != null) stk.Push(curNode.Right);
					if (curNode.Left != null) stk.Push(curNode.Left);
				}
			}
		}
		public static void PrintInOrderItr(BinNode<T> node)
		{
			Stack<BinNode<T>> stk = new Stack<BinNode<T>>();
			BinNode<T> curNode = node;
			while (stk.Count > 0 || curNode != null)
			{
				// Find leftmost node for tree rooted at curNode
				while (curNode != null)
				{
					stk.Push(curNode);
					curNode = curNode.Left;
				}
				// curNode is null, pop from stk to get the leftmost node and print it
				curNode = stk.Pop();
				PrintNode(curNode);
				// Visit the right sub-tree of the leftmost node
				curNode = curNode.Right;
			}
		}
		public static void PrintPostOrderItr(BinNode<T> node)
		{
			BinNode<T> curNode = node, prevNode = null;
			Stack<BinNode<T>> stk = new Stack<BinNode<T>>();
			while(stk.Count > 0 || curNode != null)
			{
				if(curNode != null)
				{
					// Push node into stk and visit left sub-tree
					stk.Push(curNode);
					curNode = curNode.Left;
				}
				else // curNode is null
				{
					curNode = stk.Peek();
					// If there exists no right sub-tree or it's root has been visited
					if (curNode.Right == null || curNode.Right == prevNode)
					{
						PrintNode(curNode);
						stk.Pop(); // Backtrack
						prevNode = curNode; // Mark sub-tree rooted at 'curNode' as visited
						curNode = null; // Move onto next node on stk
					}
					// Visit right subtree
					else curNode = curNode.Right;
				}
			}
		}

		// Iterative using no stack
		private static BinNode<T> FindInOrderPredecessor(BinNode<T> node)
		{
			/* Finds and returns the in order predecessor for the given node.
			 * Assumes curNode has a left sub-tree.
			 * The in order predecessor of a node is the rightmost node appearing in the
			 * given node's left sub-tree, and is also the maximal value for a BST. */

			BinNode<T> curNode = node.Left;
			// Dive right while curNode's right sub-tree is not null or links back to node
			while (curNode.Right != null && curNode.Right != node) curNode = curNode.Right;
			// return the in order predecessor
			return curNode;
		}
		public static void PrintPreOrderItrMorris(BinNode<T> node)
		{
			BinNode<T> curNode = node;
			while (curNode != null)
			{
				if (curNode.Left == null)
				{
					PrintNode(curNode);
					curNode = curNode.Right;
				}
				else
				{
					BinNode<T> predecessor = FindInOrderPredecessor(curNode);
					if (predecessor.Right == null) // Predecessor's right does not link back to curNode
					{
						PrintNode(curNode);
						// Link predecessor to curNode (a way to come back from the predecessor to curNode)
						predecessor.Right = curNode;
						curNode = curNode.Left;
					}
					else // Predecessor's right links back to curNode
					{
						predecessor.Right = null; // Unlink predecessor from curNode
						curNode = curNode.Right;
					}
				}
			}
		}
		public static void PrintInOrderItrMorris(BinNode<T> node)
		{
			BinNode<T> curNode = node;
			while (curNode != null)
			{
				if (curNode.Left == null)
				{
					PrintNode(curNode);
					curNode = curNode.Right;
				}
				else
				{
					BinNode<T> predecessor = FindInOrderPredecessor(curNode);
					if (predecessor.Right == null) // Predecessor's right does not link back to curNode
					{
						// Link predecessor to curNode (a way to come back from the predecessor to curNode)
						predecessor.Right = curNode;
						curNode = curNode.Left;
					}
					else // Predecessor's right links back to curNode
					{
						PrintNode(curNode);
						predecessor.Right = null; // Unlink predecessor from curNode
						curNode = curNode.Right;
					}
				}
			}
		}
		#endregion

		#region BFS
		// Breadth first searches (level order)
		public static void PrintLevelOrder(BinNode<T> node)
		{
			Queue<BinNode<T>> q = new Queue<BinNode<T>>();
			q.Enqueue(node);
			while (q.Count > 0)
			{
				BinNode<T> curNode = q.Dequeue();
				if (curNode != null)
				{
					PrintNode(curNode);
					if (curNode.Left != null) q.Enqueue(curNode.Left);
					if (curNode.Right != null) q.Enqueue(curNode.Right);
				}
			}
		}
		public static void PrintReverseLevelOrder(BinNode<T> node)
		{
			Queue<BinNode<T>> q = new Queue<BinNode<T>>();
			Stack<BinNode<T>> stk = new Stack<BinNode<T>>();
			q.Enqueue(node);
			// Traverse the tree from top to bot, right to left and push values into stk.
			while (q.Count > 0)
			{
				BinNode<T> curNode = q.Dequeue();
				if (curNode != null)
				{
					stk.Push(curNode);
					if (curNode.Right != null) q.Enqueue(curNode.Right);
					if (curNode.Left != null) q.Enqueue(curNode.Left);
				}
			}
			// By poping & printing values from stk the printing order is bot to top, left to right
			while (stk.Count > 0) PrintNode(stk.Pop());
		}
		public static void PrintLevelByLevelOrder(BinNode<T> node)
		{
			// Print each level of the tree in a seperate line
			// null values are used as delimiters between levels of the tree
			Queue<BinNode<T>> q = new Queue<BinNode<T>>();
			q.Enqueue(node);
			q.Enqueue(null);
			while (q.Count > 0)
			{
				BinNode<T> curNode = q.Dequeue();
				if (curNode != null)
				{
					PrintNode(curNode);
					if (curNode.Left != null) q.Enqueue(curNode.Left);
					if (curNode.Right != null) q.Enqueue(curNode.Right);
				}
				// Reached a null delimiter, add a line break if not the last level (q non empty)
				else if (q.Count > 0)
				{
					Console.WriteLine();
					q.Enqueue(null);
				}
			}
		}
		#endregion

		#region Other
		private static void PrintNode(BinNode<T> node) => Console.Write(node.Data + " ");
		
		public static int Size(BinNode<T> node)
		{
			if (node == null) return 0;
			return 1 + Size(node.Left) + Size(node.Right);
		}

		public static int Height(BinNode<T> node)
		{
			if (node == null) return -1;
			return 1 + Math.Max(Height(node.Left), Height(node.Right));
		}
		
		public static void Invert(BinNode<T> node)
		{
			if (node == null) return;
			BinNode<T> tmp = node.Left;
			node.Left = node.Right;
			node.Right = tmp;
			Invert(node.Left);
			Invert(node.Right);
		}
		
		public static void InvertItr(BinNode<T> node)
		{
			Stack<BinNode<T>> stk = new Stack<BinNode<T>>();
			stk.Push(node);
			while(stk.Count > 0)
			{
				BinNode<T> curNode = stk.Pop();
				if (curNode != null)
				{
					BinNode<T> tmp = curNode.Left;
					curNode.Left = curNode.Right;
					curNode.Right = tmp;
					stk.Push(curNode.Left);
					stk.Push(curNode.Right);
				}
			}
		}

		// Tree construction from 2 given traversals
		private static int preStart;
		public static BinNode<T> TreeFromInAndPreOrderTrav(T[] inOrder, T[] preOrder)
		{
			preStart = 0;
			return TreeFromInAndPreOrderTrav(inOrder, preOrder, 0, inOrder.Length - 1);
		}
		private static BinNode<T> TreeFromInAndPreOrderTrav(T[] inOrder, T[] preOrder, int inStart, int inEnd)
		{
			// out of bounds
			if (inStart > inEnd) return null;
			BinNode<T> node = new BinNode<T>(preOrder[preStart++]);
			// leaf node
			if (inStart == inEnd) return node;

			int inIdx = FindIdx(inOrder, inStart, inEnd, node.Data);

			node.Left = TreeFromInAndPreOrderTrav(inOrder, preOrder, inStart, inIdx - 1);
			node.Right = TreeFromInAndPreOrderTrav(inOrder, preOrder, inIdx + 1, inEnd);
			return node;
		}
		
		private static int postEnd;
		public static BinNode<T> TreeFromInAndPostOrderTrav(T[] inOrder, T[] postOrder)
		{
			postEnd = postOrder.Length - 1;
			return TreeFromInAndPostOrderTrav(inOrder, postOrder, 0, inOrder.Length - 1);
		}
		private static BinNode<T> TreeFromInAndPostOrderTrav(T[] inOrder, T[] postOrder, int inStart, int inEnd)
		{
			// out of bounds
			if (inStart > inEnd) return null;
			BinNode<T> node = new BinNode<T>(postOrder[postEnd--]);
			// leaf node
			if (inStart == inEnd) return node;

			int inIdx = FindIdx(inOrder, inStart, inEnd, node.Data);

			node.Right = TreeFromInAndPostOrderTrav(inOrder, postOrder, inIdx + 1, inEnd);
			node.Left = TreeFromInAndPostOrderTrav(inOrder, postOrder, inStart, inIdx - 1);
			return node;
		}
		
		public static BinNode<T> TreeFromInAndLevelOrderTrav(T[] inOrder, T[] levelOrder)
		{
			return TreeFromInAndLevelOrderTrav(inOrder, levelOrder, 0, inOrder.Length - 1);
		}
		private static BinNode<T> TreeFromInAndLevelOrderTrav(T[] inOrder, T[] levelOrder, int inStart, int inEnd)
		{
			// out of bounds
			if (inStart > inEnd) return null;

			BinNode<T> node = null;
			int inIdx = -1;
			bool found = false;
			for(int i = 0; i < levelOrder.Length && !found; i++)
			{
				inIdx = FindIdx(inOrder, inStart, inEnd, levelOrder[i]);
				if (inIdx != -1)
				{
					node = new BinNode<T>(levelOrder[i]);
					found = true;
				}
			}
			// leaf node
			if (inStart == inEnd) return node;

			node.Left = TreeFromInAndLevelOrderTrav(inOrder, levelOrder, inStart, inIdx - 1);
			node.Right = TreeFromInAndLevelOrderTrav(inOrder, levelOrder, inIdx + 1, inEnd);
			return node;
		}
		
		private static int FindIdx(T[] arr, int start, int end, T val)
		{
			// Returns the index of 'val' in 'arr' if present, else -1.
			if (start > end) return -1;
			for (int i = start; i <= end; i++)
				if (arr[i].Equals(val)) return i;
			return -1;
		}
		#endregion
	}
}
