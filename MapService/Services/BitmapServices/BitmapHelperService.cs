using System;
using System.Drawing;

namespace printmap.Services.BitmapServices
{
    public class BitmapHelperService 
    {
        
        public byte[] Bitmap2Bytes(Bitmap image){
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            return byteImage;
        }
        
    }

}