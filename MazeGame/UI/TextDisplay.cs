using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace MazeGame
{
    public class TextDisplay
    {
        Vector2 position;
        double scale;
        Color color;
        public TextDisplay(Vector2 inpPos, Color inpColor, double inpScale)
        {
            position = inpPos;
            scale = inpScale;
            color = inpColor;
        }

        public void Draw(SpriteBatch _spriteBatch,string sting)
        {
            _spriteBatch.DrawString(Game1.font, sting, position, color, 0, new Vector2(0, 0), (float)scale, SpriteEffects.None, 0);
        }
    }
}
