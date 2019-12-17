using System;
using System.Collections.Generic;
namespace DsBasic
{
    public class BinaryTree
    {
        public char Value { get; set; }
        public BinaryTree Left { get; set; }
        public BinaryTree Right { get; set; }
        /// <summary>
        /// construct a tree
        /// </summary>
        public BinaryTree()
        {

        }
        /// <summary>
        /// Add a Node with Value
        /// </summary>
        /// <param name="value">Node Value</param>
        public BinaryTree(char value)
        {
            Value = value;
        }
        public BinaryTree(char value, BinaryTree left = null, BinaryTree right = null)
        {
            Value = value;
            Left = left;
            Right = right;
        }
        public static BinaryTree ConstructBinaryTree()
        {
            /*
                      A
                  B      C
                D   E       F
                   G
                     H
            */
            BinaryTree root = new BinaryTree('A');
            var left = root.Left = new BinaryTree('B');
            root.Right = new BinaryTree('C', null, new BinaryTree('F'));
            left.Left = new BinaryTree('D');
            left.Right = new BinaryTree('E', new BinaryTree('G', null, new BinaryTree('H')), null);
            return root;
        }

        private List<char> Order;
        private void PreOrder(BinaryTree root)
        {
            if (root == null)
            {
                return;
            }
            Order.Add(root.Value);
            PreOrder(root.Left);
            PreOrder(root.Right);
        }
        public char[] GetPreOrder()
        {
            Order = new List<char>();
            PreOrder(this);
            return Order.ToArray();

        }
        private void InOrder(BinaryTree root)
        {
            if (root == null)
            {
                return;
            }
            InOrder(root.Left);
            Order.Add(root.Value);
            InOrder(root.Right);
        }
        public char[] GetInOrder()
        {
            Order = new List<char>();
            InOrder(this);
            return Order.ToArray();
        }
        private void PostOrder(BinaryTree root)
        {
            if (root == null)
            {
                return;
            }
            PostOrder(root.Left);
            PostOrder(root.Right);
            Order.Add(root.Value);
        }
        public char[] GetPostOrder()
        {
            Order = new List<char>();
            PostOrder(this);
            return Order.ToArray();
        }




    }
    public class OriginTree
    {
        public char Value { get; set; }
        public IList<OriginTree> MyProperty { get; set; }
    }
}
