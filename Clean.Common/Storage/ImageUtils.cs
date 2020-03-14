using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Clean.Common.Storage
{
    public static class ImageUtils
    {
        public static Image Resize(Image image, int scaledWidth, int scaledHeight)
        {
            return new Bitmap(image, scaledWidth, scaledHeight);
        }
        public static Image Crop(Image image, int x, int y, int width, int height)
        {
            var croppedBitmap = new Bitmap(width, height);
            using (var g = Graphics.FromImage(croppedBitmap))
            {
                g.DrawImage(image,
                    new Rectangle(0, 0, width, height),
                    new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            }
            return croppedBitmap;
        }
        public static void RemoveFile(string inputFile)
        {
            File.Delete(inputFile);
        }
    }
}
