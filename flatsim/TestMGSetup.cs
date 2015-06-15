using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace flatsim
{
    public class TestMGSetup : MonoGameSetup
    {
        public MonoGameInterface createGameInterface()
        {
            return new TestMGI();
        }
    }

    public class TestMGI : MonoGameInterface
    {
        public readonly string[] PNGS_TO_LOAD = new string[] {
            "1x1",
            "blueFramedWindow",
            "simpleBlock",
            "grass1",
            "faceLeft1",
            "faceRight1"
        };

        public Dictionary<string, FVImage> textures = new Dictionary<string,FVImage>();
        public Dictionary<string, Drawable> dbls = new Dictionary<string,Drawable>();

        public List<Drawable> testAssets = new List<Drawable>();
        public List<Vector2> testLocs = new List<Vector2>();
        public Color clearColor = Color.DarkCyan;

        /*
         */
        public override void loadContent()
        {
            initAssets();

            TextureDrawable dbl = ((TextureDrawable)dbls["blueFramedWindow"].clone());
            testAssets.Add(dbl);
            testLocs.Add(new Vector2(200, 200));
        }

        public override void unloadContent()
        {
            // noop
        }

        public override void update(GameTime gameTime)
        {
            foreach (Drawable dbl in testAssets)
            {
                dbl.update();
            }
        }

        public override void draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(clearColor);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            
            for(int i = 0; i < testAssets.Count; i++)
            {
                testAssets[i].simpleDraw(spriteBatch, testLocs[i]);
            }

            spriteBatch.End();
        }

        /*
         *helpers
         */
        public void initAssets()
        {
            foreach (string str in PNGS_TO_LOAD)
            {
                textures.Add(str, FVImage.load(content, str));
                TextureDrawable tDbl = new TextureDrawable(textures[str], 0.5f);
                tDbl.centerHandle();
                dbls.Add(str, tDbl);
            }
        }
    }
}