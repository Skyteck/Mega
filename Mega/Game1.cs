using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TiledSharp;

namespace Mega
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player testGuy;
        List<Rectangle> rectList = new List<Rectangle>();

        TilemapManager _MapManager;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            testGuy = new Player();

            testGuy.LoadContent(@"Art/Player", Content);
            testGuy._Position = new Vector2(200, 0);

            _MapManager = new TilemapManager();
            _MapManager.LoadMap("ProtoLevel", Content);
            LoadCollision(_MapManager.findMapByName("ProtoLevel"));

            //for(int i = 0; i < 14; i++)
            //{
            //    Block b = new Block();
            //    b.LoadContent(@"Art/BLock", Content);
            //    b._Position = new Vector2(i * 64, 448);
            //    b.Name = i.ToString();
            //    blockList.Add(b);
            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    Block b = new Block();
            //    b.LoadContent(@"Art/BLock", Content);
            //    b._Position = new Vector2((i * 64) + 384, 320);
            //    b.Name = (i + 13).ToString();
            //    blockList.Add(b);
            //}
            // TODO: use this.Content to load your game content here
        }

        private void LoadCollision(TileMap theMap)
        {
            TmxList<TmxObject> ObjectList = theMap.FindFloors();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    Rectangle newR = new Rectangle((int)thing.X, (int)thing.Y, (int)thing.Width, (int)thing.Height);
                    rectList.Add(newR);
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHelper.Update();

            testGuy.UpdateActive(gameTime, rectList);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            _MapManager.Draw(spriteBatch);
            testGuy.Draw(spriteBatch);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
