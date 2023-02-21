using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Texture2D blockTexture;
        public static SpriteFont font;
        public static Maze testMaze=null;


        //animations
        public static Texture2D[] walk;
        public static Texture2D[] idle;
        public static Texture2D[] down;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
        }

        protected override void Initialize()
        {
            


            // TODO: Add your initialization logic here
            blockTexture = Content.Load<Texture2D>("block");
            font = Content.Load<SpriteFont>("Score");

            //load animations

            walk = new Texture2D[6] { Content.Load<Texture2D>("player-run-1"), Content.Load<Texture2D>("player-run-2"), Content.Load<Texture2D>("player-run-3"), 
                Content.Load<Texture2D>("player-run-4"),Content.Load<Texture2D>("player-run-5"),Content.Load<Texture2D>("player-run-6") };

            idle = new Texture2D[4] { Content.Load<Texture2D>("player-idle-1"), Content.Load<Texture2D>("player-idle-2"), Content.Load<Texture2D>("player-idle-3"),Content.Load<Texture2D>("player-idle-4") };

            down = new Texture2D[3] { Content.Load<Texture2D>("player-down-1"), Content.Load<Texture2D>("player-down-2"), Content.Load<Texture2D>("player-down-3")};

            base.Initialize();

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here

            GameManager.Update(gameTime);

            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            // TODO: Add your drawing code here
            //_spriteBatch
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

            base.Draw(gameTime);
            GameManager.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}
