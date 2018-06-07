using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace VillaNova
{
    class ImageHelper
    {

        public static Image GetGround(string path)
        {
            return GetImage($"ground\\{path}");
        }

        public static Image GetPlayer(string path)
        {
            return GetImage($"player\\{path}");
        }

        private static Image GetImage(string path)
        {
            Image img = null;

            string fileDir = ($"{System.AppDomain.CurrentDomain.BaseDirectory}res\\images\\{path}.png");
            
            if (File.Exists(fileDir))
            {
                try
                {
                    img = Image.FromFile(fileDir);
                    Debug.WriteLine($"Read file {fileDir}");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error reading file {fileDir}: {e.Message}");
                }
            }
            else
            {
                Debug.WriteLine($"No file {fileDir} exists");
            }

            return img;
        }
    }
}