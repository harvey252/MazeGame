using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    public class GameManger
    {
        private static char state = '1';

        public static void Update(GameTime gameTime)
        {
            switch (state)
            {

                case '1':
                    //main menu

                    break;

                ///multiplayer 
                case 'H':
                    //host connection menu

                    break;
                case 'D':
                    //host dependent settings

                    break;
                case 'I':
                    //host indpendent settings

                    break;
                case 'C':
                    //connection setup

                    break;
                case 'M':
                    //mulitplayer game

                    break;
                case 'W':
                    //mulitplyer game ended

                    break;

                //single player
                case 'S':
                    //single player settings

                    break;
                case 'G':
                    //single player game

                    break;
                case 'O':
                    //single playe game over

                    break;

            }
        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            switch (state)
            {

                case '1':
                    //main menu

                    break;

                ///multiplayer 
                case 'H':
                    //host connection menu

                    break;
                case 'D':
                    //host dependent settings

                    break;
                case 'I':
                    //host indpendent settings

                    break;
                case 'C':
                    //connection setup

                    break;
                case 'M':
                    //mulitplayer game

                    break;
                case 'W':
                    //mulitplyer game ended

                    break;

                //single player
                case 'S':
                    //single player settings

                    break;
                case 'G':
                    //single player game

                    break;
                case 'O':
                    //single playe game over

                    break;

            }
        }

    }
    }
}
