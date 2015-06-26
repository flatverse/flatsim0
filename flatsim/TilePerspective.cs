using System;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public class TilePerspective
    {
        public class TileDrawInfo
        {
            public Direction facing;
            public Vector2 pos;
            public Vector2 scale;
            public float depth;
            public int depthDigits;

            public TileDrawInfo(Direction facing, Vector2 pos, Vector2 scale, float depth, int depthDigits)
            {
                this.facing = facing;
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

        public virtual TileDrawInfo getTileDrawInfo(int coordNS, int coordWE, float height, TilePart part)
        {
            Direction facing = getDirectionFacing();
            Vector2 pxPos = getTilePixelPosition(coordNS, coordWE, height, part);
            Vector2 scale = getScale();
            float depth = getTileDepth(0, coordNS, coordWE, part);
            int digitCount = getDepthDigitsNeeded(0);
            TileDrawInfo tdi = new TileDrawInfo(facing, pxPos, scale, depth, digitCount);

            return tdi;
        }

        public virtual Direction getDirectionFacing()
        {
            return facing;
        }

        // TODO use 'part'
        public virtual Vector2 getTilePixelPosition(int coordNS, int coordWE, float height, TilePart part)
        {
            // TODO take scale into account
            float halfWidth = tilePixelWidth / 2;
            float halfHeight = tilePixelHeight / 2;
            Vector2 centerCoord = getCenterCoord();
            Tuple<float, float> dist = getTileDistance(centerCoord, new Vector2(coordNS, coordWE));

            // item1 is rel. tile coord top left to bottom right
            // item2 is rel. tile coord bottom left to top right
            float xAdjust = dist.Item1 + dist.Item2;
            xAdjust *= halfWidth;
            xAdjust += part.horizontalTileOffset() * tilePixelWidth;
            float yAdjust = dist.Item2 + (-dist.Item1);
            yAdjust *= halfHeight;
            yAdjust += part.verticalTileOffset() * tilePixelAltitudeUnit; // this is a bit heavy handed might want to remove

            Vector2 pxPos = new Vector2(position.X + xAdjust, position.Y + yAdjust);
            pxPos.Y += height * tilePixelAltitudeUnit;
            return pxPos;
        }

        public virtual Vector2 getScale()
        {
            return scale;
        }

        public virtual float getTileDepth(int extraDigits, int coordNS, int coordWE, TilePart part)
        {
            int digitsNeeded = getDepthDigitsNeeded(extraDigits);

            Tuple<float, float> dist = getTileDistance(getTopCoord(), new Vector2(coordNS, coordWE));
            int drawOrder = (int)dist.Item2 * getTilesTLtoBR();
            drawOrder += (int)dist.Item1;

            float depth = Utils.shiftRight(drawOrder, digitsNeeded - 1);
            depth += Utils.shiftRight((int)part, digitsNeeded);
            
            return depth;
        }

        public virtual int getDepthDigitsNeeded(int extraDigits)
        {
            int digitsNeeded = Utils.digitCount(tilesNS * tilesWE);
            digitsNeeded += extraDigits + 1; // the extra digit is for the TilePart
            return digitsNeeded;
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

        public virtual Vector2 getTopCoord()
        {
            switch (facing)
            {
                case Direction.WESTNORTH:
                    return new Vector2(0, 0);
                case Direction.SOUTHWEST:
                    return new Vector2(tilesNS - 1, 0);
                case Direction.EASTSOUTH:
                    return new Vector2(tilesNS - 1, tilesWE - 1);
                case Direction.NORTHEAST:
                    return new Vector2(0, tilesWE - 1);
            }

            return new Vector2(float.NaN);
        }

        public virtual int getTilesTLtoBR()
        {
            if (facing == Direction.WESTNORTH || facing == Direction.EASTSOUTH)
            {
                return tilesWE;
            }
            else
            {
                return tilesNS;
            }
        }

        public virtual int getTilesBLtoTR()
        {
            if (facing == Direction.WESTNORTH || facing == Direction.EASTSOUTH)
            {
                return tilesNS;
            }
            else
            {
                return tilesWE;
            }
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