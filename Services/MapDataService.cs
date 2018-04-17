using System.Net.Http;
using System.Threading.Tasks;
using System.Drawing;
using System;

namespace printmap.Services 
{


    public class MapDataService 
    {

        private string _token = "pk.eyJ1IjoiY2RoYW5uYSIsImEiOiJjaXVoY3AxdGUwMHVmM3BxZnZnMGFtZDA0In0.G2Gjh4DkpoyC1h9nZFaL5g"; // todo make a config option

        public async Task<Bitmap> GetBitmapForRegion(float lat1, float lon1, float lat2, float lon2){
            //"https://api.mapbox.com/v4/mapbox.streets/1/0/0.png?access_token=your-access-token"

            var zoom = 16;
            var coord1 = GetTile(lat1, lon1, zoom);
            var coord2 = GetTile(lat2, lon2, zoom);
            var minX = Math.Min(coord1.X, coord2.X);
            var maxX = Math.Max(coord1.X, coord2.X);
            var minY = Math.Min(coord1.Y, coord2.Y);
            var maxY = Math.Max(coord1.Y, coord2.Y);
            var width = (maxX - minX) + 1;
            var height = (maxY - minY) + 1;
            var ouptut = new Bitmap(512, 512);
            var combined = new Bitmap(256 * width, 256 * height);

            var combinedLon1 = GetLonFromTileX(zoom, minX);
            var combinedLon2 = GetLonFromTileX(zoom, maxX);
            var combinedLat1 = GetLatFromTileY(zoom, minY);
            var combinedLat2 = GetLatFromTileY(zoom, maxY);



            using (HttpClient client = new HttpClient())
            {
                for (var x = minX ; x <= maxX; x ++)
                {
                    for (var y = minY ; y <= maxY; y ++)
                    {
                        var url = BuildUrl(new TileCoord(x, y, zoom));
                        //http://localhost:5000/api/map/42.290658/-71.171843/42.289903/-71.169225
                        using (HttpResponseMessage res = await client.GetAsync(url))
                        using (HttpContent content = res.Content)
                        {
                            res.EnsureSuccessStatusCode();

                            var stream = await content.ReadAsStreamAsync();

                            var image = System.Drawing.Image.FromStream(stream);
                            var bitmap = new Bitmap(image);

                            CopyRegionToIntoBitmap(bitmap,
                                new Rectangle(0, 0, 256, 256),
                                ref combined, 
                                new Rectangle((x-minX) * 256, (y-minY) * 256, 256, 256));
                        }
                    }
                }
            }

            // TODO copy the right part of the combined image into the output
            //var combinedRegion = new Rectangle()



            return combined;

        }

        private TileCoord GetTile(float lat, float lon, int zoom=16)
        {
            /*
            from https://wiki.openstreetmap.org/wiki/Slippy_map_tilenames
            n = 2 ^ zoom
            xtile = n * ((lon_deg + 180) / 360)
            ytile = n * (1 - (log(tan(lat_rad) + sec(lat_rad)) / π)) / 2
             */
            var n = Math.Pow(2, zoom);
            var x = n * ((lon + 180) / 360);
            var lat_rad = lat * Math.PI / 180;
            var y = n * (1 - (Math.Log( Math.Tan(lat_rad) + (1/Math.Cos(lat_rad))) / Math.PI)) / 2;
            return new TileCoord((int)x, (int)y, zoom);
        }

        private float GetLonFromTileX(int zoom, float tileX)
        {
            var n = (float) Math.Pow(2, zoom);
            return tileX / n * 360 - 180;
        }
        private float GetLatFromTileY(int zoom, float tileY){
            var n = (float) Math.Pow(2, zoom);
            var latRad = (float)Math.Atan(Math.Sinh(Math.PI * (1 - 2 * tileY / n)));
            return latRad * 180 / (float)Math.PI;            
        }

        private void CopyRegionToIntoBitmap(Bitmap srcBitmap, Rectangle srcRegion, ref Bitmap dstBitmap, Rectangle dstRegion)
        {
            //https://stackoverflow.com/questions/9616617/c-sharp-copy-paste-an-image-region-into-another-image
            using (Graphics grD = Graphics.FromImage(dstBitmap))            
            {
                grD.DrawImage(srcBitmap, dstRegion, srcRegion, GraphicsUnit.Pixel);                
            }
        }

        private string BuildUrl(TileCoord coord)
        {
            return $"https://api.mapbox.com/v4/mapbox.satellite/{coord.Zoom}/{coord.X}/{coord.Y}.png?access_token={_token}"; // todo pull options out into config options
        }

    }

}