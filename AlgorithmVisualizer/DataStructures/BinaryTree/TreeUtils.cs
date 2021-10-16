using System;
using System.Collections.Generic;


namespace AlgorithmVisualizer.DataStructures.BinaryTree
{

	/* IMPORTANT 
	 * Tree construction methods (pre/post/level + in) can be further optimized!
	 * Consider using Dictionaries instead of arrays for O(1) value lookups!
	 */


	public class TreeUtils<T> where T : IComparable
	{
		// Depth first traversals (Recursive):
		public static void PrintPreOrderRec(BinNode<T> node)
		{
			if (node == null) return;
			Console.Write(node.Data + " ");
			PrintPreOrderRec(node.Left);
			PrintPreOrderRec(node.Right);
		}
		public static void PrintInOrderRec(BinNode<T> node)
		{
			if (node == null) return;
			PrintInOrderRec(node.Left);
			Console.Write(node.Data + " ");
			PrintInOrderRec(node.Right);
		}
		public static void PrintPostOrderRec(BinNode<T> node)
		{
			if (node == null) return;
			PrintPostOrderRec(node.Left);
			PrintPostOrderRec(node.Right);
			Console.Write(node.Data + " ");
		}
		
		// Depth first traversals (Iterative using a stack):
		public static void PrintPreOrderItr(BinNode<T> node)
		{
			// Stack to simulate the recusion call stack, initally holds root node
			Stack<BinNode<T>> stk = new Stack<BinNode<T>>();
			stk.Push(node);
			// As long as the stack is not empty
			while(stk.Count > 0)
			{
				// pop from stk
				BinNode<T> curNode = stk.Pop();
				// popped value is not null
				if(curNode != null)
				{
					// print the value, push right and then left into the stack if not null
					Console.Write(curNode.Data + " ");
					if (curNode.Right != null) stk.Push(curNode.Right);
					if (curNode.Left != null) stk.Push(curNode.Left);
				}
			}
		}
		public static void PrintInOrderItr(BinNode<T> node)
		{
			// Stack to simulate the recusion call stack, curNode to hold a node
			BinNode<T> curNode = node;
			Stack<BinNode<T>> stk = new Stack<BinNode<T>>();
			// As long as the stack is not empty or currenley held node is not null
			while (stk.Count > 0 || curNode != null)
			{
				// Dive left as long as curNode is not null
				while(curNode != null)
				{
					stk.Push(curNode);
					curNode = curNode.Left;
				}
				// curNode must be null at this point, stk's top holds the leftmost node of the current sub-tree, print it
				curNode = stk.Pop();
				Console.Write(curNode.Data + " ");
				// Visit curNode's right sub-tree (may not exist)
				curNode = curNode.Right;
			}
		}
		public static void PrintPostOrderItr(BinNode<T> node)
		{
			BinNode<T> curNode = node, prevNode = null;
			Stack<BinNode<T>> stk = new Stack<BinNode<T>>();
			while(stk.Count > 0 || curNode != null)
			{
				// if curNode is not null, push it into stk and visit left sub-tree
				if(curNode != null)
				{
					stk.Push(curNode);
					curNode = curNode.Left;
				}
				// curNode is null
				else
				{
					curNode = stk.Peek();
					// If there exists no right sub-tree or it's root has been visited (prevNode holds it)
					if (curNode.Right == null || curNode.Right == prevNode)
					{
						Console.Write(curNode.Data + " "); // Print the node
						stk.Pop(); // Backtrack
						// mark prevNode as the currentley visisted node, and curNode as null
						prevNode = curNode;
						curNode = null;
					}
					// Visit right subtree
					else curNode = curNode.Right;
				}
			}
		}

		// Depth first traversals (Iterative using no stack):
		private static BinNode<T> FindInOrderPredecessor(BinNode<T> node)
		{
			// Move onto curNode's left sub-tree, assumes curNode has a left sub-tree!
			// Note that the predecessor is the maximal value of curNode's left sub-tree.
			BinNode<T> curNode = node.Left;
			// As long as the next node is not not (empty sub-tree) or the root node, dive right
			while (curNode.Right != null && curNode.Right != node) curNode = curNode.Right;
			// return resulting predecessor
			return curNode;
		}
		public static void PrintPreOrderItrMorris(BinNode<T> node)
		{
			BinNode<T> curNode = node;
			// As long as curNode is not null
			while (curNode != null)
			{
				// If curNode has no left sub-tree
				if (curNode.Left == null)
				{
					Console.Write(curNode.Data + " "); // Print the node
					curNode = curNode.Right; // Move into the right sub-tree
				}
				else
				{
					// Find curNode's in order predecessor, that is the max value in the left sub-tree
					// Note that curNode has a left sub-tree (non null), and that the in order predecessor may point back to curNode!
					BinNode<T> predecessor = FindInOrderPredecessor(curNode);
					if(predecessor.Right == null) // predecessor's right is null (not linked to curNode)
					{
						Console.Write(curNode.Data + " "); // Print the node
						predecessor.Right = curNode; // Link predecessors right to curNode
						curNode = curNode.Left; // Established a way to go back to curNode, move into curNode's left sub-tree
					}
					else // predecessor's right is not null (is linked to curNode)
					{
						predecessor.Right = null; // Unlink predecessor from curNode
						curNode = curNode.Right; // Move into the right sub-tree
					}
				}
			}
		}
		public static void PrintInOrderItrMorris(BinNode<T> node)
		{
			BinNode<T> curNode = node;
			// As long as curNode is not null
			while (curNode != null)
			{
				// If curNode has no left sub-tree
				if (curNode.Left == null)
				{
					Console.Write(curNode.Data + " "); // Print the node
					curNode = curNode.Right; // Move into the right sub-tree
				}
				else
				{
					// Find curNode's in order predecessor, that is the max value in the left sub-tree
					// Note that curNode has a left sub-tree (non null), and that the in order predecessor may point back to curNode!
					BinNode<T> predecessor = FindInOrderPredecessor(curNode);
					if (predecessor.Right == null) // predecessor's right is null (not linked to curNode)
					{
						predecessor.Right = curNode; // link predecessor's right to curNode	  
						curNode = curNode.Left; // Established a way to go back to curNode, move into curNode's left sub-tree
					}
					else // predecessor's right is not null (is linked to curNode)
					{
						Console.Write(curNode.Data + " "); // Print the node
						predecessor.Right = null; // Unlink predecessor from curNode
						curNode = curNode.Right; // Move into the right sub-tree
					}
				}
			}
		}

		// Breadth first traversals:
		public static void PrintLevelOrder(BinNode<T> node)
		{
			// Queue used to traverse the tree in a level order fashion, initially holds root node
			Queue<BinNode<T>> q = new Queue<BinNode<T>>();
			q.Enqueue(node);
			while (q.Count > 0)
			{
				BinNode<T> curNode = q.Dequeue();
				if (curNode != null)
				{
					Console.Write(curNode.Data + " "); // print the node
					// Add left & right if they are not null
					if (curNode.Left != null)
						q.Enqueue(curNode.Left);
					if (curNode.Right != null)
						q.Enqueue(curNode.Right);
				}
			}
		}
		public static void PrintReverseLevelOrder(BinNode<T> node)
		{
			// Queue used to traverse the tree in a level order fashion, initially holds root node
			Queue<BinNode<T>> q = new Queue<BinNode<T>>();
			// The stack is used to reverse the order of traversal
			Stack<BinNode<T>> stk = new Stack<BinNode<T>>();
			q.Enqueue(node);
			// As long as the queue is not empty
			while (q.Count > 0)
			{
				BinNode<T> curNode = q.Dequeue();
				if (curNode != null)
				{
					// Push curNode into the stack
					stk.Push(curNode);
					// Add right & left if they are not null
					// Note that the visiting order is reversed compared to a level order!
					if (curNode.Right != null) q.Enqueue(curNode.Right);
					if (curNode.Left != null) q.Enqueue(curNode.Left);
				}
			}
			// The former traversal using the queue forms a zig-zag from top to bot, right to left.
			// If we reverse the traversal we get a zig-zag from bot to top, left to right, exactly what we needed.
			while(stk.Count > 0) Console.Write(stk.Pop() + " "); // print the node
		}
		public static void PrintLevelByLevelOrder(BinNode<T> node)
		{
			// Queue used to traverse the tree in a level order fashion, initially holds root node
			// Additionally the queue will hold null nodes to indicate level advancement,
			// In other words null nodes are level delimiters
			Queue<BinNode<T>> q = new Queue<BinNode<T>>();
			q.Enqueue(node);
			q.Enqueue(null); // Add the first delimiter
			while (q.Count > 0)
			{
				BinNode<T> curNode = q.Dequeue();
				if (curNode != null)
				{
					Console.Write(curNode.Data + " "); // print the node
					// Add left & right if they are not null
					// Please note that in this solution adding null nodes is not only inefficient,
					// but will also lead to an infinite loop!
					if (curNode.Left != null) q.Enqueue(curNode.Left);
					if (curNode.Right != null) q.Enqueue(curNode.Right);
				}
				// curNode holds the null delimiter, level finished
				// If the queue is not empty, there are still unfinished levels.
				else if (q.Count > 0)
				{
					Console.WriteLine();
					q.Enqueue(null);
				}
			}
		}

		// Generic
		public static int Size(BinNode<T> node)
		{
			// If tree is empty (null) return 0
			if (node == null) return 0;
			// Return 1 + sizes of left/right sub-trees
			return 1 + Size(node.Left) + Size(node.Right);
		}
		public static int Height(BinNode<T> node)
		{
			// The height of an empty sub-tree is -1
			if (node == null) return -1;
			// Return 1 + the Maximal height between left and right sub-trees
			return 1 + Math.Max(Height(node.Left), Height(node.Right));
		}
		public static void Invert(BinNode<T> node)
		{
			if (node == null) return;
			// Swap left/right sub-tree pointers
			BinNode<T> tmp = node.Left;
			node.Left = node.Right;
			node.Right = tmp;
			// Repeat for left/right sub-trees
			Invert(node.Left);
			Invert(node.Right);
		}
		public static void InvertItr(BinNode<T> node)
		{
			Stack<BinNode<T>> stk = new Stack<BinNode<T>>();
			stk.Push(node);
			while(stk.Count > 0)
			{
				BinNode<T> cur = stk.Pop();
				if (cur != null)
				{
					// Swap left and right children
					var tmp = cur.Left;
					cur.Left = cur.Right;
					cur.Right = tmp;
					// Adding children into stk
					foreach (var child in new BinNode<T>[] { cur.Left, cur.Right })
						stk.Push(child);
				}
			}
		}

		// Tree generators
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
		
		// Tree generator helper - find element's index in given bounds of given array
		private static int FindIdx(T[] arr, int start, int end, T val)
		{
			if (start > end) return -1;
			for (int i = start; i <= end; i++)
				if (arr[i].Equals(val)) return i;
			return -1;
		}
	}
}
