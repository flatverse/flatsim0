﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    public class Tile
    {
        List<TileSection> tileSections;

        public Tile()
        {
            tileSections = new List<TileSection>();
        }

        public virtual void update(int coordNS, int coordWE, int timeElapsedMillis)
        {
            foreach (TileSection tile in tileSections)
            {
                tile.update(timeElapsedMillis);
            }
        }

        public virtual void draw(int coordNS, int coordWE, TilePart tilePart, TilePerspective perspective, SpriteBatch spriteBatch)
        {
            foreach (TileSection tile in tileSections)
            {
                tile.draw(coordNS, coordWE, tilePart, perspective, spriteBatch);
            }
        }

        public virtual int addTileSection(TileSection section)
        {
            int ix = tileSections.Count;
            tileSections.Add(section);
            return ix;
        }

        public virtual int addTileSection(int minHeight, int maxHeight, TileDrawablePack dblPack)
        {
            TileSection newTS = new TileSection(dblPack);
            newTS.minHeight = minHeight;
            newTS.maxHeight = maxHeight;
            return addTileSection(newTS);
        }

        public virtual TileSection getTileSection(int index)
        {
            return tileSections[index];
        }
    }
}
