using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace flatsim
{
    public abstract class MonoGameInterface
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public ContentManager content;

        public virtual void init(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, ContentManager content)
        {
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;
            this.content = content;
        }

        public abstract void loadContent();

        public abstract void unloadContent();

        public abstract void update(GameTime gameTime);

        public abstract void draw(GameTime gameTime);
    }

    public interface MonoGameSetup
    {
        MonoGameInterface createGameInterface();
    }
}