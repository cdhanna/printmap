
using System.Threading.Tasks;
using System.Drawing;
using System;
using printmap.Services.BitmapServices;
using System.Collections.Generic;

namespace printmap.Services.MapTransformServices
{


    public class ElevationMapService 
    {
        public BitmapHelperService BitmapHelperService {get; set;}
        public ElevationMapService(BitmapHelperService bitmapHelperService){
            BitmapHelperService = bitmapHelperService;
        }

        public Bitmap TransformElevationToHeightMap(Bitmap elevationMap){

            var output = new Bitmap(elevationMap);
            var locked = BitmapHelperService.LockBitmap(output);
            locked.LockBits();

            var maxHeight = float.MinValue;
            var minHeight = float.MaxValue;
            var heights = new List<float>();
            var i = 0;
            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    var pixel = locked.GetPixel(x, y);
                    var height = -10000 + ((pixel.R * 256 * 256 + pixel.G * 256 + pixel.B) * .1f);
                    maxHeight = Math.Max(maxHeight, height);
                    minHeight = Math.Min(minHeight, height);
                    heights.Add(height);
                }
            }


            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    var pixel = locked.GetPixel(x, y);
                    var height = heights[i++];
                    height = (height - minHeight) / (maxHeight - minHeight);
                    locked.SetPixel(x, y, (byte)(255 * height));
                    //http://localhost:5000/api/map/height/44.206819/-71.095963/44.302230/-71.008759
                }
            }

            locked.UnlockBits();
            return output;

        }

    }
}