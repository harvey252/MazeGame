using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;


namespace MazeGame
{
    public class Maze
    {
        public Vector2[] blocks;
        private float scale;
        public Vector2 position;
        public Player player;


        //value of greaset point in maze needed by player for errror checking
        private int _greatest;
        //getter for greatest
        public int Greastest { 
            get { return _greatest; } 
        }
        
        public Maze(Vector2 inputPos, Vector2[] inputBlocks, float length)
        {
            position = inputPos;
            blocks = inputBlocks;
            _greatest = 0;
            foreach(Vector2 block in blocks)
            {
                if (block.X > _greatest) _greatest = Convert.ToInt32(block.X);
                else if (block.Y > _greatest) _greatest = Convert.ToInt32(block.Y);
            }

            scale = System.Math.Abs( length) /(_greatest+1);
            player = new Player(new Vector2(1, 1), new Vector2(_greatest - 1, _greatest - 1));
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            foreach (Vector2 block in blocks)
            {
                drawPoint(_spriteBatch, block, Game1.blockTexture, Color.DarkBlue);
            }
            Maze dummy = this;
            player.draw(_spriteBatch, ref dummy);
        }
        public void drawPoint(SpriteBatch _spriteBatch, Vector2 pos, Texture2D texture, Color color)
        {
            _spriteBatch.Draw(
                 texture,
                 new Vector2(pos.X * scale + position.X, pos.Y * scale + position.Y),
                 null, color, 0f,
                 new Vector2(0, 0), 
                 scale / 32,
                 SpriteEffects.None,
                 0f);

        }
        public void update(GameTime gameTime)
        {
            Maze dummy = this;
            player.update(gameTime, ref dummy);
        }
    }
}