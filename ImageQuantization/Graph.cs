using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    public class Graph
    {
        public int NumOfVertices, NumOfEdges;
        public Edge[] Edges;

        public class Edge
        {
            public int Source, Destination;
            public double Weight;
        }

        public Graph(int NumOfVertices, int NumOfEdges)
        {
            this.NumOfVertices = NumOfVertices;
            this.NumOfEdges = NumOfEdges;
            Edges = new Edge[NumOfEdges];
            for (int i = 0; i < NumOfEdges; ++i)
                Edges[i] = new Edge();
        }

        public void BuildEdges(List<RGBPixel> DistinctColors)
        {
            int EdgeIndex = 0;

            for(int i = 0; i < NumOfVertices; i++)
            {
                for (int j = 0; j < NumOfVertices; j++)
                {
                    if(i != j)
                    {
                        Edges[EdgeIndex].Source = i;
                        Edges[EdgeIndex].Destination = j;
                        Edges[EdgeIndex].Weight = MathUtilities.GetDistance(DistinctColors[i], DistinctColors[j]);
                        EdgeIndex++;
                    }
                }
            }
        }
    }
}