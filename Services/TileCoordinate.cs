namespace printmap.Services 
{
    public struct TileCoord 
    {
        int X, Y, Zoom;

        public TileCoord(int x, int y, int zoom = 16)
        {
            X = x;
            Y = y;
            Zoom = zoom;
        }


    }

}