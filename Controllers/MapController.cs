using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using printmap.Services;

namespace printmap.Controllers
{
    [Route("api/[controller]")]
    public class MapController : Controller
    {
        [HttpGet("{lat1}/{lon1}/{lat2}/{lon2}")]
        public string Get(float lat1, float lon1, float lat2, float lon2)
        {
            var map = new MapDataService(); // todo, add unity container or some such ingest
            var imageTask = map.GetBitmapForRegion();
            imageTask.Wait();

            var image = imageTask.Result;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64= Convert.ToBase64String(byteImage); //Get Base64

            return $"data:image/png;base64,{SigBase64}";
            // return $"COORD1 {lat1}, {lon1}";
        }
    }
}
