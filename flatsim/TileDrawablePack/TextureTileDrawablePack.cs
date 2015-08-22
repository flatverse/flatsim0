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

        public override void update(int elapsedMillis)
        {
            foreach (string sPart in Enum.GetNames(typeof(TilePart)))
            {
                TilePart part = (TilePart)Enum.Parse(typeof(TilePart), sPart);
                if (textures.ContainsKey(part))
                {
                    textures[part].update(part, elapsedMillis);
                }
            }
        }

        public override void drawStructure(TileDrawInfo drawInfo, SpriteBatch spriteBatch)
        {
            if (!textures.ContainsKey(TilePart.STRUCTURE)) {
                return;
            }
            textures[TilePart.STRUCTURE].draw(drawInfo, spriteBatch);
        }

        public override void drawSurface(TileDrawInfo drawInfo, SpriteBatch spriteBatch)
        {
            if (!textures.ContainsKey(TilePart.SURFACE)) {
                return;
            }
            textures[TilePart.SURFACE].draw(drawInfo, spriteBatch);
        }

        public override void drawLeftFace(TileDrawInfo drawInfo, SpriteBatch spriteBatch)
        {
            if (!textures.ContainsKey(TilePart.LEFTFACE)) {
                return;
            }
            textures[TilePart.LEFTFACE].draw(drawInfo, spriteBatch);
        }

        public override void drawRightFace(TileDrawInfo drawInfo, SpriteBatch spriteBatch)
        {
            if (!textures.ContainsKey(TilePart.RIGHTFACE)) {
                return;
            }
            textures[TilePart.RIGHTFACE].draw(drawInfo, spriteBatch);
        }

        public override float[] getHeightRangeStructure(float minHeight, float maxHeight)
        {
            if (!textures.ContainsKey(TilePart.STRUCTURE))
            {
                return new float[0];
            }
            return textures[TilePart.STRUCTURE].getHeightRange(minHeight, maxHeight);
        }

        public override float[] getHeightRangeSurface(float minHeight, float maxHeight)
        {
            if (!textures.ContainsKey(TilePart.SURFACE))
            {
                return new float[0];
            }
            return textures[TilePart.SURFACE].getHeightRange(minHeight, maxHeight);
        }
        
        public override float[] getHeightRangeLeftFace(float minHeight, float maxHeight)
        {
            if (!textures.ContainsKey(TilePart.LEFTFACE))
            {
                return new float[0];
            }
            return textures[TilePart.LEFTFACE].getHeightRange(minHeight, maxHeight);
        }
        
        public override float[] getHeightRangeRightFace(float minHeight, float maxHeight)
        {
            if (!textures.ContainsKey(TilePart.RIGHTFACE))
            {
                return new float[0];
            }
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
