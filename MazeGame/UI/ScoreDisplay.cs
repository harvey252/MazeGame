using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    public class ScoreDisplay
    {
        private float scale;
        Vector2 postion; 
        Color color;
        float length;
        int total;
        int current;

        int rows;
        int rowCount;

        Texture2D completeTexture = Game1.idle[0];
        Texture2D inpCompleteTexture = Game1.path;

        public ScoreDisplay(Vector2 inpPostion, Color inpColor, float inpLength, int inpTotal)
        {
            current = 0;
            postion = inpPostion;
            color = inpColor;
            length = inpLength;
            total = inpTotal;
            scale = length / inpTotal;


            //creating rows asumes the obejct is a squair
            rows = (int)Math.Ceiling(Math.Sqrt(total));
            //finds number of objects in a row
            //row count exits in case I ever whant to include different hieghts and widths for different screen sizes
            rowCount = rows;
            if (rows>1)
            {
                scale = length / rows;
            }

        }

        public void updateScore()
        {
            current++;
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            int y = 0;
            for (int n = 0; n < total; n ++)
            {

                //checking for incrementing next row
                if (n - y * rowCount == rowCount)
                    y++;
                Texture2D texture;
                //draw completed
                if (n < current) 
                {
                    texture = completeTexture;
                }
                //draw incompleted
                else 
                {
                    texture = inpCompleteTexture;
                }


                //postion is adjusted for y
                _spriteBatch.Draw(
                 texture,
                 new Vector2(postion.X + (n-y*rowCount)*scale,postion.Y+y*length/rows),
                 null, color, 0f,
                 new Vector2(0, 0),
                 scale / 32,
                 SpriteEffects.None,
                 0f);
                
                
            }
        }


    }
}
