﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    public interface TileTexture
    {
        void update(TilePart part, int elapsedMillis);
        void draw(TileDrawInfo drawInfo, SpriteBatch spriteBatch);
        float[] getHeightRange(float minHeight, float maxHeight);
        TileTexture clone();
    }
}