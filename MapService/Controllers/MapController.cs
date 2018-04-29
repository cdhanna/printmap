using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using printmap.Services.MapDataServices;

namespace printmap.Controllers
{
    [Route("api/[controller]")]
    public class MapController : Controller
    {
        public MapDataService MapDataService {get; set;}

        public MapController(MapDataService mapDataService){
            MapDataService = mapDataService;
        }


        [HttpGet("sat/{lat1}/{lon1}/{lat2}/{lon2}")]
        public IActionResult GetSatellite(float lat1, float lon1, float lat2, float lon2)
        {
            var request = new MapBBoxRequest(){
                Lon1 = lon1,
                Lon2 = lon2,
                Lat1 = lat1,
                Lat2 = lat2,
                MapName = "mapbox.satellite",
                Zoom = 18
            };
            var imageTask = MapDataService.GetBitmapForRegion(request);
            imageTask.Wait();

            var image = imageTask.Result;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64= Convert.ToBase64String(byteImage); //Get Base64

            return File(byteImage, "image/png");
            // return $"data:image/png;base64,{SigBase64}";
            // return $"COORD1 {lat1}, {lon1}";
        }

        [HttpGet("elevation/{lat1}/{lon1}/{lat2}/{lon2}")]
        public IActionResult GetElevation(float lat1, float lon1, float lat2, float lon2)
        {
            var request = new MapBBoxRequest(){
                Lon1 = lon1,
                Lon2 = lon2,
                Lat1 = lat1,
                Lat2 = lat2,
                MapName = "mapbox.terrain-rgb",
                Zoom = 18
            };
            var imageTask = MapDataService.GetBitmapForRegion(request);
            imageTask.Wait();

            var image = imageTask.Result;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64= Convert.ToBase64String(byteImage); //Get Base64

            return File(byteImage, "image/png");
            // return $"data:image/png;base64,{SigBase64}";
            // return $"COORD1 {lat1}, {lon1}";
        }
    }
}
