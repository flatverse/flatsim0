using System;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public interface TileTexture
    {
        void draw(TilePerspective.TileDrawInfo drawInfo);
    }
}