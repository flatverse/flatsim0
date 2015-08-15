using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    public class TileSection
    {
        public float minHeight = 0;
        public float maxHeight = 0;
       
        public TileDrawablePack drawablePack;

        public TileSection(TileDrawablePack drawablePack)
        {
            this.drawablePack = drawablePack;
        }

        public virtual void update(int timeElapsedMillis)
        {
            drawablePack.update(timeElapsedMillis);
        }

        public virtual void draw(int coordNS, int coordWE, TilePart tilePart, TilePerspective perspective, SpriteBatch spriteBatch)
        {
            float[] heights = drawablePack.getHeightRange(tilePart, minHeight, maxHeight);
            TileDrawInfo tdi;
            foreach (float height in heights)
            {
                tdi = perspective.getTileDrawInfo(coordNS, coordWE, height, tilePart, null);
                drawablePack.draw(tdi, spriteBatch);
            }
        }
    }
}