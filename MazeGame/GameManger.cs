﻿using System;
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
            //single settings
            private static string singleType;
            private static int singleSize;
            private static bool singleDeafault;


            //single couters
            private static double singleTimeRemaining;
            private static double singleTimeElapased;
            private static int singleMazeCount;

            //single game items
            public static Maze singlePlayermaze;
            private static TextDisplay timeCounter = new TextDisplay(new Vector2(0, 0), Color.White, .8f);
        public static void Update(GameTime gameTime)
        {
            switch (state)
            {

                case '1':
                    //main menu
                   


                    //getting option
                    Console.WriteLine("single player game S, multi player game M or W to see scores ");
                    string tempState = (string)Console.ReadLine();
                    //to get valid type
                    while (tempState != "M" && tempState != "S" && tempState !="W")
                    {
                        Console.WriteLine("invaild resposnce S, M or W");
                        tempState = (string)Console.ReadLine();
                    }

                    if(tempState=="M")
                    {
                        state = 'H';
                    }
                    else if(tempState == "S")
                    {
                        state = 'S';
                    }
                    else
                    {
                        foreach(DataBaseManger.Score i in DataBaseManger.GetAll())
                        {
                            Console.WriteLine(i.time + " " + i.name + " " + i.mazes);
                        }
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
                        singleTimeRemaining=30;
                        singleSize = 10;
                        singleDeafault = true;
                        state = 'G';
                        break;
                    }
                    singleDeafault = false;

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


                    Console.WriteLine("initial time");
                    temp = Console.ReadLine();
                    bool timeValid = false;
                    //getting valid time
                    while (!timeValid)
                    {
                        try
                        {
                            singleTimeRemaining = Convert.ToInt32(temp);

                            if (singleTimeRemaining <= 1)
                            {
                                Console.WriteLine("too small");
                                timeValid = false;
                                temp = Console.ReadLine();
                            }
                            else
                                timeValid = true;
                        }
                        catch (Exception)
                        {
                            timeValid = false;
                            Console.WriteLine("enter valid size");
                            temp = Console.ReadLine();
                        }
                    }

                    singleMazeCount = -1;
                    state = 'G';

                    break;
                case 'G':
                    //single player game


                    //if new maze is required
                    if (singlePlayermaze == null || singlePlayermaze.player.win)
                    {
                        singleMazeCount += 1;
                        int[,] grid = new int[0,0];
                        if (singleType == "1")
                            grid = MazeGenerator.generateBinary(singleSize);        
                        else if (singleType == "2")
                            grid = MazeGenerator.generateSideWidener(singleSize);
                        else
                            grid = MazeGenerator.generateWilsons(singleSize);

                        //creating speed
                        singlePlayermaze = new Maze(new Vector2(0, 0), MazeGenerator.toVector(grid), 600);
                        //adding new time
                        singleTimeRemaining += MazeGenerator.getDijkstraTime(grid) / singlePlayermaze.player.speed;
                    }
                    //nomral operation
                    else
                    {
                        if (singleTimeRemaining >= 0)
                        {
                            singlePlayermaze.update(gameTime);
                            singleTimeRemaining -= gameTime.ElapsedGameTime.TotalSeconds;
                            singleTimeElapased += gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        //ending game
                        else
                        {
                            state = 'O';
                        }
                        
                    }

                    break;
                case 'O':
                    //single playe game over
                    Console.WriteLine("Game over");
                    Console.WriteLine("time: " + singleTimeElapased.ToString());
                    Console.WriteLine("mazes: " + singleMazeCount.ToString());

                    //recording score
                    if (singleDeafault)
                    {
                        
                        Console.WriteLine("would you like to record you score");

                        temp = (string)Console.ReadLine();
                        if (temp == "y")
                        {

                            Console.WriteLine("what is your name");
                            temp = Console.ReadLine();
                            DataBaseManger.Add(temp, singleMazeCount, singleTimeElapased);
                            
                        }

                    }

                    state = '1';
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
