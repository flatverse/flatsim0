using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace flatsim
{
    public class FVImage
    {
        public static FVImage load(ContentManager content, string assetName)
        {
            Texture2D texture = content.Load<Texture2D>(assetName);
            return new FVImage(texture, null);
        }

        public Texture2D texture;
        public Rectangle? sourceRect;

        public FVImage(Texture2D texture, Rectangle? sourceRect)
        {
            this.texture = texture;
            this.sourceRect = sourceRect;
        }

        public FVImage(Texture2D texture, int srcX, int srcY, int srcWidth, int srcHeight)
            : this(texture, new Rectangle(srcX, srcY, srcWidth, srcHeight))
        {}

        public virtual int getWidth()
        {
            if (sourceRect != null)
            {
                return ((Rectangle)sourceRect).Width;
            }
            return texture.Width;
        }

        public virtual int getHeight()
        {
            if (sourceRect != null)
            {
                return ((Rectangle)sourceRect).Height;
            }
            return texture.Height;
        }

        public virtual FVImage clone()
        {
            return new FVImage(texture, sourceRect);
        }
    }
}