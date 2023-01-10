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

            //putting in teh rest of the rows
            for (int a = 1; a <= size * 2; a += 2)
            {
                for (int b = 0; b <= size * 2; b+=2)
                {
                    grid[a, b] = 1;
                }
            }

            //removeWall(grid, 1, 1, 's');
            //grid = removeWall(grid, 0, 0, 'n');

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
                        break;
                    
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
            
    }
}
