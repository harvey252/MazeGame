﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeGame
{
    public class MultiGame
    {
        public char state;
        //settings
        public static int[][,] mazes;
        public static int[][,] clientMazes;
        public static Color playerColor;
        public static Color oppoentColor;
        public static string playername;
        public static string oppoentName;


        private static Host host;
        private static Client client;

        public MultiGame()
        {
            state = 'M';
        }
        public void update(GameTime gameTime)
        {
            switch (state)
            {
                case 'M':
                    //check internet connection
                    if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        Console.WriteLine("not Connected to the internet");
                        state = 'E';
                        break;
                    }

                    Console.WriteLine("colour red R, yellow Y, Blue B, green G");
                    string tempState = (string)Console.ReadLine();
                    playername = tempState;
                    //to get valid type
                    while (tempState != "R" && tempState != "Y" && tempState != "B" && tempState != "G")
                    {
                        Console.WriteLine("invaild resposnce R, Y, B or G");
                        tempState = (string)Console.ReadLine();
                    }

                    if (tempState == "R")
                        playerColor = Color.Red;
                    else if (tempState == "Y")
                        playerColor = Color.Yellow;
                    else if (tempState == "B")
                        playerColor = Color.Blue;
                    else
                        playerColor = Color.Green;

                    Console.WriteLine("name? ");
                    playername = Console.ReadLine();

                    Console.WriteLine("would you like to Host H or join J");
                    tempState = (string)Console.ReadLine();
                    //to get valid type
                    while (tempState != "H" && tempState != "J")
                    {
                        Console.WriteLine("invaild resposnce J or H");
                        tempState = (string)Console.ReadLine();
                    }


                    if (tempState == "H")
                    {
                        
                        //need to know if independent of dependent

                        Console.WriteLine("would you like to have the same settings as your oppenent (S) or different settings (D)");
                        tempState = (string)Console.ReadLine();
                        //to get valid type
                        while (tempState != "S" && tempState != "D")
                        {
                            Console.WriteLine("invaild resposnce S or D");
                            tempState = (string)Console.ReadLine();
                        }

                        if(tempState == "S")
                        {
                            mazes = getMazes();
                        }
                        else
                        {
                            Console.WriteLine("your settings");
                            mazes = getMazes();
                            Console.WriteLine("other player settings");
                            clientMazes = getMazes();
                        }

                        host = new Host(playername,playerColor.ToString(), mazes);
                        state = 'H';

                    }
                    else
                    {
                        Console.WriteLine("when ready enter host IP");
                        tempState = (string)Console.ReadLine();
                        client = new Client(tempState,playername,playerColor.ToString());
                        state = 'C';
                    }




                    break;


                case 'C':


                    break;

                case 'H':

                    break;
            }


        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            switch (state)
            {
            }
        }

        private int[][,] getMazes()
        {
            //getting type
            Console.WriteLine("maze type 1 baisc generation, 2 medium generation, or 3 complex generation (best one) ");
            string type = (string)Console.ReadLine();
            //to get valid type
            while (type != "1" && type != "2" && type != "3")
            {
                Console.WriteLine("type invalid 1, 2 or 3 ");
                type = (string)Console.ReadLine();
            }

            //geting size
            Console.WriteLine("size");
            string temp = Console.ReadLine();
            bool sizevalid = false;
            int size = 0;
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


            Console.WriteLine("number of mazes");
            temp = Console.ReadLine();
            bool numberValid = false;
            int numberMazes = 0;
            //getting valid time
            while (!numberValid)
            {
                try
                {
                    numberMazes = Convert.ToInt32(temp);

                    if (numberMazes <= 1)
                    {
                        Console.WriteLine("too small");
                        numberValid = false;
                        temp = Console.ReadLine();
                    }
                    else
                        numberValid = true;
                }
                catch (Exception)
                {
                    numberValid = false;
                    Console.WriteLine("enter valid size");
                    temp = Console.ReadLine();
                }
            }

            int[][,] mazes = new int[numberMazes][,];

            for (int n = 0; n < numberMazes; n += 1)
            {

                if (type == "1")
                    mazes[n] = MazeGenerator.generateBinary(size);
                else if (type == "2")
                    mazes[n] = MazeGenerator.generateSideWidener(size);
                else
                    mazes[n] = MazeGenerator.generateWilsons(size);
            }


            return mazes;
            
        }


    }
}