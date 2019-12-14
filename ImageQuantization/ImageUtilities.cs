using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImageQuantization
{
    public class ImageUtilities
    {
        public static unsafe int FindDistinctColours(string imagePath)
        {
            Bitmap bmp = new Bitmap(imagePath);
            int width = bmp.Width;
            int height = bmp.Height;

            var rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var colors = new HashSet<int>();

            var bmpPtr = (int*)bmpData.Scan0;

            for (int i = 0; i < width * height; i++)
            {
                colors.Add(bmpPtr[0]);
                bmpPtr++;
            }
            bmp.UnlockBits(bmpData);
            return colors.Count;
        }
    }
}
