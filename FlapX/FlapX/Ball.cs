using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FlapX
{
    class Ball: Sprite
    {
        public Vector2 Velocity;
        public Vector2 Position;

        //Constructor for a Ball object. Inherits the properties from the Sprite class
        public Ball(Texture2D spriteTexture, Vector2 position, Point frameSize, 
            Point currentFrame, Point sheetSize, Vector2 speed, int timePerFrame,Vector2 velocity)
            : base(spriteTexture, position,
                frameSize, currentFrame, sheetSize, speed, timePerFrame)
        {
            this.Velocity = velocity;
            this.Position = position;
        }

        //Update method which allows the user to control the ball
        //Adds gravity to the ball
        public override void Update(GameTime gameTime, Rectangle clientRect)
        {
            position.Y += Velocity.Y;
            //Adds gravity
            Velocity.Y += 0.179f;
            position.X += Velocity.X;

            //Takes keyboard input
           /* KeyboardState newState = Keyboard.GetState();
            if (oldState.IsKeyUp(Keys.Space) && newState.IsKeyDown(Keys.Space))
            {
                if (Velocity.Y >= 0)
                {
                    Velocity.Y *= -1;
                }
                Velocity.Y = -6;
            }*/

            //If the ball hits the walls on the top or the side, the respective velocity is reversed,
            //making the ball "bounce" off
            if (position.Y <= 0) Velocity.Y *= -1f;
            if (position.Y >= clientRect.Height - frameSize.Y) Velocity.Y *= -.90f;
            if (position.X <= 0 || position.X >= clientRect.Width-frameSize.X) Velocity.X *= -1;

            //oldState = newState;
            
            
           base.Update(gameTime, clientRect);
        }

        //Draw method, inherited from Sprite class
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }


    }
}
