using System;
using System.Collections.Generic;
using static ImageQuantization.Graph;

namespace ImageQuantization
{
    public class MST
    {
        public class SubSet
        {
            public int Parent, Rank;
        }

        public static int Find(SubSet[] SubSets, int i)
        {
            if (SubSets[i].Parent != i)
                SubSets[i].Parent = Find(SubSets, SubSets[i].Parent);

            return SubSets[i].Parent;
        }

        public static void Union(SubSet[] SubSets, int x, int y)
        {
            int Root_X = Find(SubSets, x);
            int Root_Y = Find(SubSets, y);

            if (SubSets[Root_X].Rank < SubSets[Root_Y].Rank)
                SubSets[Root_X].Parent = Root_Y;

            else if (SubSets[Root_X].Rank > SubSets[Root_Y].Rank)
                SubSets[Root_Y].Parent = Root_X;

            else
            {
                SubSets[Root_Y].Parent = Root_X;
                SubSets[Root_X].Rank++;
            }
        }

        public static double KruskalMST(Edge[] Edges, int NumOfVertices)
        {
            int index = 0;
            double Result = 0;
            int NumOfEdges = 0;
            Edge[] NewEdges = new Edge[NumOfVertices];

            for (int i = 0; i < NumOfVertices; ++i)
                NewEdges[i] = new Edge();

            Array.Sort(Edges, delegate (Edge Edges1, Edge Edges2)
            {
                return Edges1.Weight.CompareTo(Edges2.Weight);
            });

            SubSet[] SubSets = new SubSet[NumOfVertices];
            for (int i = 0; i < NumOfVertices; ++i)
                SubSets[i] = new SubSet();

            for (int v = 0; v < NumOfVertices; ++v)
            {
                SubSets[v].Parent = v;
                SubSets[v].Rank = 0;
            }

            while (NumOfEdges < NumOfVertices - 1)
            {
                Edge NextEdge = new Edge();
                NextEdge = Edges[index++];

                int x = Find(SubSets, NextEdge.Source);
                int y = Find(SubSets, NextEdge.Destination);

                if (x != y)
                {
                    NewEdges[NumOfEdges++] = NextEdge;
                    Union(SubSets, x, y);
                }
            }

            for (int i = 0; i < NumOfEdges; ++i)
                Result += NewEdges[i].Weight;

            return Result;
        }

        public static double PrimMST(List<List<Edge>> AdjacencyList, int NumOfVertices)
        {
            double MSTSum = 0;
            bool[] Visited = new bool[NumOfVertices];
            List<Edge> Edges = new List<Edge>();
            PriorityQueue PQ = new PriorityQueue();

            PQ.Enqueue(new Edge(-1, 0, 0));

            while(PQ.Count > 0)
            {
                Edge e = PQ.Dequeue();
                if (Visited[e.Destination])
                    continue;
                Visited[e.Destination] = true;
                MSTSum += e.Weight;
                if (e.Destination != 0)
                    Edges.Add(e);
                for(int i = 0; i < AdjacencyList[e.Destination].Count; i++)
                {
                    Edge ne = AdjacencyList[e.Destination][i];
                    if (!Visited[ne.Destination])
                        PQ.Enqueue(ne);
                }
            }

            return MSTSum;
        }
    }
}
