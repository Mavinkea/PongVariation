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
    abstract class Sprite
    {
        //Instance variables
        Texture2D spriteTexture;
        protected Vector2 position;
        protected Vector2 speed;
        protected Point frameSize;
        Point currentFrame;
        Point sheetSize;
        int timeSinceLastFrame = 0;
        const int defaultTimeSinceLastFrame = 16;
        int timePerFrame;
        public SpriteEffects flip = SpriteEffects.None;

        public Sprite(Texture2D spriteTexture, Vector2 position, Point frameSize, 
            Point currentFrame, Point sheetSize, Vector2 speed, int timePerFrame)
        {
            this.spriteTexture = spriteTexture;
            this.position = position;
            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            this.currentFrame = currentFrame;
            this.speed = speed;
            this.timePerFrame = timePerFrame;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientRect)
        {
            //Code that allows for animation of the sprite
            //Works for all animated sprites
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > timePerFrame)
            {
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    if (currentFrame.Y >= sheetSize.Y) currentFrame.Y = 0;
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            spritebatch.Draw(spriteTexture, position, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X,
                frameSize.Y), Color.White, 0, Vector2.Zero, 1f, flip, 0);
        }

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y,
                    frameSize.X, frameSize.Y);
            }
        }

    }
}
