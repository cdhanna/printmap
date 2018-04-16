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

            using (HttpClient client = new HttpClient())
            {
                for (var x = minX ; x <= maxX; x ++)
                {
                    for (var y = minY ; y < maxY; y ++)
                    {
                        var url = BuildUrl(new TileCoord(x, y, zoom));
                        
                        using (HttpResponseMessage res = await client.GetAsync(url))
                        using (HttpContent content = res.Content)
                        {
                            res.EnsureSuccessStatusCode();

                            var stream = await content.ReadAsStreamAsync();

                            var image = System.Drawing.Image.FromStream(stream);
                            return new Bitmap(image); // TODO https://stackoverflow.com/questions/9616617/c-sharp-copy-paste-an-image-region-into-another-image
                        }
                    }
                }
            }

        }

        private TileCoord GetTile(float lat, float lon, int zoom=16){
            /*
            from https://wiki.openstreetmap.org/wiki/Slippy_map_tilenames
            n = 2 ^ zoom
            xtile = n * ((lon_deg + 180) / 360)
            ytile = n * (1 - (log(tan(lat_rad) + sec(lat_rad)) / π)) / 2
             */
            var n = Math.Pow(2, zoom);
            var x = n * ((lon + 180) / 360);
            var lat_rad = lat * Math.PI / 180;
            var y = (1 - (Math.Log( Math.Tan(lat_rad) + Math.Asin(lat_rad)) / Math.PI)) / 2;
            return new TileCoord((int)x, (int)y, zoom);
        }


        private string BuildUrl(TileCoord coord){
            return $"https://api.mapbox.com/v4/mapbox.streets/{coord.Zoom}/{coord.X}/{coord.Y}.png?access_token={_token}"; // todo pull options out into config options
        }

    }

}