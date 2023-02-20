using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame
{
    public class displayMaze:BaseMaze
    {
        public Vector2 playerPos;
        private Color playerColor;
        public displayMaze(Vector2 inputPos, Vector2[] inputBlocks, float length, Color inputColor)
        {
            position = inputPos;
            blocks = inputBlocks;
            greatest = 0;
            playerColor = inputColor;
            playerPos = new Vector2(1,1);
            foreach (Vector2 block in blocks)
            {
                if (block.X > greatest) greatest = Convert.ToInt32(block.X);
                else if (block.Y > greatest) greatest = Convert.ToInt32(block.Y);
            }
            scale = Math.Abs(length) / (greatest + 1);
        }

        public override void draw(SpriteBatch _spriteBatch)
        {
            base.draw(_spriteBatch);
            drawPoint(_spriteBatch, playerPos, Game1.blockTexture, playerColor);
            drawPoint(_spriteBatch, new Vector2(greatest - 1, greatest - 1), Game1.blockTexture, Color.Yellow);
            
        }
    }
}
