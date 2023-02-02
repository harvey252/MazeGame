using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Data.SqlTypes;
using System.Data;

namespace MazeGame
{
    public static class GameManger
    {
        private static char state = '1';


        //single player
        private static string singleType;
        private static int singleSize;
        private static double singleTimeRemaining;
        private static double singleTimeElapased;
        public static Maze singlePlayermaze;
        private static TextDisplay timeCounter = new TextDisplay(new Vector2(0, 0), Color.White, .8f);
        public static void Update(GameTime gameTime)
        {
            switch (state)
            {

                case '1':
                    //main menu

                    DataBaseManger.add("test", 6, 1);

                    //getting option
                    Console.WriteLine("single player game S or multipl player game M ");
                    string tempState = (string)Console.ReadLine();
                    //to get valid type
                    while (tempState != "M" && tempState != "S")
                    {
                        Console.WriteLine("invaild resposnce S or M");
                        tempState = (string)Console.ReadLine();
                    }

                    if(tempState=="M")
                    {
                        state = 'H';
                    }
                    else
                    {
                        state = 'S';
                    }

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
                    Console.WriteLine("do you want deafault settings (this will be required to upload sorce) y/n ");
                    string temp = (string)Console.ReadLine();
                    if(temp=="y")
                    {
                        //placeholder default vlaues

                        singleType="3";
                        singleTimeRemaining=100;
                        singleSize = 10;
                        state = 'G';
                        break;
                    }


                    //getting type
                    Console.WriteLine("maze type 1 baisc generation, 2 medium generation, or 3 complex generation (best one) ");
                    singleType = (string)Console.ReadLine();
                    //to get valid type
                    while (singleType != "1" && singleType != "2" && singleType != "3")
                    {
                        Console.WriteLine("type invalid 1, 2 or 3 ");
                        singleType = (string)Console.ReadLine();
                    }

                    //geting size
                    Console.WriteLine("size");
                    temp = Console.ReadLine();
                    bool sizevalid = false;
                    //getting valid size
                    while (!sizevalid)
                    {
                        try
                        {
                            singleSize = Convert.ToInt32(temp);
                            if (singleSize > 40)
                            {
                                Console.WriteLine("too large");
                                sizevalid = false;
                                temp = Console.ReadLine();
                            }
                            else if (singleSize <= 1)
                            {
                                Console.WriteLine("too small");
                                sizevalid = false;
                                temp = Console.ReadLine();
                            }
                            else
                                sizevalid = true;
                        }
                        catch (Exception)
                        {
                            sizevalid = false;
                            Console.WriteLine("enter valid size");
                            temp = Console.ReadLine();
                        }
                    }


                    state = 'G';

                    break;
                case 'G':
                    //single player game

                    if (singlePlayermaze == null || singlePlayermaze.player.win)
                    {
                        if (singleType == "1")
                            singlePlayermaze = new Maze(new Vector2(0, 0), MazeGenerator.generateBinary(singleSize), 600);
                        else if (singleType == "2")
                            singlePlayermaze = new Maze(new Vector2(0, 0), MazeGenerator.generateSideWidener(singleSize), 600);
                        else
                            singlePlayermaze = new Maze(new Vector2(0, 0), MazeGenerator.generateWilsons(singleSize), 600);
                    }
                    else
                    {
                        singlePlayermaze.update(gameTime);
                        singleTimeRemaining -= gameTime.ElapsedGameTime.TotalSeconds;
                        singleTimeElapased += gameTime.ElapsedGameTime.TotalSeconds;
                        
                    }

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

                    if (singlePlayermaze != null)
                        singlePlayermaze.draw(_spriteBatch);

                    timeCounter.Draw(_spriteBatch, Math.Round(singleTimeRemaining).ToString());

                    break;
                case 'O':
                    //single playe game over

                    break;

            }
        }

    }
    
}
