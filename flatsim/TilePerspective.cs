using System;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public class TilePerspective
    {


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
        public int depthShift = 0;

        public Vector2 position;
        public Vector2 scale;
        public Direction facing;

        public TilePerspectiveAdjusterManager adjusters;

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

            adjusters = new TilePerspectiveAdjusterManager();
        }

        public virtual void initAdjusters()
        {
            adjusters.init(this);
        }

        public virtual TileDrawInfo getTileDrawInfo(int coordNS, int coordWE, float height, TilePart part, string slope)
        {
            Direction facing = getDirectionFacing();
            Vector2 pxPos = getTilePixelPosition(coordNS, coordWE, height, part);
            Vector2 scale = getScale();
            float depth = getTileDepth(depthShift, coordNS, coordWE, part);
            int digitCount = getDepthDigitsNeeded(depthShift);
            TileDrawInfo tdi = new TileDrawInfo(coordNS, coordWE, height, facing, part, slope, pxPos, scale, depth, digitCount);

            adjusters.adjust(tdi);

            return tdi;
        }

        public virtual void addAdjuster(TilePerspectiveAdjuster adjuster)
        {
            adjusters.add(adjuster);
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
            float yAdjust = -dist.Item2 + dist.Item1;
            yAdjust *= halfHeight;

            Vector2 pxPos = new Vector2(position.X + xAdjust, position.Y + yAdjust);
            pxPos.Y -= height * tilePixelAltitudeUnit;
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
            int drawOrder = -(int)dist.Item2 * getTilesBLtoTR();
            drawOrder += (int)dist.Item1;

            float depth = Utils.shiftRight(drawOrder, digitsNeeded - 1);
            float partDepth = Utils.shiftRight((int)part, digitsNeeded);
            depth += partDepth;
            
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
            return new Vector2(((float)tilesNS / 2.0f) - 0.5f, ((float)tilesWE / 2.0f) - 0.5f);
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