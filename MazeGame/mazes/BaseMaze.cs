using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame
{
    public abstract class BaseMaze
    {
        public Vector2[] blocks;
        public float scale;
        public Vector2 position;

        public int greatest;

        public virtual void draw(SpriteBatch _spriteBatch)
        {
            foreach (Vector2 block in blocks)
            {
                drawPoint(_spriteBatch, block, Game1.blockTexture, Color.DarkBlue);
            }
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

        public void drawPoint(SpriteBatch _spriteBatch, Vector2 pos, Texture2D texture, Color color, SpriteEffects spriteEffects)
        {
            _spriteBatch.Draw(
                 texture,
                 new Vector2(pos.X * scale + position.X, pos.Y * scale + position.Y),
                 null, color, 0f,
                 new Vector2(0, 0),
                 scale / 32,
                 spriteEffects,
                 0f);

        }

    }
}
