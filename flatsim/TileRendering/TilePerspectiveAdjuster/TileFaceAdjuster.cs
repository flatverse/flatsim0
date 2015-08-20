using System;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public class TileFaceAdjuster : TilePerspectiveAdjuster
    {
        public float leftShift = -0.25f;
        public float rightShift = 0.25f;

        public readonly float priority;

        public TilePerspective perspective;

        public TileFaceAdjuster(float priority) {
            this.priority = priority;
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
            if (drawInfo.tilePart == TilePart.LEFTFACE)
            {
                float pxShiftX = leftShift * perspective.tilePixelWidth;
                drawInfo.pos.X += pxShiftX;
            }
            else if (drawInfo.tilePart == TilePart.RIGHTFACE)
            {
                float pxShiftX = rightShift * perspective.tilePixelWidth;
                drawInfo.pos.X += pxShiftX;
            }
        }
    }
}