using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    public abstract class TileDrawablePack
    {
        public abstract void drawStructure(TilePerspective.TileDrawInfo drawInfo, SpriteBatch spriteBatch);
        public abstract void drawSurface(TilePerspective.TileDrawInfo drawInfo, SpriteBatch spriteBatch);
        public abstract void drawLeftFace(TilePerspective.TileDrawInfo drawInfo, SpriteBatch spriteBatch);
        public abstract void drawRightFace(TilePerspective.TileDrawInfo drawInfo, SpriteBatch spriteBatch);

        public virtual void draw(TilePerspective.TileDrawInfo drawInfo, SpriteBatch spriteBatch)
        {
            switch (drawInfo.tilePart)
            {
                case TilePart.STRUCTURE:
                    drawStructure(drawInfo, spriteBatch);
                    break;
                case TilePart.SURFACE:
                    drawSurface(drawInfo, spriteBatch);
                    break;
                case TilePart.LEFTFACE:
                    drawLeftFace(drawInfo, spriteBatch);
                    break;
                case TilePart.RIGHTFACE:
                    drawRightFace(drawInfo, spriteBatch);
                    break;
            }
        }

        public abstract float[] getHeightRangeStructure(float minHeight, float maxHeight);
        public abstract float[] getHeightRangeSurface(float minHeight, float maxHeight);
        public abstract float[] getHeightRangeLeftFace(float minHeight, float maxHeight);
        public abstract float[] getHeightRangeRightFace(float minHeight, float maxHeight);

        public virtual float[] getHeightRange(TilePart part, float minHeight, float maxHeight)
        {
            switch (part)
            {
                case TilePart.STRUCTURE:
                    return getHeightRangeStructure(minHeight, maxHeight);
                case TilePart.SURFACE:
                    return getHeightRangeSurface(minHeight, maxHeight);
                case TilePart.LEFTFACE:
                    return getHeightRangeLeftFace(minHeight, maxHeight);
                case TilePart.RIGHTFACE:
                    return getHeightRangeRightFace(minHeight, maxHeight);
            }

            return new float[] {minHeight, maxHeight};
        }
    }
}
