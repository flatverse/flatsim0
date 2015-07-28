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
    }
}