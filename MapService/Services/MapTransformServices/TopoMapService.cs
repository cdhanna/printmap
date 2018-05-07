
using System.Threading.Tasks;
using System.Drawing;
using System;
using printmap.Services.BitmapServices;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace printmap.Services.MapTransformServices
{
    public class TopoMapService 
    {

        public BitmapHelperService BitmapHelperService {get; set;}
        public TopoMapService(BitmapHelperService bitmapHelperService){
            BitmapHelperService = bitmapHelperService;
        }

        public Bitmap TransformElevationToTopoMap(Bitmap elevationMap, int gapSizeMeters = 50){

            var output = new Bitmap(elevationMap);
            var locked = BitmapHelperService.LockBitmap(output);
            locked.LockBits();

            var maxHeight = float.MinValue;
            var minHeight = float.MaxValue;
            var heights = new List<float>();
            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    var pixel = locked.GetPixel(x, y);
                    var height = -10000 + ((pixel.R * 256 * 256 + pixel.G * 256 + pixel.B) * .1f);
                    maxHeight = Math.Max(maxHeight, height);
                    minHeight = Math.Min(minHeight, height);
                    heights.Add(height);
                }
            }

            var heightDifference = (maxHeight - minHeight);
            var gapCount = Math.Ceiling(heightDifference / gapSizeMeters);
            var gapAssignments = new List<int>();

            for (var i = 0 ; i < heights.Count ; i ++){
                var ratio = (heights[i] - minHeight) / heightDifference;
                var gap = (int)Math.Floor(ratio * gapCount);
                gapAssignments.Add(gap);
            }

            var index = 0;
            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    var gap = gapAssignments[index++];
                    var flat = 255 * (gap / gapCount);
                    locked.SetPixel(x, y, (byte)flat);
                }
            }

            locked.UnlockBits();

            return output;
        }

        public Bitmap TransformElevationToTopoLinesMap(Bitmap elevationMap, int gapSizeMeters = 50){

            var output = new Bitmap(elevationMap);
            var locked = BitmapHelperService.LockBitmap(output);
            locked.LockBits();

            var maxHeight = float.MinValue;
            var minHeight = float.MaxValue;
            var heights = new List<float>();
            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    var pixel = locked.GetPixel(x, y);
                    var height = -10000 + ((pixel.R * 256 * 256 + pixel.G * 256 + pixel.B) * .1f);
                    maxHeight = Math.Max(maxHeight, height);
                    minHeight = Math.Min(minHeight, height);
                    heights.Add(height);
                }
            }

            var heightDifference = (maxHeight - minHeight);
            var gapCount = Math.Ceiling(heightDifference / gapSizeMeters);
            var gapAssignments = new List<int>();

            for (var i = 0 ; i < heights.Count ; i ++){
                var ratio = (heights[i] - minHeight) / heightDifference;
                var gap = (int)Math.Floor(ratio * gapCount);
                gapAssignments.Add(gap);
            }

            var index = 0;
            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    var gap = gapAssignments[index++];
                    var flat = 255 * (gap / gapCount);
                    locked.SetPixel(x, y, (byte)flat);
                }
            }

            index = 0;
            var nextColors = new List<byte>();
            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    
                    var color = locked.GetPixel(x, y).R;    

                    var topColor = locked.GetPixel(x, y - 1 > -1 ? y - 1 : 0).R;
                    var lowColor = locked.GetPixel(x, y + 1 < locked.Height ? y + 1 : locked.Height-1).R;
                    var leftColor = locked.GetPixel(x - 1 > -1 ? x - 1 : 0, y).R;
                    var rightColor = locked.GetPixel(x + 1 < locked.Width ? x + 1 : locked.Width-1, y).R;

                    var outputColor = (byte)(
                        (color != topColor || color != lowColor || color != leftColor || color != rightColor)
                         ? color : 0);
                    nextColors.Add(outputColor);
                    //locked.SetPixel(x, y, color);

                }
            }

            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    locked.SetPixel(x, y, nextColors[index++]);
                }
            }

            locked.UnlockBits();

            return output;
        }

        public string TransformTopoToSVG(Bitmap topoMap){
            var sb = new StringBuilder();
            sb.AppendLine($"<svg width='{topoMap.Width}' height='{topoMap.Height}' xmlns='http://www.w3.org/2000/svg'>");
           

            // sb.AppendLine("<circle cx='20' cy='20' r='10'> </circle>"); // test. Draw a circle.
            // sb.AppendLine("<path d='M50 50L70 50L70 70' fill='transparent' stroke='red'/>"); //test. Draw a path


            var bitmap = new Bitmap(topoMap);
            var locked = BitmapHelperService.LockBitmap(bitmap);
            locked.LockBits();
            
            var trackedMask = new bool[locked.Width * locked.Height];

            var pathData = "";
            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){

                    var pixel = locked.GetPixel(x, y);
                    if (pixel.R == 255){
                        var paths = Trace(locked, trackedMask, x, y, pixel);
                        // if (path.Count > 0){
                        //     Console.WriteLine("Starting Trace");

                        //     pathData += $"M{path[0].X} {path[0].Y}";
                        //     for (var i = 1 ; i < path.Count; i ++){
                        //         pathData += $" L{path[i].X} {path[i].Y}";
                        //     }
                        // }
                    }                        

                }
            }
            sb.AppendLine($"<path d='{pathData}' fill=\"none\" stroke='red'/>");


            sb.AppendLine("</svg>");
            var svgText = sb.ToString();          
            return svgText;
        }

        private List<List<Point>> Trace(LockBitmap bitmap, bool[] mask, int x, int y, Color currentPixel){

            var maskIndex = y * bitmap.Width + x;
            if (mask[maskIndex]){
                return new List<List<Point>>(); // this pixel has already been processed.
            }

            mask[maskIndex] = true; // mark this pixel as processed.
            
            var paths = new List<List<Point>>();

            for (var nx = x - 1; nx <= x + 1; nx ++){
                for (var ny = y - 1; ny <= y + 1;ny ++){
                    if (nx == x && ny == y){
                        continue; // ignore the neighbor coordinate that *is* the current pixel
                    }

                    var neighborPixel = bitmap.GetPixel(nx, ny);

                    if (!mask[ny*bitmap.Width + nx] && neighborPixel.Equals(currentPixel)){
                        // call trace on this neighbor!
                        var subPaths = Trace(bitmap, mask, nx, ny, currentPixel);
                        subPaths.ForEach(path => {
                            path.Insert(0, new Point(x, y));
                        });
                        paths.AddRange(subPaths);
                        //path.AddRange(subPath);
                        //return path;
                        // var subPaths = Trace(bitmap, mask, nx, ny, currentPixel);
                        // foreach(var subPath in subPaths){
                        //     subPath.Insert(0, new Point(x, y));
                        // }
                        // paths.AddRange(subPaths);
                    }

                }
            }
            if (paths.Count == 0){
                var path = new Point[]{new Point(x, y)}.ToList();
                paths.Add(path);
            }
            return paths;
        }

        public string TransformElevationToSVG(Bitmap elevationMap, int gapSizeMeters = 50){

            var sb = new StringBuilder();
            sb.AppendLine($"<svg width='{elevationMap.Width}' height='{elevationMap.Height}' xmlns='http://www.w3.org/2000/svg'>");
           

            var output = new Bitmap(elevationMap);
            var locked = BitmapHelperService.LockBitmap(output);
            locked.LockBits();

            var maxHeight = float.MinValue;
            var minHeight = float.MaxValue;
            var heights = new List<float>();
            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    var pixel = locked.GetPixel(x, y);
                    var height = -10000 + ((pixel.R * 256 * 256 + pixel.G * 256 + pixel.B) * .1f);
                    maxHeight = Math.Max(maxHeight, height);
                    minHeight = Math.Min(minHeight, height);
                    heights.Add(height);
                }
            }

            var heightDifference = (maxHeight - minHeight);
            var gapCount = Math.Ceiling(heightDifference / gapSizeMeters);
            var gapAssignments = new List<int>();

            for (var i = 0 ; i < heights.Count ; i ++){
                var ratio = (heights[i] - minHeight) / heightDifference;
                var gap = (int)Math.Floor(ratio * gapCount);
                gapAssignments.Add(gap);
            }

            var index = 0;
            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    var gap = gapAssignments[index++];
                    var flat = 255 * (gap / gapCount);
                    locked.SetPixel(x, y, (byte)flat);
                }
            }

            index = 0;
            var nextColors = new List<byte>();
            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    
                    var color = locked.GetPixel(x, y).R;    

                    var topColor = locked.GetPixel(x, y - 1 > -1 ? y - 1 : 0).R;
                    var lowColor = locked.GetPixel(x, y + 1 < locked.Height ? y + 1 : locked.Height-1).R;
                    var leftColor = locked.GetPixel(x - 1 > -1 ? x - 1 : 0, y).R;
                    var rightColor = locked.GetPixel(x + 1 < locked.Width ? x + 1 : locked.Width-1, y).R;

                    var outputColor = (byte)(
                        (color != topColor || color != lowColor || color != leftColor || color != rightColor)
                         ? color : 0);
                    nextColors.Add(outputColor);
                    //locked.SetPixel(x, y, color);

                }
            }

            for (var x = 0 ; x < locked.Width; x ++){
                for (var y = 0 ; y < locked.Height; y ++){
                    locked.SetPixel(x, y, nextColors[index++]);
                }
            }

            locked.UnlockBits();

            sb.AppendLine("</svg>");
            var svg = sb.ToString();
            return svg;
        }
    }
}