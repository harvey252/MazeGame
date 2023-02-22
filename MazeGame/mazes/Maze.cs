using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;


namespace MazeGame
{
    public class Maze:BaseMaze
    {

        public Player player;
        private Random random;


        
        public Maze(Vector2 inputPos, Vector2[] inputBlocks, float length)
        {
            random = new Random();
            walltexture = Game1.wallTexures[random.Next(0,Game1.wallTexures.Length)];

            if(walltexture == Game1.wallTexures[0])
            {
                //Program.chargeBGcolor(Color.LightGreen);
            }
            else
            {
                //Game1.chargeBGcolor(Color.LightGray);
            }

            position = inputPos;
            blocks = inputBlocks;
            greatest = 0;
            foreach(Vector2 block in blocks)
            {
                if (block.X > greatest) greatest = Convert.ToInt32(block.X);
                else if (block.Y > greatest) greatest = Convert.ToInt32(block.Y);
            }

            scale = System.Math.Abs( length) /(greatest+1);
            player = new Player(new Vector2(1, 1), new Vector2(greatest - 1, greatest - 1));
        }

        public override void draw(SpriteBatch _spriteBatch)
        {
            base.draw(_spriteBatch);
            Maze dummy = this;
            player.draw(_spriteBatch, ref dummy);
        }

        public void update(GameTime gameTime)
        {
            Maze dummy = this;
            player.update(gameTime, ref dummy);
        }
    }
}