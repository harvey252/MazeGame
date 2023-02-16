using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MazeGame
{
    public class Player
    {
        private Vector2 start;
        private Vector2 end;

        public Vector2 position;
        public float speed = 4f;

        private Color color = Color.Blue;
        private Texture2D texture = Game1.blockTexture;
        private Vector2 endPos;
        private Vector2 target;
       
        public bool error = false;
        public bool win = false;

        //reference to maze
        


        private List<Vector2> trail = new List<Vector2>();

        public Player(Vector2 startPos, Vector2 finishPos)
        {
            position = startPos;
            target = position;
            start = position;

            end = finishPos;

        }

        public void update(GameTime gameTime, ref Maze maze)
        {
            error = !pointValid(position,ref maze);
            //temporay code stops player updating if error
            if (error)
                return;

            //temporay code stops player updating if won
            if (win)
                return;


            if (position == end)
                win = true;



            var kstate = Keyboard.GetState();

            
            //resetting the player
            if(kstate.IsKeyDown(Keys.R))
            {
                position = start;
                target = start;
                trail = new List<Vector2>(); 
            }

            //defines new target from direction input
            if (atTarget())
            {
                if (!trail.Contains(target))
                    trail.Add(target);

                if (kstate.IsKeyDown(Keys.W)|| kstate.IsKeyDown(Keys.Up))
                {
                    target.Y += -1;

                }
                else if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down))
                {
                    target.Y += 1;

                }
                else if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left))
                {
                    target.X += -1;

                }
                else if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
                {
                    target.X += 1;

                }


            }
            //moves to target
            if (!atTarget() && pointValid(target,ref maze))
            {
                //if target to right
                if (target.X > position.X)
                {
                    position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
             
                    if (target.X < position.X)
                        position.X = target.X;
                }
                //if target to left
                else if (target.X < position.X)
                {
                    position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (target.X > position.X)
                        position.X = target.X;
                }
                //if target bellow (greater Y cord means lower on screen)
                else if (target.Y > position.Y)
                {
                    position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
   
                    if (target.Y < position.Y)
                        position.Y = target.Y;
                }
                //if target above
                else if (target.Y < position.Y)
                {
                    position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    
                    if (target.Y > position.Y)
                        position.Y = target.Y;
                }
            }
            else if (!pointValid(target,ref maze))
            {
                target = position;
            }

            //check if at end of level
            if (position == endPos)
            {
                //not used yet
            }

        }
        //check to see if at target
        public bool atTarget()
        {
            if (position == target)
            {
                return true;
            }
            else
                return false;
        }

        //makes sure target is allowed
        public bool pointValid(Vector2 point, ref Maze maze)
        {

            //check target is not occupied
            foreach (Vector2 block in maze.blocks)
            {
                if (point == block) 
                {
                    return false;
                }
            }
            //checks point is not outside the maze
            if (position.X > maze.Greastest|| position.Y > maze.Greastest)
            {
                return false;
            }


            return true;
        }

        public void draw(SpriteBatch _spriteBatch, ref Maze maze)
        {
            //draw trail
            foreach(Vector2 point in trail)
                maze.drawPoint(_spriteBatch, point, Game1.blockTexture, Color.Orange);

            //draw end point
            maze.drawPoint(_spriteBatch, end, Game1.blockTexture, Color.Yellow);
            //draw player
            maze.drawPoint(_spriteBatch, position, Game1.blockTexture, Color.Red);
            

        }
    }
}
