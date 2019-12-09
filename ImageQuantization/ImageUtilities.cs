using System.Collections.Generic;

namespace ImageQuantization
{
    public class ImageUtilities
    {
        public static List<RGBPixel> FindDistinctColors(string imagePath)
        {
            RGBPixel[,] Colors = ImageOperations.OpenImage(imagePath);
            HashSet<RGBPixel> Distincit_Colors = new HashSet<RGBPixel>();

            for (int i = 0; i < Colors.GetLength(0); i++)
            {
                for (int j = 0; j < Colors.GetLength(1); j++)
                {
                    Distincit_Colors.Add(Colors[i, j]);
                }
            }

            return new List<RGBPixel>(Distincit_Colors);
        }
    }
}
