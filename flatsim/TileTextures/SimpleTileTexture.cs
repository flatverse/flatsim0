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

        float heightOffset = 0;
        float tileHeight = 0;

        public SimpleTileTexture()
        {
            structures = new List<Drawable>();

            heightOffset = 0;
            tileHeight = 0;
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

        public float[] getHeightRange(float minHeight, float maxHeight)
        {
            if (tileHeight == 0)
            {
                return new float[] {maxHeight + heightOffset};
            }
            else
            {
                int tileCnt = (int)Math.Ceiling((maxHeight - minHeight) / tileHeight);
                float[] heights = new float[tileCnt];
                float curHeight = maxHeight - (tileHeight / 2);
                for (int i = 0; i < tileCnt; i++)
                {
                    heights[i] = curHeight;
                    curHeight += tileHeight;
                }
                return heights;
            }
        }
    }
}