using System;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public class TilePerspective
    {
        public class TileDrawInfo
        {
            public Vector2 pos;
            public Vector2 scale;
            public float depth;
            public int depthDigits;

            public TileDrawInfo(Vector2 pos, float depth, int depthDigits)
            {
                this.pos = pos;
                scale = new Vector2(1, 1);
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

        /*
         *     /\   <--
         *    /\/\    |
         * W /\/\/\ N
         *  /\/\/\/\
         * /\/\/\/\/\
         * \/\/\/\/\/
         *  \/\/\/\/
         * S \/\/\/ E
         *    \/\/
         *     \/
         */

        public float tilePixelWidth;
        public float tilePixelHeight;
        public float tilePixelAltitudeUnit;
        public int tilesNS, tilesWE;
        public int depthShift = 1;

        public Vector2 position;
        public Vector2 scale;
        public Direction facing;

        public TilePerspective(float tilePixelWidth, int tilesNS, int tilesWE)
        {
            this.tilePixelWidth = tilePixelWidth;
            tilePixelHeight = tilePixelWidth / (float)Math.Sqrt(3);
            tilePixelAltitudeUnit = tilePixelHeight / 2;
            this.tilesNS = tilesNS;
            this.tilesWE = tilesWE;

            position = new Vector2(0, 0);
            scale = new Vector2(1, 1);
            facing = Direction.WESTNORTH;
        }

        public virtual Direction getDirectionFacing()
        {
            return facing;
        }

        public virtual Vector2 getTilePixelPosition(int coordNS, int coordWE, float height)
        {
            float halfWidth = tilePixelWidth / 2;
            float halfHeight = tilePixelHeight / 2;
            Vector2 centerCoord = getCenterCoord();
            Tuple<float, float> dist = getTileDistance(centerCoord, new Vector2(coordNS, coordWE));

            // item1 is rel. tile coord top left to bottom right
            // item2 is rel. tile coord bottom left to top right
            float xAdjust = dist.Item1 + dist.Item2;
            xAdjust *= halfWidth;
            float yAdjust = dist.Item2 + (-dist.Item1);
            yAdjust *= halfHeight;

            Vector2 pxPos = new Vector2(position.X + xAdjust, position.Y + yAdjust);
            return pxPos;
        }

        /*
         * helpers
         */
        public virtual Vector2 getCenterCoord()
        {
            // TODO figure out if we need to do something like this
            //if (facing == Direction.WESTNORTH || facing == Direction.EASTSOUTH)
            // else
            return new Vector2((float)tilesNS / 2.0f, (float)tilesWE / 2.0f);
        }

        public virtual Tuple<float, float> getTileDistance(Vector2 from, Vector2 to)
        {
            float fromNS = from.X;
            float fromWE = from.Y;
            float toNS = to.X;
            float toWE = to.Y;

            float offsetTLtoBR, offsetBLtoTR;

            switch (facing)
            {
                case Direction.WESTNORTH:
                    offsetTLtoBR = toWE - fromWE;
                    offsetBLtoTR = fromNS - toNS;
                    break;
                case Direction.SOUTHWEST:
                    offsetTLtoBR = fromNS - toNS;
                    offsetBLtoTR = fromWE - toWE;
                    break;
                case Direction.EASTSOUTH:
                    offsetTLtoBR = fromWE - toWE;
                    offsetBLtoTR = toNS - fromNS;
                    break;
                case Direction.NORTHEAST:
                    offsetTLtoBR = toNS - fromNS;
                    offsetBLtoTR = toWE - fromWE;
                    break;
                default:
                    offsetTLtoBR = float.NaN;
                    offsetBLtoTR = float.NaN;
                    break;
            }

            // using a Tuple just to add some structure difference between a tile coordinate which is 
            // affected by facing direction and tile distance which is not affected by facing direction
            // TODO change this to it's own struct or class to give better names the the return values
            // other than item1 and item2
            return new Tuple<float, float>(offsetTLtoBR, offsetBLtoTR);
        }
    }
}