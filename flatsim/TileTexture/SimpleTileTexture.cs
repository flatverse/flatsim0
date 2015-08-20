using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    public class SimpleTileTexture : TileTexture
    {
        public List<Drawable> structures;
        public Drawable surface;
        public Drawable leftFace;
        public Drawable rightFace;

        public float heightOffset = 0;
        public float tileHeight = 0;

        public SimpleTileTexture()
        {
            structures = new List<Drawable>();

            heightOffset = 0;
            tileHeight = 0;
        }

        public virtual void update(TilePart part, int ellapsedMillis)
        {
            if (part == TilePart.STRUCTURE)
            {
                foreach (Drawable dbl in structures)
                {
                    dbl.update();
                }
            }
            else
            {
                Drawable dbl = getDrawable(part, -1);
                dbl.update();
            }
        }

        public virtual void draw(TileDrawInfo drawInfo, SpriteBatch spriteBatch)
        {
            switch (drawInfo.tilePart)
            {
                case TilePart.STRUCTURE:
                    foreach (Drawable structure in structures)
                    {
                        adjustedDepthDraw(structure, drawInfo, spriteBatch);
                    }
                    break;
                case TilePart.SURFACE:
                    if (surface == null)
                    {
                        break;
                    }
                    adjustedDepthDraw(surface, drawInfo, spriteBatch);
                    break;
                case TilePart.LEFTFACE:
                    if (leftFace == null)
                    {
                        break;
                    }
                    adjustedDepthDraw(leftFace, drawInfo, spriteBatch);
                    break;
                case TilePart.RIGHTFACE:
                    if (rightFace == null)
                    {
                        break;
                    }
                    adjustedDepthDraw(rightFace, drawInfo, spriteBatch);
                    break;
            }
        }

        protected virtual void adjustedDepthDraw(Drawable dbl, TileDrawInfo tdi, SpriteBatch sb)
        {
            float origDepth = dbl.depth;
            dbl.depth = tdi.getAdjustedDepth(origDepth);
            dbl.draw(sb, tdi.pos, tdi.scale, Color.White, 0);
            dbl.depth = origDepth;
        }

        public virtual float[] getHeightRange(float minHeight, float maxHeight)
        {
            if (tileHeight == 0)
            {
                return new float[] {maxHeight + heightOffset};
            }
            else
            {
                int tileCnt = (int)Math.Ceiling((maxHeight - minHeight) / tileHeight);
                float[] heights = new float[tileCnt];
                float curHeight = maxHeight - 1;
                for (int i = 0; i < tileCnt; i++)
                {
                    heights[i] = curHeight;
                    curHeight -= tileHeight;
                }
                return heights;
            }
        }

        public virtual TileTexture clone()
        {
            SimpleTileTexture newTex = new SimpleTileTexture();
            newTex.tileHeight = tileHeight;
            newTex.heightOffset = heightOffset;
            foreach (Drawable dbl in structures)
            {
                newTex.structures.Add(dbl.clone());
            }
            if (leftFace != null)
            {
                newTex.leftFace = leftFace.clone();
            }
            if (rightFace != null)
            {
                newTex.rightFace = rightFace.clone();
            }
            if (surface != null)
            {
                newTex.surface = surface.clone();
            }
            return newTex;
        }

        /*
         * helpers
         */
        /// <summary>
        /// returns the drawable based on the TilePart provided and
        /// the structure index where applicable.
        /// </summary>
        /// <param name="part">Specifies which drawable to return</param>
        /// <param name="structureIndex">Only used when 'part' is STRUCTURE</param>
        /// <returns></returns>
        public virtual Drawable getDrawable(TilePart part, int structureIndex)
        {
            switch (part)
            {
                case TilePart.STRUCTURE:
                    return structures[structureIndex];
                case TilePart.SURFACE:
                    return surface;
                case TilePart.LEFTFACE:
                    return leftFace;
                case TilePart.RIGHTFACE:
                    return rightFace;
            }
            return null;
        }
    }
}