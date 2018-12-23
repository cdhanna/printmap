namespace printmap.Services.MapDataServices
{
    public struct TileCoord 
    {
        public int X, Y, Zoom;

        public TileCoord(int x, int y, int zoom = 16)
        {
            X = x;
            Y = y;
            Zoom = zoom;
        }
    }

}