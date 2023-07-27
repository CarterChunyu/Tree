using System;
using System.Collections.Generic;

namespace DataStructure
{
    class BST1<E> where E : IComparable<E>
    {
        private class Node
        {
            public E e;
            public Node left;
            public Node right;
            public Node(E e)
            {
                this.e = e;
                left = null;
                right = null;
            }
        }
        private Node root;
        private int N;

        public BST1()
        {
            root = null;
            N = 0;
        }
        public int Count { get { return N; } }
        public bool IsEmptry { get { return N == 0; } }

        // 非遞規
        public void add(E e)
        {
            if (root == null) // 二叉查找樹為空
            {
                root = new Node(e);
                N++;
            }
            Node cur = root;
            Node pre = null;
            while (cur != null)
            {
                if (cur.e.CompareTo(e) == 0) // 二叉查找樹不能有重複
                    return;
                pre = cur;
                if (cur.e.CompareTo(e) < 0)
                    cur = cur.right;
                else
                    cur = cur.left;
            }
            cur = new Node(e);
            if (pre.e.CompareTo(e) < 0)
                pre.right = cur;
            else
                pre.left = cur;
            N++;
        }

        public void Add(E e)
        {
            this.root = Add(this.root, e); // 注意 雖然node是參考型別傳址, 但是一開始root 是null
        }
        // 以node為根的二叉查找樹中添加元素, 添加後返回為根節點node
        private Node Add(Node node, E e)
        {
            if (node == null)
            {
                N++;
                return new Node(e);
            }
            if (e.CompareTo(node.e)<0)
                node.left = Add(node.left, e);
            else if (e.CompareTo(node.e)>0)
                node.right = Add(node.right, e);
            return node;
        }

        public bool Contains(E e)
        {
            return Contains(root, e);
        }
        // 以node為根的二叉查找樹是否包含元素e
        private bool Contains(Node node, E e)
        {
            if (node == null)
                return false;
            if (node.e.CompareTo(e) == 0)
                return true;
            else if (node.e.CompareTo(e) < 0)
                return Contains(node.right, e);
            else // node.e.CompareTo(e) > 0
                return Contains(node.left, e);
        }

        // 前序遍歷
        public void PreOrder()
        {
            PreOrder(root);
        }
        // 前序遍歷以node為根的二叉查找樹
        private void PreOrder(Node node)
        {
            if (node == null)
                return;
            Console.WriteLine(node.e);
            PreOrder(node.left);
            PreOrder(node.right);
        }
        // 中序遍歷
        public void InOrder()
        {
            InOrder(root);
        }
        // 中序遍歷以node為根的二叉查找樹
        private void InOrder(Node node)
        {
            if (node == null)
                return;
            InOrder(node.left);
            Console.WriteLine(node.e);
            InOrder(node.right);
        }
        // 層序遍歷(廣度優先)
        public void LevelOrder()
        {
            Queue<Node> queue = new Queue<Node>();
            if (this.root != null)
                queue.Enqueue(root);
            while (queue.Count > 0)
            {
                Node cur = queue.Dequeue();
                Console.WriteLine(cur.e);
                if (cur.left != null)
                    queue.Enqueue(cur.left);
                if (cur.right != null)
                    queue.Enqueue(cur.right);
            }
        }
        //public void LevelOrder()
        //{
        //    LevelOrder(this.root);
        //}
        //private void LevelOrder(Node node)
        //{
        //    Queue<Node> queue = new Queue<Node>();
        //    if(node!=null)
        //        queue.Enqueue(node);
        //    while (queue.Count > 0)
        //    {
        //        Node cur = queue.Dequeue();
        //        Console.WriteLine(cur.e);
        //        if (node.left != null)
        //            queue.Enqueue(node.left);
        //        if (node.right != null)
        //            queue.Enqueue(node.right);
        //    }
        //}

        public E Min()
        {
            if (IsEmptry)
                throw new ArgumentException("空樹! ");
            return Min(this.root).e;
        }
        // 返回以node為根的二叉查找樹的最小值
        private Node Min(Node node)
        {
            if (node.left == null)
                return node;
            return Min(node.left);
        }

        public E Max()
        {
            if (IsEmptry)
                throw new ArgumentException("空樹! ");
            return Max(this.root).e;
        }
        // 返回以node為根的二叉查找樹的最大值
        private Node Max(Node node)
        {
            if (node.right != null)
                return node;
            return Max(node.left);
        }

        public E RemoveMin()
        {
            E e = Min();
            this.root = RemoveMin(this.root);
            return e;
        }
        // 刪除掉以node為根的二叉查找樹中的最小節點
        // 返回刪除最小節點後新的二叉查找樹的根
        private Node RemoveMin(Node node)
        {
            if (node.left == null)
            {
                N--;
                return node.right;
            }

            node.left = RemoveMin(node.left);
            return node;
        }

        public E RemoveMax()
        {
            E e = Max();
            this.root = RemoveMax(this.root);
            return e;
        }
        // 刪除掉以node為根的二叉查找樹中的最大節點
        // 返回刪除最大節點後新的二叉查找樹的根
        private Node RemoveMax(Node node)
        {
            if (node.right == null)
            {
                N--;
                return node.left;
            }
            node.right = RemoveMax(node.right);
            return node;
        }
        public void Remove(E e)
        {
            this.root = Remove(this.root, e);
        }
        // 刪除掉以node為根的二叉查找樹中值為e的節點
        // 返回刪除節點後新的二叉查找樹的根
        private Node Remove(Node node, E e)
        {
            if (node == null)
                return node;

            if (e.CompareTo(node.e) < 0)
            {
                node.left = Remove(node.left, e);
                return node;
            }
            else if (e.CompareTo(node.e) > 0)
            {
                node.right = Remove(node.right, e);
                return node;
            }
            else //e.CompareTo(node.e)==0
            {
                N--;
                if (node.left == null)
                    return node.right;
                else if (node.right == null)
                    return node.left;
                // 要刪除的節點左右都有孩子
                // 找到要比待刪除節點的最小節點, 即待刪除節點的右子樹的最小節點
                // 用這個節點頂替代刪除節點的位置
                else
                {
                    Node s = Min(node.right);
                    s.right = RemoveMin(node.right);
                    s.left = node.left;
                    return s;
                }
            }
        }
        public int MaxHeight()
        {
            return MaxHeight(this.root);
        }
        private int MaxHeight(Node node)
        {
            if (node == null)
                return 0;
            int l = MaxHeight(node.left);
            int r = MaxHeight(node.right);
            return Math.Max(l, r) + 1;
        }
        //  排名: 找出小於指定鍵的數量
        public int Rank(E e)
        {
            return Rank(this.root, e);
        }
        private int Rank(Node node, E e)
        {
            if (node == null)
                return 0;
            if (e.CompareTo(node.e) <= 0)  // 如果目標小於等於目前比較結點, 繼續往比較結點左子數找
                return Rank(node.left, e);
            else // e.CompareTo(node.e)>0 // 目標大於目前比較結點
            {
                // 往目標左子樹找 + 往目標右子樹找 + 目標結點  
                return Rank(node.left, e) + Rank(node.right, e) + 1;
            }
        }
        // 找出小於或等於key的最小鍵
        public E Floor(E e)
        {
            Node f = Floor(this.root, e);
            if (f == null)
                throw new ArgumentException($"找不到小於或等於{e}的鍵");
            return f.e;
        }
        private Node Floor(Node node, E e)
        {
            if (node == null)
                return null;
            if (e.CompareTo(node.e) <= 0)
                return Floor(node.left, e);
            else // e.CompareTo(node.e)>0
            {
                Node r = Floor(node.right, e);
                return r != null ? r : node;
            }
        }

        // 找出大於或等於key的最小鍵
        public E Ceiling(E e)
        {
            Node c = Ceiling(this.root, e);
            if (c == null)
                throw new ArgumentException($"找不到大於或等於{e}的鍵");
            return c.e;
        }
        private Node Ceiling(Node node, E e)
        {
            if (node == null)
                return null;
            if (e.CompareTo(node.e) == 0)
                return node;
            else if (e.CompareTo(node.e) > 0)
                return Ceiling(node.right, e);
            else // e.CompareTo(node.e)<0
            {
                Node r = Ceiling(node.left, e);
                return r != null ? r : node;
            }
        }
        public E Select(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new ArgumentException("超過樹的邊界~!");
            return Select(this.root, index).e;
        }
        private Node Select(Node node, int index)
        {
            int node_index = Rank(node.e);
            if (node_index == index)
                return node;
            else if (node_index < index)
                return Select(node.left, index);
            else //node_index>index
                return Select(node.right, index);
        }


        // 一開始的寫法(想太複雜)
        //public E Select(int index)
        //{
        //    if (index < 0 || index >= Count)
        //        throw new ArgumentException("超越邊界");
        //    return Select(this.root, index + 1).e;
        //}
        //private Node Select(Node node, int indexPlus1)
        //{
        //    int nodeCount = NodeCount(this.root, node);
        //    if (nodeCount == indexPlus1)
        //        return node;
        //    else if (nodeCount > indexPlus1)
        //        return Select(node.left, indexPlus1);
        //    else // nodeCount<indexPlus1
        //        return Select(node.right, indexPlus1);
        //}

        //private int NodeCount(Node root, Node node)
        //{
        //    if (node.e.CompareTo(root.e) == 0)
        //        return NodeCount(node);
        //    else if (node.e.CompareTo(root.e) < 0)
        //        return NodeCount(root.left, node);
        //    else // node.e.CompareTo(root.e)>0
        //        return NodeCount(root, root) + NodeCount(root.right, node);
        //}
        //private int NodeCount(Node node)
        //{
        //    if (node == null)
        //        return 0;
        //    return FullNodeCount(node.left) + 1;
        //}
        //private int FullNodeCount(Node node)
        //{
        //    if (node == null)
        //        return 0;
        //    int l = FullNodeCount(node.left);
        //    int r = FullNodeCount(node.right);
        //    return l + r + 1;
        //}
    }
}
