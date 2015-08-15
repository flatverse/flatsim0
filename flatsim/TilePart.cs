using System;

namespace flatsim
{
    public enum TilePart
    {
        STRUCTURE = 3,
        SURFACE = 2,
        LEFTFACE = 0,
        RIGHTFACE = 1
    }

    public static class TilePartExtension
    {
        public static float verticalTileOffset(this TilePart tp)
        {
            switch (tp)
            {
                case TilePart.STRUCTURE:
                    return -0.5f;
                case TilePart.LEFTFACE:
                    return 0f;
                case TilePart.RIGHTFACE:
                    return 0f;
                default:
                    return 0;
            }
        }
    }
}