﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    public class Maze
    {
        public Vector2[] blocks;
        private float scale;
        public Vector2 position;
        
        public Maze(Vector2 inputPos, Vector2[] inputBlocks, float length)
        {
            position = inputPos;
            blocks = inputBlocks;
            float greatest = 0;
            foreach(Vector2 block in blocks)
            {
                if (block.X > greatest) greatest = block.X;
                else if (block.Y > greatest) greatest = block.Y;
            }

            scale = length /(greatest+1);
            
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            foreach (Vector2 block in blocks)
            {
                _spriteBatch.Draw(
                    Game1.blockTexture,
                    new Vector2(block.X*scale+position.X,block.Y*scale+position.Y),
                    null, 
                    Color.LightGray,
                    0f,
                    new Vector2(0, 0),
                    scale/32,
                    SpriteEffects.None,
                    0f);
            }
        }
    }
}