using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal class Knapsack
    {
        public int MaxValue { get; private set; }
        private int[][] M { get; set; }
        private int[][] P { get; set; }
        private Item[] I { get; set; }
        private int W { get; set; }

        public Knapsack(List<Item> items, int maxWeight)
        {
            I = items.ToArray();
            W = maxWeight;

            var n = I.Length;
            M = new int[n + 1][];
            P = new int[n + 1][];
            for (var i = 0; i < M.Length; i++)
            {
                M[i] = new int[W + 1];
            }
            for (var i = 0; i < P.Length; i++)
            {
                P[i] = new int[W + 1];
            }
        }

        public void Run()
        {
            var n = I.Length;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= W; j++)
                {
                    if (I[i - 1].w <= j)
                    {
                        M[i][j] = Max(M[i - 1][j], I[i - 1].v + M[i - 1][j - I[i - 1].w]);
                        if (I[i - 1].v + M[i - 1][j - I[i - 1].w] > M[i - 1][j])
                        {
                            P[i][j] = 1;
                        }
                        else
                        {
                            P[i][j] = -1;
                        }
                    }
                    else
                    {
                        P[i][j] = -1;
                        M[i][j] = M[i - 1][j];
                    }
                }
            }
            MaxValue = M[n][W];
        }

        public List<Item> Print()
        {
            var list = new List<Item>();
            list.AddRange(I);
            var w = W;
            int i = list.Count;

            var picked = new List<Item>();

            int valueSum = 0;
            int weightSum = 0;
            while (i >= 0 && w >= 0)
            {
                if (P[i][w] == 1)
                {
                    valueSum += list[i - 1].v;
                    weightSum += list[i - 1].w;
                    picked.Add(list[i - 1]);

                    w -= list[i - 1].w;
                }

                i--;
            }
            return picked;
        }

        public void PrintPicksMatrix(Action<object> write)
        {
            write("\n\n");
            foreach (var i in P)
            {
                foreach (var j in i)
                {
                    var s = j.ToString();
                    var _ = s.Length > 1 ? " " : "  ";
                    write(string.Concat(s, _));
                }
                write("\n");
            }
        }

        private static int Max(int a, int b)
        {
            return a > b ? a : b;
        }
    }

    internal class Item
    {
        public int Id { get; set; }
        public int v { get; set; } // value
        public int w { get; set; } // weight

        public override string ToString()
        {
            return string.Format("Id: {0}  v: {1}  w: {2}",
                Id, v, w);
        }
    }
}