using System.Net.Http;
using System.Threading.Tasks;
using System.Drawing;

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

        private string BuildUrl(){
            return $"https://api.mapbox.com/v4/mapbox.streets/{1}/{0}/{0}.png?access_token={_token}"; // todo pull options out into config options
        }

    }

}