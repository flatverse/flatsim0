﻿using System;

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
        public static float horizontalTileOffset(this TilePart tp)
        {
            switch (tp)
            {
                case TilePart.LEFTFACE:
                    return -0.25f;
                case TilePart.RIGHTFACE:
                    return 0.25f;
                default:
                    return 0;
            }
        }

        public static float verticalTileOffset(this TilePart tp)
        {
            switch (tp)
            {
                case TilePart.STRUCTURE:
                    return -0.5f;
                case TilePart.LEFTFACE:
                    return 0.5f;
                case TilePart.RIGHTFACE:
                    return 0.5f;
                default:
                    return 0;
            }
        }
    }
}