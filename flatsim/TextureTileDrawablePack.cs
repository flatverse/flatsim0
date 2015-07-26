using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public class TextureTileDrawablePack : TileDrawablePack
    {
        public Dictionary<TilePart, TileTexture> textures;

        public TextureTileDrawablePack()
        {
            this.textures = new Dictionary<TilePart, TileTexture>();
        }

        public override void drawStructure(TilePerspective.TileDrawInfo drawInfo)
        {
            textures[TilePart.STRUCTURE].draw(drawInfo);
        }

        public override void drawSurface(TilePerspective.TileDrawInfo drawInfo)
        {
            textures[TilePart.SURFACE].draw(drawInfo);
        }

        public override void drawLeftFace(TilePerspective.TileDrawInfo drawInfo)
        {
            textures[TilePart.LEFTFACE].draw(drawInfo);
        }

        public override void drawRightFace(TilePerspective.TileDrawInfo drawInfo)
        {
            textures[TilePart.RIGHTFACE].draw(drawInfo);
        }
    }
}