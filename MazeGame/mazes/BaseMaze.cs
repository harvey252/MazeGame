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

        public Texture2D walltexture = Game1.blockTexture;

        public Color BGcolor;

        public int greatest;
        public float length;

        public virtual void draw(SpriteBatch _spriteBatch)
        {
            //drawing back ground
            if(BGcolor!=null)
            {
                _spriteBatch.Draw(
                 Game1.blockTexture,
                 position,
                 null, BGcolor, 0f,
                 new Vector2(0, 0),
                 length/32,
                 SpriteEffects.None,
                 0f);
            }


            //drawing all tiles
            foreach (Vector2 block in blocks)
            {
                drawPoint(_spriteBatch, block, walltexture, Color.White);
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
