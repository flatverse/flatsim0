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
    }
}