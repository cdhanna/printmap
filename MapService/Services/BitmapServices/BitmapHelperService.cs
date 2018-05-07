using System;
using System.Drawing;
using System.IO;

namespace printmap.Services.BitmapServices
{
    public class BitmapHelperService 
    {
        
        public byte[] Bitmap2Bytes(Bitmap image, bool png=false){
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, png ? System.Drawing.Imaging.ImageFormat.Png : System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            return byteImage;
        }
        
        public Bitmap Bytes2Bitmap(byte[] data){
            var dataStream = new MemoryStream(data);
            var image = System.Drawing.Image.FromStream(dataStream);
            var bitmap = new Bitmap(image);
            return bitmap;
        }

        public LockBitmap LockBitmap(Bitmap bitmap){
            return new LockBitmap(bitmap);
        }


    }

}