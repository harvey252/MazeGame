using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    public class Maze
    {
        public Vector2[] blocks;
        private float scale;
        public Vector2 position;
        public Player player = new Player(new Vector2(1,1));
        
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

            scale = System.Math.Abs( length) /(greatest+1);
            
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            foreach (Vector2 block in blocks)
            {
                drawPoint(_spriteBatch, block, Game1.blockTexture, Color.White);
            }
            player.draw(_spriteBatch);
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
            player.update(gameTime);
        }
    }
}