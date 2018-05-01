using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using printmap.Services.BitmapServices;
using printmap.Services.MapDataServices;
using printmap.Services.MapTransformServices;

namespace printmap.Controllers
{
    [Route("api/[controller]")]
    public class MapController : Controller
    {
        public MapDataService MapDataService {get; set;}
        public ElevationMapService ElevationTransformService {get; set;}
        public BitmapHelperService BitmapHelperService {get; set;}
        public TopoMapService TopoMapService {get; set;}

        public MapController(MapDataService mapDataService, 
            ElevationMapService elevationMapService, 
            BitmapHelperService bitmapHelperService,
            TopoMapService topoMapService){

            MapDataService = mapDataService;
            ElevationTransformService = elevationMapService;
            BitmapHelperService = bitmapHelperService;
            TopoMapService = topoMapService;
        }


        [HttpGet("sat/{lat1}/{lon1}/{lat2}/{lon2}/{zoom?}")]
        public IActionResult GetSatellite(float lat1, float lon1, float lat2, float lon2, int zoom=9)
        {
            var request = new MapBBoxRequest(){
                Lon1 = lon1,
                Lon2 = lon2,
                Lat1 = lat1,
                Lat2 = lat2,
                MapName = "mapbox.satellite",
                Zoom = zoom
            };
            var imageTask = MapDataService.GetBitmapForRegion(request);
            imageTask.Wait();
            return File(BitmapHelperService.Bitmap2Bytes(imageTask.Result), "image/jpg");
        }

        [HttpGet("elevation/{lat1}/{lon1}/{lat2}/{lon2}/{zoom?}")]
        public IActionResult GetElevation(float lat1, float lon1, float lat2, float lon2, int zoom=9)
        {
            var request = new MapBBoxRequest(){
                Lon1 = lon1,
                Lon2 = lon2,
                Lat1 = lat1,
                Lat2 = lat2,
                MapName = "mapbox.terrain-rgb",
                Zoom = zoom
            };
            var imageTask = MapDataService.GetBitmapForRegion(request);
            imageTask.Wait();

            var image = imageTask.Result;

            return File(BitmapHelperService.Bitmap2Bytes(image), "image/jpg");

            // return $"data:image/png;base64,{SigBase64}";
            // return $"COORD1 {lat1}, {lon1}";
        }

        [HttpGet("height/{lat1}/{lon1}/{lat2}/{lon2}/{zoom?}")]
        public IActionResult GetHeightMap(float lat1, float lon1, float lat2, float lon2, int zoom=9)
        {
            var request = new MapBBoxRequest(){
                Lon1 = lon1,
                Lon2 = lon2,
                Lat1 = lat1,
                Lat2 = lat2,
                MapName = "mapbox.terrain-rgb",
                Zoom = zoom
            };
            var imageTask = MapDataService.GetBitmapForRegion(request);
            imageTask.Wait();

            var heightMap = ElevationTransformService.TransformElevationToHeightMap(imageTask.Result);

            return File(BitmapHelperService.Bitmap2Bytes(heightMap), "image/jpg");
        }

        [HttpGet("topo/{lat1}/{lon1}/{lat2}/{lon2}/{zoom?}")]
        public IActionResult GetTopoMap(float lat1, float lon1, float lat2, float lon2, int zoom=10)
        {
            var request = new MapBBoxRequest(){
                Lon1 = lon1,
                Lon2 = lon2,
                Lat1 = lat1,
                Lat2 = lat2,
                MapName = "mapbox.terrain-rgb",
                Zoom = zoom
            };
            var imageTask = MapDataService.GetBitmapForRegion(request);
            imageTask.Wait();

            var heightMap = TopoMapService.TransformElevationToTopoMap(imageTask.Result);

            return File(BitmapHelperService.Bitmap2Bytes(heightMap, true), "image/png");
        }

        [HttpGet("topolines/{lat1}/{lon1}/{lat2}/{lon2}/{zoom?}")]
        public IActionResult GetTopoLinesMap(float lat1, float lon1, float lat2, float lon2, int zoom=10)
        {
            var request = new MapBBoxRequest(){
                Lon1 = lon1,
                Lon2 = lon2,
                Lat1 = lat1,
                Lat2 = lat2,
                MapName = "mapbox.terrain-rgb",
                Zoom = zoom
            };
            var imageTask = MapDataService.GetBitmapForRegion(request);
            imageTask.Wait();

            var heightMap = TopoMapService.TransformElevationToTopoLinesMap(imageTask.Result);

            return File(BitmapHelperService.Bitmap2Bytes(heightMap, true), "image/png");
        }
    }
}
