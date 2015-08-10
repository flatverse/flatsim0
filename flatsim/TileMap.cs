using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    public class TileMap
    {
        public TilePerspective perspective;

        public bool drawStructure = true;
        public bool drawSurface = true;
        public bool drawLeftface = true;
        public bool drawRightface = true;

        /*
         *     /\   <--
         *    /\/\    |
         * W /\/\/\ N
         *  /\/\/\/\
         * /\/\/\/\/\
         * \/\/\/\/\/
         *  \/\/\/\/
         * S \/\/\/ E
         *    \/\/
         *     \/
         */
        Tile[,] tiles;

        public TileMap(TilePerspective perspective)
        {
            this.perspective = perspective;
            tiles = new Tile[perspective.tilesNS, perspective.tilesWE];
            for (int ns = 0; ns < perspective.tilesNS; ns++)
            {
                for (int we = 0; we < perspective.tilesWE; we++)
                {
                    tiles[ns, we] = new Tile();
                }
            }
        }

        public virtual void update(int elapsedMillis)
        {
            for (int ns = 0; ns < tiles.GetLength(0); ns++)
            {
                for (int we = 0; we < tiles.GetLength(1); we++)
                {
                    tiles[ns, we].update(ns, we, elapsedMillis);
                }
            }
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            for (int ns = 0; ns < tiles.GetLength(0); ns++)
            {
                for (int we = 0; we < tiles.GetLength(1); we++)
                {
                    if (drawLeftface)
                    {
                        tiles[ns, we].draw(ns, we, TilePart.LEFTFACE, perspective, spriteBatch);
                    }
                    if (drawRightface)
                    {
                        tiles[ns, we].draw(ns, we, TilePart.RIGHTFACE, perspective, spriteBatch);
                    }
                    if (drawSurface)
                    {
                        tiles[ns, we].draw(ns, we, TilePart.SURFACE, perspective, spriteBatch);
                    }
                    if (drawStructure)
                    {
                        tiles[ns, we].draw(ns, we, TilePart.STRUCTURE, perspective, spriteBatch);
                    }
                }
            }
        }

        public Tile this[int ns, int we]
        {
            get { return tiles[ns, we]; }
            set { tiles[ns, we] = value; }
        }

        public int getTilesNS()
        {
            return tiles.GetLength(0);
        }

        public int getTilesWE()
        {
            return tiles.GetLength(1);
        }
    }
}
