using System;
using Microsoft.Xna.Framework;

using flatsim;

namespace flatsim.SlopeImpls.Angle45
{
    public class SlopeAdjuster45 : TilePerspectiveAdjuster
    {
        public float priority = 0.1f;
        public TilePerspective perspective;

        public SlopeAdjuster45()
        {
        }

        public virtual void init(TilePerspective perspective)
        {
            this.perspective = perspective;
        }

        public virtual float getPriority()
        {
            return priority;
        }

        public virtual void adjust(TileDrawInfo drawInfo)
        {
            float newHeight = drawInfo.slopeInfo.getRelativeHeight(0, 0);
            Vector2 newPos = perspective.getTilePixelPosition(drawInfo.coordNS, drawInfo.coordWE, newHeight, drawInfo.tilePart);
            drawInfo.pos = newPos;
            drawInfo.adjustedHeight = newHeight;
        }
    }
}