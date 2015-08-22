using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    public class TextureDrawable : Drawable
    {
        public FVImage image;

        public TextureDrawable(FVImage image, float depth)
            : base(depth)
        {
            this.image = image;
        }

        public override void update(int elapsedMillis)
        {}

        public override void draw(SpriteBatch spriteBatch, Vector2 pos, Vector2 scale, Color color, float lerpVal)
        {
            pos = getOffsetPos(pos);
            scale = getMultipliedScale(scale);
            color = getLerpedColor(color, lerpVal);

            spriteBatch.Draw(image.texture, pos, image.sourceRect, color, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
        }

        public override Drawable clone()
        {
            TextureDrawable newInst = new TextureDrawable(image.clone(), depth);
            newInst.scale = scale;
            newInst.color = color;
            newInst.handle = handle;
            return newInst;
        }

        public override void centerHandle()
        {
            float halfW = image.getWidth() / 2;
            float halfH = image.getHeight() / 2;
            handle = new Vector2(-halfW, -halfH);
        }
    }
}