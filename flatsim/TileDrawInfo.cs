using System;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public class TileDrawInfo
    {
        public Direction facing;
        public TilePart tilePart;
        public Vector2 pos;
        public Vector2 scale;
        public float depth;
        public int depthDigits;

        public TileDrawInfo(Direction facing, TilePart tilePart, Vector2 pos, Vector2 scale, float depth, int depthDigits)
        {
            this.facing = facing;
            this.tilePart = tilePart;
            this.pos = pos;
            this.scale = scale;
            this.depth = depth;
            this.depthDigits = depthDigits;
        }

        public virtual Vector2 getPos()
        {
            return pos;
        }

        public virtual Vector2 getScale()
        {
            return scale;
        }

        public virtual float getAdjustedDepth(float originalDepth)
        {
            originalDepth = Utils.shiftRight(originalDepth, depthDigits);
            return depth + originalDepth;
        }
    }
}