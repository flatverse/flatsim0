using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    class Tile
    {
        public int coordNS, coordWE;

        List<TileSection> tileSections;

        public Tile(int coordNS, int coordWE)
        {
            this.coordNS = coordNS;
            this.coordWE = coordWE;

            tileSections = new List<TileSection>();
        }

        public virtual void update(int timeElapsedMillis)
        {
            foreach (TileSection tile in tileSections)
            {
                tile.update(timeElapsedMillis);
            }
        }

        public virtual void draw(TilePart tilePart, TilePerspective perspective, SpriteBatch spriteBatch)
        {
            foreach (TileSection tile in tileSections)
            {
                tile.draw(coordNS, coordWE, tilePart, perspective, spriteBatch);
            }
        }
    }
}
