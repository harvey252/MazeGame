using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeGame
{
    public static class GameManager
    {
        private static char state = '1';

        private static SingleGame singleGame;
        public static MultiGame multiGame;
        


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
                        while (tempState != "M" && tempState != "S" && tempState != "W")
                        {
                            Console.WriteLine("invaild resposnce S, M or W");
                            tempState = (string)Console.ReadLine();
                        }

                        if (tempState == "M")
                        {
                            state = 'M';
                        multiGame = new MultiGame();
                        }
                        else if (tempState == "S")
                        {
                            state = 'S';
                        singleGame = new SingleGame();
                        }
                        else
                        {
                            foreach (DataBaseManager.Score i in DataBaseManager.GetAll())
                            {
                                Console.WriteLine(i.time + " " + i.name + " " + i.mazes);
                            }
                        }


                        break;

                case 'S':

                    singleGame.update(gameTime);
                    if(singleGame.state == 'E')
                    {
                        singleGame = null;
                        state = '1';
                    }

                    break;

                case 'M':

                    multiGame.update(gameTime);
                    if (multiGame.state == 'E')
                    {
                        multiGame = null;
                        state = '1';
                    }

                    break;


            }
        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            switch(state)
            {
                case 'S':
                    singleGame.Draw(_spriteBatch);
                    break;
                case 'M':
                    multiGame.Draw(_spriteBatch);
                    break;
            }
            

        }

        public static string getName()
        {
            string name=null;
            bool valid = false;
            while(!valid)
            {
                Console.WriteLine("name? ");
                name = Console.ReadLine();
                if (name == null)
                {
                    Console.WriteLine("please enter a name");
                }
                else if(name.Length>10)
                {
                    Console.WriteLine("please enter a shorter name");
                }
            }

            return name;
        }

    }
    
}
