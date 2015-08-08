using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    public class TextureTileDrawablePack : TileDrawablePack
    {
        public Dictionary<TilePart, TileTexture> textures;

        public TextureTileDrawablePack()
        {
            this.textures = new Dictionary<TilePart, TileTexture>();
        }

        public override void update(int ellapsedMillis)
        {
            foreach (string sPart in Enum.GetNames(typeof(TilePart)))
            {
                TilePart part = (TilePart)Enum.Parse(typeof(TilePart), sPart);
                if (textures.ContainsKey(part))
                {
                    textures[part].update(part, ellapsedMillis);
                }
            }
        }

        public override void drawStructure(TilePerspective.TileDrawInfo drawInfo, SpriteBatch spriteBatch)
        {
            textures[TilePart.STRUCTURE].draw(drawInfo, spriteBatch);
        }

        public override void drawSurface(TilePerspective.TileDrawInfo drawInfo, SpriteBatch spriteBatch)
        {
            textures[TilePart.SURFACE].draw(drawInfo, spriteBatch);
        }

        public override void drawLeftFace(TilePerspective.TileDrawInfo drawInfo, SpriteBatch spriteBatch)
        {
            textures[TilePart.LEFTFACE].draw(drawInfo, spriteBatch);
        }

        public override void drawRightFace(TilePerspective.TileDrawInfo drawInfo, SpriteBatch spriteBatch)
        {
            textures[TilePart.RIGHTFACE].draw(drawInfo, spriteBatch);
        }

        public override float[] getHeightRangeStructure(float minHeight, float maxHeight)
        {
            return textures[TilePart.STRUCTURE].getHeightRange(minHeight, maxHeight);
        }

        public override float[] getHeightRangeSurface(float minHeight, float maxHeight)
        {
            return textures[TilePart.SURFACE].getHeightRange(minHeight, maxHeight);
        }
        
        public override float[] getHeightRangeLeftFace(float minHeight, float maxHeight)
        {
            return textures[TilePart.LEFTFACE].getHeightRange(minHeight, maxHeight);
        }
        
        public override float[] getHeightRangeRightFace(float minHeight, float maxHeight)
        {
            return textures[TilePart.RIGHTFACE].getHeightRange(minHeight, maxHeight);
        }

        public override TileDrawablePack clone()
        {
            TextureTileDrawablePack newPack = new TextureTileDrawablePack();
            foreach (string sPart in Enum.GetNames(typeof(TilePart)))
            {
                TilePart part = (TilePart)Enum.Parse(typeof(TilePart), sPart);
                if (textures.ContainsKey(part))
                {
                    newPack.textures.Add(part, textures[part].clone());
                }
            }
            return newPack;
        }
    }
}