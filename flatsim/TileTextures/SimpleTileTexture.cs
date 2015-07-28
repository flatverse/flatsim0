using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    public class SimpleTileTexture : TileTexture
    {
        public List<Drawable> structures;
        public Drawable surface;
        public Drawable leftFace;
        public Drawable rightFace;

        public SimpleTileTexture()
        {
            structures = new List<Drawable>();
        }

        public void draw(TilePerspective.TileDrawInfo drawInfo, SpriteBatch spriteBatch)
        {
            switch (drawInfo.tilePart)
            {
                case TilePart.STRUCTURE:
                    foreach (Drawable structure in structures)
                    {
                        structure.draw(spriteBatch, drawInfo.pos, drawInfo.scale, Color.White, 0);
                    }
                    break;
                case TilePart.SURFACE:
                    surface.draw(spriteBatch, drawInfo.pos, drawInfo.scale, Color.White, 0);
                    break;
                case TilePart.LEFTFACE:
                    surface.draw(spriteBatch, drawInfo.pos, drawInfo.scale, Color.White, 0);
                    break;
                case TilePart.RIGHTFACE:
                    surface.draw(spriteBatch, drawInfo.pos, drawInfo.scale, Color.White, 0);
                    break;
            }
        }
    }
}