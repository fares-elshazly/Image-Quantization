using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    public class KClustring
    {
        int K;
        bool[] Visited;
        int[] Nodes = new int[ImageUtilities.DistinctColours.Count];
        Edge[] Edges;
        public List<int>[] adj;
        public List<RGBPixel> Palette;
        public List<List<int>> NewColors = new List<List<int>>();
        RGBPixel[,,] Updated_Colours;
        public RGBPixel[,] Quantized_Image;

        public KClustring(int k, int[] nodes, Edge[] edges)
        {
            this.K = k;
            this.Nodes = nodes;
            this.Edges = edges;
            adj = new List<int>[ImageUtilities.DistinctColours.Count];
            for (int i = 0; i < ImageUtilities.DistinctColours.Count; i++)
            {
                adj[i] = new List<int>(ImageUtilities.DistinctColours.Count);
            }

        }

        public void Clustering()
        {
            Palette = new List<RGBPixel>();
            Visited = new bool[ImageUtilities.DistinctColours.Count];

            for (int i = 0; i < K - 1; i++)
            {
                int temp = 0;
                double max = 0;
                for (int j = 0; j < ImageUtilities.DistinctColours.Count; j++)
                {
                    if (Edges[j].Weight > max)
                    {
                        max = Edges[j].Weight;
                        temp = j;
                    }
                }
                Edges[temp].Weight = -1;
                Nodes[temp] = temp;
            }

            for (int j = 0; j < ImageUtilities.DistinctColours.Count; j++)
            {
                if (Nodes[j] != j)
                {
                    adj[j].Add(Nodes[j]);
                    adj[Nodes[j]].Add(j);
                }
            }

            for (int i = 0; i < ImageUtilities.DistinctColours.Count; i++)
            {
                List<int> Colors_Sum = new List<int>(ImageUtilities.DistinctColours.Count);

                if (Visited[i] == false)
                {

                    DFS(adj, i, Colors_Sum);
                    NewColors.Add(Colors_Sum);

                }

            }
            for (int c = 0; c < NewColors.Count; c++)
            {
                Palette = new List<RGBPixel>();
                RGBPixel color = new RGBPixel();
                for (int i = 0; i < NewColors.Count; i++)
                {
                    int red = 0;
                    int blue = 0;
                    int green = 0;

                    int groupSize = NewColors[i].Count;
                    for (int j = 0; j < groupSize; j++)
                    {
                        red += ImageUtilities.DistinctColours[NewColors[i][j]].red;
                        blue += ImageUtilities.DistinctColours[NewColors[i][j]].blue;
                        green += ImageUtilities.DistinctColours[NewColors[i][j]].green;

                    }
                    red = red / NewColors[i].Count;
                    blue = blue / NewColors[i].Count;
                    green = green / NewColors[i].Count;
                    color.red = (byte)red;
                    color.blue = (byte)blue;
                    color.green = (byte)green;
                    Palette.Add(color);
                }
            }

        }

        public void DFS(List<int>[] input, int node, List<int> Colors_Sum)
        {

            Colors_Sum.Add(node);
            Visited[node] = true;

            for (int i = 0; i < input[node].Count; i++)
            {
                if (!Visited[input[node][i]])
                {
                    DFS(input, input[node][i], Colors_Sum);
                }
            }
        }

        public void QuantizeImage(List<RGBPixel> palette, RGBPixel[,] ImageMatrix)
        {
            Quantized_Image = new RGBPixel[ImageMatrix.GetLength(0), ImageMatrix.GetLength(1)];
            Updated_Colours = new RGBPixel[256, 256, 256];
            for (int i = 0; i < NewColors.Count; i++)
            {
                int groupSize = NewColors[i].Count;
                for (int j = 0; j < groupSize; j++)
                {
                    Updated_Colours[ImageUtilities.DistinctColours[NewColors[i][j]].red, ImageUtilities.DistinctColours[NewColors[i][j]].blue, ImageUtilities.DistinctColours[NewColors[i][j]].green] = Palette[i];
                }
            }
            for (int i = 0; i < ImageMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ImageMatrix.GetLength(1); j++)
                {
                    Quantized_Image[i, j] = Updated_Colours[ImageMatrix[i, j].red, ImageMatrix[i, j].blue, ImageMatrix[i, j].green];
                }
            }

           
        }


    }
}
