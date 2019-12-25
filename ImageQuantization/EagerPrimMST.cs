using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    public struct Edge : IComparable<Edge>
    {
        public int From, To;
        public double Weight;
        public Edge(double Weight, int src, int des)
        {
            From = src;
            To = des;
            this.Weight = Weight;
        }
        public int CompareTo(Edge other)
        {
            if (this.Weight < other.Weight)
                return -1;
            else if (this.Weight > other.Weight)
                return 1;
            else return 0;
        }
    }
    public class EagerPrimMST
    {
        public Edge[] Edge_To { set; get; }
        public double[] Destination;
        public bool[] Visited;
        public IPriorityQueue IndexedPQ;
        public int NumOfVertices;
        public int minimumindex = 0;
        public int[] Nodes;

        public EagerPrimMST(int GraphNodesCount)
        {

            NumOfVertices = GraphNodesCount;
            Nodes = new int[ImageUtilities.DistinctColours.Count];
            Edge_To = new Edge[NumOfVertices];
            Destination = new double[NumOfVertices];
            Visited = new bool[NumOfVertices];
            IndexedPQ = new IPriorityQueue(NumOfVertices);
            for (int i = 0; i < NumOfVertices; ++i)
            {
                Destination[i] = double.MaxValue;
            }

        }
        public double GetMst()
        {
            double Sum = 0;
            IndexedPQ.Insert(0, 0);
            while (!IndexedPQ.IsEmpty())
            {
                int j = IndexedPQ.DeleteMin();
                minimumindex = j;
                Visited[j] = true;

                for (int v = 0; v < NumOfVertices; ++v)
                {
                    if (Visited[v]) continue;
                    double edgeWeight = MathUtilities.GetDistance(j, v);
                    if (edgeWeight < Destination[v])
                    {
                        Destination[v] = edgeWeight;
                        Nodes[v] = minimumindex;
                        Edge_To[v] = new Edge(edgeWeight, j, v);
                        if (IndexedPQ.Contains(v))
                        {
                            IndexedPQ.DecreaseKey(v, Destination[v]);
                        }
                        else IndexedPQ.Insert(v, Destination[v]);
                    }
                }
            }

            for (int i = 0; i < Edge_To.Count(); ++i)
            {
                Sum += Edge_To[i].Weight;
            }
            return Sum;
        }






    }
}
