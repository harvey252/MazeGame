﻿using System;
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

            //putting in teh rest of the rows
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
        private static Vector2[] toVector(int[,] grid)
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
                    break;

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
                    break;

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
                    break;
            }
            return false;
        }

        public static Vector2[] generateBinary(int length)
        {
            int[,] grid = makeGrid(length);

            for (int a = 1; a <= grid.GetLength(0)-1; a += 2)
            {
                for (int b = 1; b <= grid.GetLength(1)-1; b += 2)
                {
                    //if on the edge will have to only remove in one direction
                    if(b ==1)
                    {
                        removeWall(ref grid, a, b, 'e');
                    }
                    else if(a == length*2-1)
                    {
                        removeWall(ref grid, a, b, 'n');
                    }

                    int rand = random.Next(2);
                    if(rand ==1)
                    {
    
                            removeWall(ref grid, a, b, 'e');
                        

                    }
                    else 
                    {
          
                            removeWall(ref grid, a, b, 'n');
          
                    }
                }
            }

            return toVector(grid);
        }
        public static Vector2[] generateSideWidener(int length)
        {
            int[,] grid = makeGrid(length);
            
            List<Vector2> run = new List<Vector2>();
            //clearing top row
            for (int a = 1; a <= grid.GetLength(0) - 1; a += 2)
            {
                removeWall(ref grid, a, 1, 'e');
            }

                //making list of unvisted points
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

            
            return toVector(grid);
        }

        public static Vector2[] generateWilsons(int length)
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

            unvisted.Remove(new Vector2(1, 1));


            Vector2 current=new Vector2(0,0);
            while (unvisted.Count != 0)
            {
                Vector2 start = unvisted[random.Next(0, unvisted.Count)];
                path.Add(start);
                int rand = random.Next(0, 100);

                while (unvisted.Contains(path[path.Count-1]))
                {
                    
                    current = path[path.Count - 1];
                    rand = random.Next(0, 101);
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
                   
                    if (path.Contains(current))
                    {
                        if (current != path[0]&&current!= path[path.Count-1])
                        {
                            Predicate<Vector2> find = delegate (Vector2 s) { return s.Equals(current); };

                            path.RemoveRange(path.FindIndex(find), path.Count- path.FindIndex(find));
                        }

                        current = path[path.Count - 1];
                    }
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
                Console.WriteLine(path.Count);
                foreach (Vector2 i in path)
                {
                    Console.Write(i);
                }
                Console.WriteLine();
                foreach(Vector2 i in unvisted)
                {
                    Console.Write(i);
                }


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
                
                foreach (Vector2 i in path)
                {
                    
                    unvisted.Remove(i);
                }
                
                path.Clear();
            }

            return toVector(grid);
        }

        
    }


}
