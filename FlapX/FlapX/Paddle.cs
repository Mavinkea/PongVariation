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
    class Paddle: Sprite
    {
        Random r = new Random();

        //Constructor for a paddle object. Inherits the properties from the Sprite class
        public Paddle(Texture2D spriteTexture, Vector2 position, Point frameSize, 
            Point currentFrame, Point sheetSize, Vector2 speed, int timePerFrame)
            : base(spriteTexture, position,
                frameSize, currentFrame, sheetSize, speed, timePerFrame)
        {
        }

        //Update method which moves the paddle up and down without user input
        //and change direction each time it hits the top and bottom of the screen
        public override void Update(GameTime gameTime, Rectangle clientRect)
        {
           this.position.Y += speed.Y;
           if (this.position.Y < 0)
           {
               speed.Y = r.Next(-12, -8);
               speed.Y *= -1;
           }
           if (this.position.Y > clientRect.Height - frameSize.Y)
           {
               speed.Y = r.Next(8, 12);
               speed.Y *= -1;
           }

            
           base.Update(gameTime, clientRect);
        }

        //Draw method, inherited from Sprite class
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
