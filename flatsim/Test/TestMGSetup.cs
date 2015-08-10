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

        public Color clearColor = Color.FromNonPremultiplied(20, 20, 20, 255);

        public TileTexture baseLeft = new SimpleTileTexture();
        public TileTexture baseRight = new SimpleTileTexture();
        public TileTexture baseSurface = new SimpleTileTexture();
        public TileDrawablePack basePack = new TextureTileDrawablePack();
        public TileMap tileMap;

        /*
         */
        public override void loadContent()
        {
            initAssets();

            (baseLeft as SimpleTileTexture).leftFace = dbls["faceLeft1"];
            (baseLeft as SimpleTileTexture).tileHeight = 1;
            (baseRight as SimpleTileTexture).rightFace = dbls["faceRight1"];
            (baseRight as SimpleTileTexture).tileHeight = 1;
            (baseSurface as SimpleTileTexture).surface = dbls["grass1"];

            TextureTileDrawablePack texPack = basePack as TextureTileDrawablePack;
            texPack.textures.Add(TilePart.LEFTFACE, baseLeft);
            texPack.textures.Add(TilePart.RIGHTFACE, baseRight);
            texPack.textures.Add(TilePart.SURFACE, baseSurface);

            tileMap = new TileMap(new TilePerspective(128, 2, 2));
            tileMap.perspective.position.X += 200;
            tileMap.perspective.position.Y += 200;
            //tileMap.drawLeftface = false;
            //tileMap.drawRightface = false;
            for (int ns = 0; ns < tileMap.getTilesNS(); ns++)
            {
                for (int we = 0; we < tileMap.getTilesWE(); we++)
                {
                    tileMap[ns, we].addTileSection(0, 1, basePack.clone());
                }
            }
        }

        public override void unloadContent()
        {
            // noop
        }

        public override void update(GameTime gameTime)
        {
            tileMap.update(gameTime.ElapsedGameTime.Milliseconds);
        }

        public override void draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(clearColor);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            tileMap.draw(spriteBatch);

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