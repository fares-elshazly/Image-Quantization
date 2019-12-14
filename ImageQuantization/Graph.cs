using System.Collections.Generic;

namespace ImageQuantization
{
    public class Graph
    {
        public int NumOfVertices, NumOfEdges;
        public Edge[] Edges;
        public List<List<Edge>> AdjacencyList;
        public double[,] AdjacencyMatrix;

        public class Edge
        {
            public int Source, Destination;
            public double Weight;

            public Edge() { }

            public Edge(int Source, int Destination, double Weight)
            {
                this.Source = Source;
                this.Destination = Destination;
                this.Weight = Weight;
            }
        }

        public Graph(int NumOfVertices, int NumOfEdges)
        {
            this.NumOfVertices = NumOfVertices;
            this.NumOfEdges = NumOfEdges;
            Edges = new Edge[NumOfEdges];
            AdjacencyList = new List<List<Edge>>();
            AdjacencyMatrix = new double[NumOfVertices, NumOfVertices];
            for (int i = 0; i < NumOfEdges; ++i)
                AdjacencyList.Add(new List<Edge>());
        }

        public void BuildEdges(List<RGBPixel> DistinctColors)
        {
            int EdgeIndex = 0;

            for (int i = 0; i < NumOfVertices; i++)
            {
                for (int j = 0; j < NumOfVertices; j++)
                {
                    if (i != j)
                    {
                        Edges[EdgeIndex].Source = i;
                        Edges[EdgeIndex].Destination = j;
                        Edges[EdgeIndex].Weight = MathUtilities.GetDistance(DistinctColors[i], DistinctColors[j]);
                        EdgeIndex++;
                    }
                }
            }
        }

        public void BuildAdjacencyList(List<RGBPixel> DistinctColors)
        {
            for (int i = 0; i < NumOfVertices; i++)
            {
                for (int j = 0; j < NumOfVertices; j++)
                {
                    if (i != j)
                    {
                        AdjacencyList[i].Add(new Edge(i, j, MathUtilities.GetDistance(DistinctColors[i], DistinctColors[j])));
                    }
                }
            }
        }

        public void BuildAdjacencyMatrix(List<RGBPixel> DistinctColors)
        {
            for (int i = 0; i < NumOfVertices; i++)
            {
                for (int j = 0; j < NumOfVertices; j++)
                {
                    if (i != j)
                    {
                       AdjacencyMatrix[i, j] = MathUtilities.GetDistance(DistinctColors[i], DistinctColors[j]);
                    }
                }
            }
        }
    }
}