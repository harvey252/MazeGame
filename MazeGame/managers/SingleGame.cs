using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame
{
    public class SingleGame
    {
        public char state;
        
        //settings
        private static string type;
        private static int size;
        private static bool deafault;


        //single couters
        private static double timeRemaining;
        private static double timeElapased;
        private static int singleMazeCount;

        //single game items
        public static Maze singlePlayermaze;
        private static TextDisplay timeCounter = new TextDisplay(new Vector2(0, 0), Color.White, .8f);
        public SingleGame()
        {
            state = 'M';
        }

        public void update(GameTime gameTime)
        {
            switch (state)
            {
                case 'M':
                    //single player settings
                    Console.WriteLine("do you want deafault settings (this will be required to upload sorce) y/n ");
                    string temp = (string)Console.ReadLine();
                    if (temp == "y")
                    {
                        //placeholder default vlaues

                        type = "3";
                        timeRemaining = 30;
                        size = 10;
                        deafault = true;
                        state = 'G';
                        break;
                    }
                    deafault = false;

                    //getting type
                    Console.WriteLine("maze type 1 baisc generation, 2 medium generation, or 3 complex generation (best one) ");
                    type = (string)Console.ReadLine();
                    //to get valid type
                    while (type != "1" && type != "2" && type != "3")
                    {
                        Console.WriteLine("type invalid 1, 2 or 3 ");
                        type = (string)Console.ReadLine();
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
                            size = Convert.ToInt32(temp);
                            if (size > 40)
                            {
                                Console.WriteLine("too large");
                                sizevalid = false;
                                temp = Console.ReadLine();
                            }
                            else if (size <= 1)
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
                            timeRemaining = Convert.ToInt32(temp);

                            if (timeRemaining <= 1)
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
                    //if new maze is required
                    if (singlePlayermaze == null || singlePlayermaze.player.win)
                    {
                        singleMazeCount += 1;
                        int[,] grid = new int[0, 0];
                        if (type == "1")
                            grid = MazeGenerator.generateBinary(size);
                        else if (type == "2")
                            grid = MazeGenerator.generateSideWidener(size);
                        else
                            grid = MazeGenerator.generateWilsons(size);

                        //creating speed
                        singlePlayermaze = new Maze(new Vector2(0, 0), MazeGenerator.toVector(grid), 600);
                        //adding new time
                        timeRemaining += MazeGenerator.getDijkstraTime(grid) / singlePlayermaze.player.speed;
                    }
                    //nomral operation
                    else
                    {
                        if (timeRemaining >= 0)
                        {
                            singlePlayermaze.update(gameTime);
                            timeRemaining -= gameTime.ElapsedGameTime.TotalSeconds;
                            timeElapased += gameTime.ElapsedGameTime.TotalSeconds;
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
                    Console.WriteLine("time: " + timeElapased.ToString());
                    Console.WriteLine("mazes: " + singleMazeCount.ToString());

                    //recording score
                    if (deafault)
                    {

                        Console.WriteLine("would you like to record you score");

                        temp = (string)Console.ReadLine();
                        if (temp == "y")
                        {

                            Console.WriteLine("what is your name");
                            temp = Console.ReadLine();
                            DataBaseManager.Add(temp, singleMazeCount, timeElapased);

                        }

                    }

                    state = 'E';
                    break;
         ;
                   
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            switch (state)
            {
                case 'G':
                    //single player game

                    if (singlePlayermaze != null)
                        singlePlayermaze.draw(_spriteBatch);

                    timeCounter.Draw(_spriteBatch, Math.Round(timeRemaining).ToString());

                    break;
            }
        }
    }
}
