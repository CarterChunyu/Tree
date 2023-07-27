using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    
    // 2-3樹: 絕對平衡的樹(任意空結點到根結點所經過的結點數量都一樣)
    /*
       左傾的紅黑樹必須滿足以下條件:
       1. 紅結點必為左結點
     
       2. 沒有任何一個結點同時連接兩個紅結點
     
       3. 樹是完美的黑色平衡, 及空節點到根結點路徑上經過黑結點的數量是相同的

       4. 新加入的結點均為紅結點

       5. 根結點是黑色的
     */
    class RBT1<E> where E:IComparable<E>
    {
        private const bool Red = true;
        private const bool Black = false;
        private class Node
        {
            public E e;
            public Node left;
            public Node right;
            public bool color;
            public Node(E e)
            {
                this.e = e;
                this.left = null;
                this.right = null;
                this.color = Red;
            }
        }
        private int N;
        private Node root;
        public int Count { get { return N; } }
        public bool IsEmpty { get { return N == 0; } }

        public RBT1()
        {
            this.N = 0;
            this.root = null;
        }
        private bool IsRed(Node node)
        {   
            if (node == null) // 空結點為黑色結點
                return Black;
            return node.color;
        }
       
        //  如果出現右結點為紅色而左結點是黑色( 空結點為黑色), 進行左旋轉操作
        
        //    node                     x
        //    /  \      左旋轉        / \
        //   T1   x    -------->   node  T3
        //       / \               /  \
        //      T2 T3             T1  T2

        // 產生右子結點為紅色的狀況1: 新加入的右子結點,T2、T3 皆為空, 亦即黑色結點 
        // 狀況2: 顏色翻轉, 因為右子結點為紅色會進行左旋轉, 不會繼續添加新的=結點
        // 所以T2、T3 皆為黑色, 不用擔心node 需要在做左旋
        private Node LeftRotate(Node node)
        {
            Node x = node.right;
            node.right = x.left;
            x.left = node;
            x.color = node.color;
            node.color = Red;
            return x;
        }
        // 如果出現連續兩個紅色的左子結點, 進行右旋轉操作

        //     node                  x
        //     /  \    右旋轉       / \
        //    x   T3  -------->    T1  node
        //   / \                       /  \
        //  T1 T2                     T2  T3

        // 右旋過後T1 及node 皆為紅色, x 會進行顏色翻轉
        private Node RightRotate(Node node)
        {
            Node x = node.left;
            node.left = x.right;
            x.right = node;
            x.color = node.color;
            node.color = Red;
            return x;
        }
        // 如果左右子結點皆為紅色, 進行顏色翻轉
        private void FlipColor(Node node)
        {
            node.left.color = Black;
            node.right.color = Black;
            node.color = Red;
        }
        // 往紅黑樹中添加元素, 遞歸實現
        public void Add(E e)
        {
            this.root =Add(this.root, e);
            this.root.color = Black;
        }
        // 以node為根的樹中添加元素e, 添加後返回根結點node
        private Node Add(Node node,E e)
        {
            if (node == null)
            {
                N++;
                return new Node(e); // 默認為紅結點
            }
            if (e.CompareTo(node.e) < 0)
                node.left = Add(node.left, e);
            else if (e.CompareTo(node.e) > 0)
                node.right = Add(node.right, e);
            // 如果出現右子結點是紅色, 而左子結點是黑色( 空結點為黑色), 進行左旋轉
            if (IsRed(node.right)&&!IsRed(node.left))  
                node = LeftRotate(node);
            // 如果出現連續的左子結點都為紅色, 進行右旋轉
            if (IsRed(node.left) && IsRed(node.left.left))
                node = RightRotate(node);
            // 如果左右子結點均為紅色, 進行顏色翻轉
            if (IsRed(node.left) && IsRed(node.right))
                FlipColor(node);
            return node;
        }
        // 查詢紅黑樹是否包含元素e
        public bool Contains(E e)
        {
            return Contains(this.root,e);
        }
        // 以node為根的樹是否包含元素e
        private bool Contains(Node node,E e)
        {
            if (node == null)
                return false;
            if (e.CompareTo(node.e) == 0)
                return true;
            else if (e.CompareTo(node.e) < 0)
                return Contains(node.left, e);
            else // e.CompareTo(node.e)>0  
                return Contains(node.right, e);
        }
        // 紅黑樹的最大高度
        public int MaxHeight()
        {
           return  MaxHeight(this.root);
        }
        // 計算以node為根的二叉樹的最大高度
        private int MaxHeight(Node node)   
        {
            if (node == null)
                return 0;
            //int l = MaxHeight(node.left);
            //int r = MaxHeight(node.right);
            //return Math.Max(l, r) + 1;

            // 選擇左右子樹中最高的那顆子樹在上node本身的高度。 得到以node為根紅黑樹的最大高度。
            return Math.Max(MaxHeight(node.left), MaxHeight(node.right)) + 1;
        }
    }
}
