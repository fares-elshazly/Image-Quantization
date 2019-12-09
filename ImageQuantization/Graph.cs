using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageQuantization;

namespace ImageQuantization
{
    class Graph
    {
        public class Edge
        {
            public int Source, Destination;
            public Double Weight;
        }

        public class SubSet
        {
            public int Parent, Rank;
        };

        public int Vertices, Total_Edges;
        public Edge[] Edges;

        public Graph(int V, int E)
        {
            Vertices = V;
            Total_Edges = E;
            Edges = new Edge[Total_Edges];
            for (int i = 0; i < E; ++i)
                Edges[i] = new Edge();

        }
        public int Find(SubSet[] SubSets, int i)
        {

            if (SubSets[i].Parent != i)
                SubSets[i].Parent = Find(SubSets,
                                         SubSets[i].Parent);

            return SubSets[i].Parent;
        }
        public void Union(SubSet[] SubSets, int x, int y)
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


        public Double Kruskal_MST()
        {
            Edge[] New_Edges = new Edge[Vertices];
            int Edges_index = 0;
            int i = 0;
            for (i = 0; i < Vertices; ++i)
                New_Edges[i] = new Edge();

            Array.Sort(Edges, delegate (Edge Edges1, Edge Edges2)
            {
                return Edges1.Weight.CompareTo(Edges2.Weight);
            });


            SubSet[] SubSets = new SubSet[Vertices];
            for (i = 0; i < Vertices; ++i)
                SubSets[i] = new SubSet();


            for (int v = 0; v < Vertices; ++v)
            {
                SubSets[v].Parent = v;
                SubSets[v].Rank = 0;
            }

            i = 0;

            while (Edges_index < Vertices - 1)
            {
                Edge next_Edges = new Edge();
                next_Edges = Edges[i++];

                int x = Find(SubSets, next_Edges.Source);
                int y = Find(SubSets, next_Edges.Destination);

                if (x != y)
                {
                    New_Edges[Edges_index++] = next_Edges;
                    Union(SubSets, x, y);
                }

            }

            Double Result = 0;
            for (i = 0; i < Edges_index; ++i)
            {

                Result += New_Edges[i].Weight;

            }
            return Result;

        }
    }
}