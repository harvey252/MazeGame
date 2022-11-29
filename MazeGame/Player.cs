﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MazeGame
{
    public class Player
    {
        public Vector2 position;
        private float speed = 4f;
        private Color color = Color.Blue;
        private Texture2D texture = Game1.blockTexture;
        private Vector2 endPos;
        private Vector2 target;

        public Player(Vector2 startPos)
        {
            position = startPos;
            target = position;
        }

        public void update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();


            //defines new target from direction input
            if (atTarget())
            {
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
            if (!atTarget() && targetValid())
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
        public bool targetValid()
        {

            //check target is not occupied
            foreach (Vector2 block in Game1.testMaze.blocks)
            {
                if (target == block) 
                {
                    target = position;
                    return false;
                }
            }

            return true;
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            Game1.testMaze.drawPoint(_spriteBatch, position, Game1.blockTexture, Color.Blue);
        }
    }
}
