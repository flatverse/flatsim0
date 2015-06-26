using System;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public abstract class TileDrawablePack
    {
        public abstract void drawStructure(TilePerspective.TileDrawInfo drawInfo);
        public abstract void drawSurface(TilePerspective.TileDrawInfo drawInfo);
        public abstract void drawLeftFace(TilePerspective.TileDrawInfo drawInfo);
        public abstract void drawRightFace(TilePerspective.TileDrawInfo drawInfo);

        public virtual void draw(TilePerspective.TileDrawInfo drawInfo, TilePart part)
        {
            switch (part)
            {
                case TilePart.STRUCTURE:
                    drawStructure(drawInfo);
                    break;
                case TilePart.SURFACE:
                    drawSurface(drawInfo);
                    break;
                case TilePart.LEFTFACE:
                    drawLeftFace(drawInfo);
                    break;
                case TilePart.RIGHTFACE:
                    drawRightFace(drawInfo);
                    break;
            }
        }
    }
}