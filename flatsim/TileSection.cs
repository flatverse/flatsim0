using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    public class TileSection
    {
        float minHeight = 0;
        float maxHeight = 0;
       
        TileDrawablePack drawablePack;

        public TileSection(TileDrawablePack drawablePack)
        {
            this.drawablePack = drawablePack;
        }

        public virtual void update(int timeElapsedMillis)
        {
            // TODO update drawablePack
        }

        public virtual void draw(int coordNS, int coordWE, TilePart tilePart, TilePerspective perspective, SpriteBatch spriteBatch)
        {
            float[] heights = drawablePack.getHeightRange(tilePart, minHeight, maxHeight);
            TilePerspective.TileDrawInfo tdi;
            foreach (float height in heights)
            {
                tdi = perspective.getTileDrawInfo(coordNS, coordWE, height, tilePart);
                drawablePack.draw(tdi, spriteBatch);
            }
        }
    }
}