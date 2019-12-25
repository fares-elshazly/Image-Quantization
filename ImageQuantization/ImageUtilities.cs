using System.Collections.Generic;

namespace ImageQuantization
{
    public class ImageUtilities
    {
        public static List<RGBPixel> DistinctColours;
       
        public static int GetDistinctColors(RGBPixel[,] Buffer)
        {
            bool[,,] Visited_Buffer = new bool[256, 256, 256];

            DistinctColours = new List<RGBPixel>();

            for (int i = 0; i < Buffer.GetLength(0); i++)
            {
                for (int j = 0; j < Buffer.GetLength(1); j++)
                {

                    if (Visited_Buffer[Buffer[i, j].red, Buffer[i, j].green, Buffer[i, j].blue] == false)
                    {
                        Visited_Buffer[Buffer[i, j].red, Buffer[i, j].green, Buffer[i, j].blue] = true;
                        DistinctColours.Add(Buffer[i, j]);
                    }
                }
            }
            return DistinctColours.Count;
        }
    }
}
