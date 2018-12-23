using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using printmap.Services.BitmapServices;
using printmap.Services.MapTransformServices;

namespace printmap
{
    public class VectorizeProgram
    {

        private BitmapHelperService bitmapHelper = new BitmapHelperService();

        public VectorizeProgram(string inputBitmapFilepath, string outputSvgFilePath){

            Console.WriteLine($"Loading bitmap {inputBitmapFilepath} ");

            var bitmapBytes = File.ReadAllBytes(inputBitmapFilepath);

            Console.WriteLine($"Input Byte Count {bitmapBytes.Length}");

            var bitmap = bitmapHelper.Bytes2Bitmap(bitmapBytes);

            Console.WriteLine($"Input bitmap size {bitmap.Width} x {bitmap.Height}");


            var srvc = new TopoMapService(bitmapHelper);

            var svgText = srvc.TransformTopoToSVG(bitmap);

            File.WriteAllText(outputSvgFilePath, svgText);

        }

    }
}
