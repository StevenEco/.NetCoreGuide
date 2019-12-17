using System;
using System.Collections.Generic;
using DsBasic;

namespace dbfs1
{
    class Program
    {
        private void DFSTree()
        {
            Stack<BinaryTree> stack = new Stack<BinaryTree>();
            var root = BinaryTree.ConstructBinaryTree();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var cur = stack.Pop();
                if (cur.Right != null)
                {
                    stack.Push(cur.Right);
                }
                if (cur.Left != null)
                {
                    stack.Push(cur.Left);
                }
                Console.Write(cur.Value);
            }
            Console.WriteLine();
        }
        /// <summary>
        /// 递归
        /// </summary>
        private void DFSTree1(BinaryTree root)
        {
            if (root == null)
                return;
            Console.Write(root.Value);
            DFSTree1(root.Left);
            DFSTree1(root.Right);
        }

        private void BFSTree()
        {
            var root = BinaryTree.ConstructBinaryTree();
            Queue<BinaryTree> queue = new Queue<BinaryTree>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();
                Console.Write(cur.Value);
                if (cur.Left != null)
                    queue.Enqueue(cur.Left);
                if (cur.Right != null)
                    queue.Enqueue(cur.Right);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            var root = BinaryTree.ConstructBinaryTree();
            var preOrder = root.GetPreOrder();
            var inOrder = root.GetInOrder();
            var broot = p.BuildeTree1(preOrder, inOrder);
            var preOrder1 = broot.GetPreOrder();
            var inOrder1 = broot.GetInOrder();
            for (int i = 0; i < preOrder.Length; i++)
            {
                Console.Write(preOrder[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < preOrder1.Length; i++)
            {
                Console.Write(preOrder1[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < inOrder.Length; i++)
            {
                Console.Write(inOrder[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < inOrder1.Length; i++)
            {
                Console.Write(inOrder1[i]);
            }
        }
        public BinaryTree BuildeTree1(char[] preOrder, char[] inOrder)
        {
            return SubBuilder(0, preOrder.Length - 1, 0, inOrder.Length - 1, preOrder, inOrder);
        }
        private BinaryTree SubBuilder(int pL, int pR, int iL, int iR, char[] preOrder, char[] inOrder)
        {
            if (pL > iL || pR > iR)
                return null;
            var subRoot = new BinaryTree(preOrder[pL]);
            int rootPos_InOrder = iL;
            while (rootPos_InOrder < iR && inOrder[rootPos_InOrder] != subRoot.Value)
            {
                rootPos_InOrder++;
            }
            int left = rootPos_InOrder - iL;
            subRoot.Left = SubBuilder(pL + 1, pL + left, iL, rootPos_InOrder, preOrder, inOrder);
            subRoot.Right = SubBuilder(pL + left + 1, pR, rootPos_InOrder + 1, iR, preOrder, inOrder);
            return subRoot;
        }


    }
}
