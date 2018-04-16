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

            var url = BuildUrl();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(url))
                using (HttpContent content = res.Content)
                {
                    res.EnsureSuccessStatusCode();

                    var stream = await content.ReadAsStreamAsync();

                    var image = System.Drawing.Image.FromStream(stream);
                    return new Bitmap(image);
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


        private string BuildUrl(){
            return $"https://api.mapbox.com/v4/mapbox.streets/{1}/{0}/{0}.png?access_token={_token}"; // todo pull options out into config options
        }

    }

}