using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    public class MazeGenerator
    {
        private static Random random = new Random();
        private static int[,] makeGrid(int size)
        {
            int[,] grid = new int[size * 2 + 1, size * 2 + 1];


            //filling in teh full rows

            for (int a = 0; a <= size * 2; a += 2)
            {
                for (int b = 0; b <= size * 2; b++)
                {
                    grid[a,b] = 1;
                }
            }

            //putting in the rest of the rows
            for (int a = 1; a <= size * 2; a += 2)
            {
                for (int b = 0; b <= size * 2; b+=2)
                {
                    grid[a, b] = 1;
                }
            }

    

            return grid;
        }

        //turns 2d array into array of vectors used by the maze class
        public static Vector2[] toVector(int[,] grid)
        {
  
            List<Vector2> maze = new List<Vector2>();

            for (int i = 0; i< grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                   
                    if(grid[i,j]==1)
                        maze.Add(new Vector2(i, j));
                }
            }

            return maze.ToArray();
        }

        //turns int array to string for network transmission
        public static string toString(int[,] grid)
        {
            string maze = "";

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    maze +=grid[i, j];
                }
                maze += "\n";
            }
            return maze;
        }

        //turns string maze to int for when transmission resived
        public static int[,] fromString(string maze)
        {

            int[,] grid = makeGrid((maze.Split('\n')[0].Length-1)/2);

            int x = 0;
            foreach (string row in maze.Split('\n'))
            {
                
                int y = 0;
                foreach (char n in row.ToCharArray())
                {
                    if (n == '1')
                        grid[x, y] = 1;
                    else
                        grid[x, y] = 0;
                    y += 1;
                }
                x += 1;
            }

            return grid;
            
        }


        //remove compass direction and returns true if succssful
        //it wont be successful if there is no wall there or it is a wall
        private static bool removeWall(ref int[,]grid, int x, int y, char direction)
        {

            if (x <= 0 || y <= 0 || y >= grid.GetLongLength(1) || x >= grid.GetLongLength(0))
                return false;

            //for every direction checks if not a embty or an edge andt then removed wall
            switch(direction)
            {

                case 'n':
                        if ((y == 1)||(grid[x,y-1]==0))
                        {
                        return false;
                        }
                        else
                        {
                            grid[x, y-1] = 0;
                            return true;
                        }
                    
                case 's':
                    if ((y == grid.GetLength(0)-2)|| (grid[x, y + 1] == 0))
                    {
                        return false;
                    }
                    else
                    {
                        grid[x, y+1] = 0;
                        return true;
                    }
                    

                case 'e':
                    if ((x == grid.GetLength(0)-2) || (grid[x+1, y] == 0))
                    {
                        return false;

                    }
                    else
                    {
                        grid[x +1, y] = 0;
                        return true;
                    }
                   

                case 'w':
                    if ((x == 1)|| (grid[x - 1, y] == 0))
                    {
                        return false;
                    }
                    else
                    {
                        grid[x-1, y] = 0;
                        return true;
                    }
                    
            }
            return false;
        }

        public static int[,] generateBinary(int length)
        {
            int[,] grid = makeGrid(length);

            //going through each postion
            for (int x = 1; x <= grid.GetLength(0)-1; x += 2)
            {
                for (int y = 1; y <= grid.GetLength(1)-1; y += 2)
                {
                    //if on the edge will have to only remove in one direction
                    if(y == 1)
                    {
                        removeWall(ref grid, x, y, 'w');
                    }
                    else if(x == 1)
                    {
                        removeWall(ref grid, x, y, 'n');
                    }

                    //desiding direction
                    int rand = random.Next(2);
                    if(rand ==1)
                    {
    
                            removeWall(ref grid, x, y, 'w');
                        

                    }
                    else 
                    {
          
                            removeWall(ref grid, x, y, 'n');
          
                    }
                }
            }

            return removeSymbol(grid);
        }
        public static int[,] generateSideWidener(int length)
        {
            int[,] grid = makeGrid(length);
            
            List<Vector2> run = new List<Vector2>();
            //clearing top row
            for (int a = 1; a <= grid.GetLength(0) - 1; a += 2)
            {
                removeWall(ref grid, a, 1, 'e');
            }

            //going through all points
            for (int y = 3; y <= grid.GetLength(0) - 1; y += 2)
            {
                run.Clear();
                for (int x = 1; x <= grid.GetLength(1) - 1; x += 2)
                {
                    //chosing to end a run
                    if (random.Next(0, 50) > 25 && run.Count != 0)
                    {
                        Vector2 current = run[random.Next(0, run.Count)];
                        run.Clear();
                        removeWall(ref grid, (int)current.X, (int)current.Y, 'n');
                    }
                    //not ending a run
                    else
                    {
                        run.Add(new Vector2(x, y));
                        if (!removeWall(ref grid, x, y, 'e'))
                            removeWall(ref grid, x, y, 'n');
                    }
                }
            }

            
            return removeSymbol(grid);
        }

        public static int[,] generateWilsons(int length)
        {
            int[,] grid = makeGrid(length);

            List<Vector2> unvisted = new List<Vector2>();
            List<Vector2> path = new List<Vector2>();

            //filling visted
            for (int y = 1; y <= grid.GetLength(0) - 1; y += 2)
            {
                for (int x = 1; x <= grid.GetLength(1) - 1; x += 2)
                {
                    unvisted.Add(new Vector2(x, y));
                }
            }
            //removing conver from visted so have a starting point
            unvisted.Remove(new Vector2(1, 1));


            Vector2 current=new Vector2(0,0);

            //continous untill all oints visted
            while (unvisted.Count != 0)
            {
                //chose random start
                Vector2 start = unvisted[random.Next(0, unvisted.Count)];
                path.Add(start);
                int rand = random.Next(0, 100);

                //makes path untill visted point found
                while (unvisted.Contains(path[path.Count-1]))
                {
                    
                    current = path[path.Count - 1];
                    rand = random.Next(0, 101);
                    //picking direction
                    while (path[path.Count - 1] == current)
                    {
                        rand = random.Next(0, 101);
                        if (rand <= 25)
                        {
                            current.Y += 2;
                        }
                        else if (rand <= 50)
                        {
                            current.Y -= 2;
                        }
                        else if (rand <= 75)
                        {
                            current.X += 2;
                        }
                        else
                        {
                            current.X -= 2;
                        }
                    }

                   //validating

                    //removing a loop
                    if (path.Contains(current))
                    {
                        if (current != path[0]&&current!= path[path.Count-1])
                        {
                            Predicate<Vector2> find = delegate (Vector2 s) { return s.Equals(current); };

                            path.RemoveRange(path.FindIndex(find), path.Count- path.FindIndex(find));
                        }

                        current = path[path.Count - 1];
                    }
                    //checking it is in bounds
                    else if (current.X>=1&&current.X <= length*2&& current.Y >= 1 && current.Y <= length * 2 )
                    {
                        path.Add(current);
                    }
                    else
                    {
                        current = path[path.Count - 1];
                    }
      
                }

                //cut out path
                for (int i=0; i < path.Count-1; i +=1)
                {
                    current=path[i];
                    Vector2 next=path[i + 1];
                    if(current.X>next.X)
                    {
                        removeWall(ref grid, (int)current.X, (int)current.Y, 'w');
                    }
                    else if(current.X<next.X)
                    {
                        removeWall(ref grid, (int)current.X, (int)current.Y, 'e');
                    }
                    else if(current.Y< next.Y)
                    {
                        removeWall(ref grid, (int)current.X, (int)current.Y, 's');
                    }
                    else
                    {
                        removeWall(ref grid, (int)current.X, (int)current.Y, 'n');
                    }

                }
                
                //marking points in path as visted
                foreach (Vector2 i in path)
                {
                    
                    unvisted.Remove(i);
                }
                //resetting path for next itteration
                path.Clear();
            }
            
            return removeSymbol(grid);
        }


        public static double getDijkstraTime(int[,] grid)
        {

            Dictionary<Vector2, Vector2[]> adjecent=new Dictionary<Vector2, Vector2[]>();
            Dictionary<Vector2,int> distance= new Dictionary<Vector2, int>();
            List<Vector2> unvisted = new List<Vector2>();
     


            //creating dictionary and arries need to store distances
            for (int x = 0; x <= grid.GetLength(0) - 1; x += 1)
            {
                for (int y = 0; y <= grid.GetLength(1) - 1; y += 1)
                {
                    if(grid[x,y]==0)
                    {
                        distance.Add(new Vector2(x, y), -1);
                        unvisted.Add(new Vector2(x, y));
                        List<Vector2> adjecentL=new List<Vector2>();
                        if(grid[x,y-1]==0)
                        {
                            adjecentL.Add(new Vector2(x, y-1));
                        }

                        if (grid[x, y + 1] == 0)
                        {
                            adjecentL.Add(new Vector2(x, y+1));
                        }

                        if (grid[x+1, y] == 0)
                        {
                            adjecentL.Add(new Vector2(x+1, y));
                        }

                        if (grid[x-1, y] == 0)
                        {
                            adjecentL.Add(new Vector2(x-1, y));
                        }

                        adjecent.Add(new Vector2(x, y),adjecentL.ToArray());

                    }
                }
            }
            
            //makring start at visted
            distance[new Vector2(1, 1)] = 0;
            

            while(unvisted.Count!=0)
            {
                Vector2 current = Vector2.Zero ;
                //finding shortest unvisted
                foreach (Vector2 point in unvisted)
                {
                    
                    if(current == Vector2.Zero && distance[point] != -1)
                    {
                        current = point;
                    }
                    else if (current != Vector2.Zero && distance[current] > distance[point]&& distance[point]!=-1)
                    {
                        current = point;
                    }
                }
                //addjusting distance for each connectd point
                foreach(Vector2 point in adjecent[current])
                {
                    if(distance[point] == -1)
                    {
                        distance[point] = distance[current] + 1;
                    }
                    else if(distance[point]>distance[current])
                    {
                        //does not need to record path
                        

                        distance[point] = distance[current] + 1;

                    }

                }
                
                unvisted.Remove(current);

            }


            return distance[new Vector2(grid.GetLength(0)-2,grid.GetLength(1)-2 )];
        }

        private static int[,] removeSymbol(int[,] grid)
        {
            for (int x = 3; x <= grid.GetLength(0) - 3; x += 2)
            {
                for (int y = 3; y <= grid.GetLength(1) - 3; y += 2)
                {
                    //checks all connections open
                    if(grid[x-1,y]==0 && grid[x,y-1]==0 &&grid[y,x+1]==0 &&grid[x,y+1]==0)
                    {
                        if((grid[x+1,y+2]==0&&grid[x+2,y-1]==0&&grid[x-1,y-2]==0&&grid[x-2,y+1]==0)|| //case 1
                            (grid[x - 1, y + 2] == 0 && grid[x + 2, y + 1] == 0 && grid[x + 1, y - 2] == 0 && grid[x - 2, y - 1]==0)) //case 2
                        {
                            //removing nessary walls
                            grid[x + 1, y + 1] = 0; grid[x - 1, y + 1] = 0; grid[x + 1,y - 1]=0; grid[x - 1, y - 1]=0;
                        }
                    }
                }
            }

            return grid;
        }

        
    }

}
