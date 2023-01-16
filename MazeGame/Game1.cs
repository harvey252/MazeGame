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
        public static Maze testMaze=null;
        private string type;
        private int size;
        private float speed;
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
            Console.WriteLine("hello wellcome the first prototype of my maze game\n" +
                "the goal of this game is to get the the yellow square\n" +
                "you can experement with different maze gereration methods\n" +
                "you may need to click on the window to move the player (WASD or arrow keys)\n" +
                "think about which mazes are harder and what size you find the most fun to play\n" +
                "at the end you can complete a short questionair");
            base.Initialize();
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

            if (testMaze == null || testMaze.player.win)
            {
                Console.WriteLine("maze type 1 baisc generation, 2 medium generation, or 3 complex generation (best one) ");
                type = (string)Console.ReadLine();
                while (type != "1" && type != "2" && type != "3")
                {
                    Console.WriteLine("type in valid 1, 2 or 3 ");
                    type = (string)Console.ReadLine();
                }

                Console.WriteLine("size");
                string temp = Console.ReadLine();
                bool sizevalid = false;
                while (!sizevalid)
                {
                    try
                    {
                        size = Convert.ToInt32(temp);
                        if (size > 40)
                        {
                            Console.WriteLine("too large");
                            sizevalid = false;
                            temp = Console.ReadLine();
                        }
                        else if (size == 1)
                        {
                            Console.WriteLine("too small");
                            sizevalid = false;
                            temp = Console.ReadLine();
                        }
                        else
                            sizevalid = true;
                    }
                    catch (Exception)
                    {
                        sizevalid = false;
                        Console.WriteLine("enter valid size");
                        temp = Console.ReadLine();
                    }
                }

                Console.WriteLine("speed (4 recomended)");
                temp = Console.ReadLine();
                bool speedvalid = false;
                while (!speedvalid)
                {
                    try
                    {
                        speed = Convert.ToInt32(temp);
                        if (speed <= 0)
                        {
                            Console.WriteLine("too small");
                            speedvalid = false;
                            temp = Console.ReadLine();
                        }
                        else if(speed>=50)
                        {
                            Console.WriteLine("too large");
                            speedvalid = false;
                            temp = Console.ReadLine();
                        }

                        else
                            speedvalid = true;
                    }
                    catch (Exception)
                    {
                        speedvalid = false;
                        Console.WriteLine("enter valid speed");
                        temp = Console.ReadLine();
                    }

                    
                }
                if (type == "1")
                    testMaze = new Maze(new Vector2(0, 0), MazeGenerator.generateBinary(size), 480);
                else if (type == "2")
                    testMaze = new Maze(new Vector2(0, 0), MazeGenerator.generateSideWidener(size), 480);
                else
                    testMaze = new Maze(new Vector2(0, 0), MazeGenerator.generateWilsons(size), 480);
                testMaze.player.speed = speed;

            }
            
            
            testMaze.update(gameTime);
            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            // TODO: Add your drawing code here
            //_spriteBatch
            _spriteBatch.Begin();
            testMaze.draw(_spriteBatch);
            base.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}
