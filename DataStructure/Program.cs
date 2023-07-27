using System;


namespace DataStructure
{
    class Program
    {
        static void Main(string[] args)
        {

            // 紅黑樹
            {
                int[] a = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                RBT1<int> rBT1 = new RBT1<int>();
                for (int i = 0; i < a.Length; i++)
                {
                    rBT1.Add(a[i]);
                }
                Console.WriteLine(rBT1.MaxHeight());
            }
        }
    }
}
