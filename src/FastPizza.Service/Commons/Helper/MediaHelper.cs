using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Commons.Helper
{
    public class MediaHelper
    {
        public static string CreateImageName(string fillName)
        {
            FileInfo name = new FileInfo(fillName);
            string extantion = name.Extension;
            string newName = "IMG_" + Guid.NewGuid() + extantion;
            return newName;
        }

        public static string[] GetImageExtantion()
        {
            return new string[]
            {
                 // JPG files
                ".jpg", ".jpeg",
                // Png files
                ".png",
                // Bmp files
                ".bmp",
                // Svg files
                ".svg"
            };
        }

    }
}
