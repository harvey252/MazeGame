using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;



namespace MazeGame
{
    public class MultiGame
    {
        public char state;
        //settings
        public int[][,] hostMazes;
        public int[][,] clientMazes;
        public string playerColorString;

        public string playername;
        public string oppoentName;

        public displayMaze displayMaze;

        private Host host;
        private Client client;



        //game varables 
        private Maze maze;
        private int counter=0;
        public Color opponentColor;
        public Color playerColor;
        private ScoreDisplay playerScore;
        public ScoreDisplay oppenentScore;


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
                        playerColorString = "Red";
                    else if (tempState == "Y")
                        playerColorString = "Yellow";
                    else if (tempState == "B")
                        playerColorString = "Blue";
                    else
                        playerColorString = "Green";

                    playername = GameManager.getName();

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
                            hostMazes = getMazes();
                            clientMazes = hostMazes;
                        }
                        else
                        {
                            Console.WriteLine("your settings");
                            hostMazes = getMazes();
                            Console.WriteLine("other player settings");
                            clientMazes = getMazes();
                        }

                        host = new Host(playername, playerColorString, hostMazes, clientMazes);
                        state = 'H';

                    }
                    else
                    {
                        Console.WriteLine("when ready enter host IP");
                        tempState = (string)Console.ReadLine();
                        client = new Client(tempState,playername, playerColorString);
                        state = 'C';
                    }




                    break;


                case 'C':
                    if(!client.waiting&&client.game)
                    {
                        //game start
                        if(maze == null)
                        {
                            playerColor = stringToColor(playerColorString);
                            opponentColor = stringToColor(client.hostColor);
                            

                            clientMazes = client.clientMazes;
                            hostMazes = client.hostMazes;

                            playerScore = new ScoreDisplay(new Vector2(600, 200), playerColor, 200, clientMazes.Length);
                            oppenentScore = new ScoreDisplay(new Vector2(600, 400), opponentColor,200, hostMazes.Length);

                            maze = new Maze(new Vector2(0, 0), MazeGenerator.toVector(client.clientMazes[0]), 600, playerColor);

                            displayMaze = new displayMaze(new Vector2(600, 0), MazeGenerator.toVector(hostMazes[0]), 200, opponentColor);

                        }
                        else if(maze.player.win)
                        {
                            playerScore.updateScore();
                            counter += 1;
                            if (counter < clientMazes.Length)
                            {
                                maze = new Maze(new Vector2(0, 0), MazeGenerator.toVector(client.clientMazes[counter]), 600, playerColor);
                                client.nextMaze(counter);
                            }
                            else
                            {
                                client.sendWin();
                                client.game = false;
                                Console.WriteLine("you win");
                            }
                        }
                        else
                        {
                            maze.update(gameTime);
                            client.sendpostion(maze.player.position.X, maze.player.position.Y);
                            
                            if(displayMaze.blocks != MazeGenerator.toVector(hostMazes[client.hostMazeIndex]))
                            {
            
                                displayMaze = new displayMaze(new Vector2(600, 0), MazeGenerator.toVector(hostMazes[client.hostMazeIndex]), 200, opponentColor);
                            }

                            displayMaze.playerPos = client.hostPos;
                        }
                    }

                    break;

                case 'H':
               
                    if (!host.waiting&&host.game)
                    {
                        //game start
                        if (maze == null)
                        {
                            playerColor = stringToColor(playerColorString);
                            opponentColor = stringToColor(host.clientColor);
                            playerScore = new ScoreDisplay(new Vector2(600, 200), playerColor, 200, hostMazes.Length);
                            oppenentScore = new ScoreDisplay(new Vector2(600, 400), opponentColor, 200, clientMazes.Length);

                            maze = new Maze(new Vector2(0, 0), MazeGenerator.toVector(hostMazes[counter]), 600, playerColor);
                            displayMaze = new displayMaze(new Vector2(600, 0), MazeGenerator.toVector(clientMazes[0]), 200, opponentColor);

                        }
                        else if (maze.player.win)
                        {
                            playerScore.updateScore();
                            counter += 1;
                            if (counter < hostMazes.Length)
                            {
                                maze = new Maze(new Vector2(0, 0), MazeGenerator.toVector(hostMazes[counter]), 600,playerColor);
                                host.nextMaze(counter);
                            }
                            else
                            {
                                host.sendWin();
                                host.game = false;
                                Console.WriteLine("you win");
                            }
                        }
                        else
                        {
                            maze.update(gameTime);
                            host.sendpostion(maze.player.position.X, maze.player.position.Y);
                            
                            if (displayMaze.blocks != MazeGenerator.toVector(clientMazes[host.clientIndex]))
                            {
                              
                                displayMaze = new displayMaze(new Vector2(600, 0), MazeGenerator.toVector(clientMazes[host.clientIndex]), 200, opponentColor);
                            }

                            displayMaze.playerPos = host.clientPos;
                        }
                    }

                    break;
            }


        }
        //drawing all objects
        public void Draw(SpriteBatch _spriteBatch)
        {
            switch (state)
            {
                case 'C':
                    if (maze != null)
                        maze.draw(_spriteBatch);
                    if (displayMaze != null)
                        displayMaze.draw(_spriteBatch);

                    if (playerScore != null)
                        playerScore.draw(_spriteBatch);
                    if (oppenentScore != null)
                        oppenentScore.draw(_spriteBatch);

                    break;
                case 'H':
                    if (maze != null)
                        maze.draw(_spriteBatch);
                    if (displayMaze != null)
                        displayMaze.draw(_spriteBatch);

                    if (playerScore != null)
                        playerScore.draw(_spriteBatch);
                    if (oppenentScore != null)
                        oppenentScore.draw(_spriteBatch);

                    break;
            }
        }

        private Color stringToColor(string color)
        {
            if(color == "Blue")
            {
                return Color.Blue;
            }
            else if(color == "Yellow")
            {
                return Color.Yellow;
            }
            else if(color == "Red")
            {
                return Color.Red;
            }
            else if(color == "Green")
            {
                return Color.LightGreen;
            }
            else
            {
                //if error has occured
                return Color.White;
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

                    if (numberMazes < 1)
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
