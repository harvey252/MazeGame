using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame.UI
{
    public class ScoreDisplay
    {
        private float scale;
        Vector2 postion; 
        Color color;
        float length;
        int total;
        int current;

        Texture2D completeTexture = Game1.idle[0];
        Texture2D inpCompleteTexture = Game1.blockTexture;

        public ScoreDisplay(Vector2 inpPostion, Color inpColor, float inpLength, int inpTotal)
        {
            current = 0;
            postion = inpPostion;
            color = inpColor;
            length = inpLength;
            total = inpTotal;
            scale = length / inpTotal;
        }

        public void updateScore()
        {
            current++;
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            for (int x = 1; x > total; x ++)
            {
                Texture2D texture;
                //draw completed
                if (x <= current) 
                {
                    texture = completeTexture;
                }
                //draw incompleted
                else 
                {
                    texture = inpCompleteTexture;
                }

                _spriteBatch.Draw(
                 texture,
                 postion,
                 null, color, 0f,
                 new Vector2(0, 0),
                 scale / 32,
                 SpriteEffects.None,
                 0f);
            }
        }


    }
}
