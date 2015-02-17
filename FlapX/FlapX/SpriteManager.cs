using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace FlapX
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spritebatch;
        Texture2D ballTexture, paddleTexture, ball2Texture, txtTexture;
        Paddle leftPaddle, rightPaddle;
        Ball ball, ball2;
        public int score { get; set; }
        public int score2 { get; set; }
        public int time { get; set; }
        SpriteFont font;
        KeyboardState oldState;

        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            spritebatch = new SpriteBatch(Game.GraphicsDevice);
            ballTexture = Game.Content.Load<Texture2D>("basketball");
            ball2Texture = Game.Content.Load<Texture2D>("basketball2");
            paddleTexture = Game.Content.Load<Texture2D>("paddleanimated");
            font = Game.Content.Load<SpriteFont>("SpriteFont1");


            leftPaddle = new Paddle(paddleTexture, new Vector2(0, 200), new Point(12, 110), new Point(0, 0), new Point(4, 0),
                new Vector2(2, -6), 2500);
            rightPaddle = new Paddle(paddleTexture, new Vector2(638, 200), new Point(12, 110), new Point(0, 0), new Point(4, 0),
                new Vector2(2, 9), 2500);
            rightPaddle.flip = SpriteEffects.FlipHorizontally;
            ball = new Ball(ballTexture, new Vector2(Game.GraphicsDevice.PresentationParameters.BackBufferWidth/3, Game.GraphicsDevice.PresentationParameters.BackBufferHeight/2), new Point(58, 57), new Point(1, 0), new Point(1, 0),
                new Vector2(5,-10), 100,new Vector2(3,-6));
            ball2 = new Ball(ball2Texture, new Vector2(Game.GraphicsDevice.PresentationParameters.BackBufferWidth / 3, Game.GraphicsDevice.PresentationParameters.BackBufferHeight / 2), new Point(58, 57), new Point(1, 0), new Point(1, 0),
                new Vector2(5, -10), 100, new Vector2(-3, -6));

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            // TODO: Add your update code here
            leftPaddle.Update(gameTime, new Rectangle(0, 0, 650, 750));
            rightPaddle.Update(gameTime, new Rectangle(0, 0, 650, 750));

            ball.Update(gameTime, new Rectangle(0, 0, 650, 750));
            ball2.Update(gameTime, new Rectangle(0, 0, 650, 750));

            //Updates score and reverses the ball's direction if it collides with the paddle
            if (ball.CollisionRectangle.Intersects(leftPaddle.CollisionRectangle) && ball.Position.X>=12)
            {
                ball.Velocity.X *= -1;
                score += 100;
            }

            //Updates the score and reverses the ball's position if it collides with the paddle
            if (ball.CollisionRectangle.Intersects(rightPaddle.CollisionRectangle) && ball.Position.X<=638)
            {
                ball.Velocity.X *= -1;
                score += 100;
            }

            //Takes keyboard input
            KeyboardState newState = Keyboard.GetState();
            if (oldState.IsKeyUp(Keys.Space) && newState.IsKeyDown(Keys.Space))
            {
                if (ball.Velocity.Y >= 0)
                {
                    ball.Velocity.Y *= -1;
                }
                ball.Velocity.Y = -6;
            }

            if (ball2.CollisionRectangle.Intersects(leftPaddle.CollisionRectangle) && ball2.Position.X >= 12)
            {
                ball2.Velocity.X *= -1;
                score2 += 100;
            }

            //Updates the score and reverses the ball's position if it collides with the paddle
            if (ball2.CollisionRectangle.Intersects(rightPaddle.CollisionRectangle) && ball2.Position.X <= 638)
            {
                ball2.Velocity.X *= -1;
                score2 += 100;
            }

            if (oldState.IsKeyUp(Keys.Up) && newState.IsKeyDown(Keys.Up))
            {
                if (ball2.Velocity.Y >= 0)
                {
                    ball2.Velocity.Y *= -1;
                }
                ball2.Velocity.Y = -6;
            }
            oldState = newState;

            time = 60 - gameTime.TotalGameTime.Seconds;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spritebatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            leftPaddle.Draw(gameTime, spritebatch);
            rightPaddle.Draw(gameTime, spritebatch);
            ball.Draw(gameTime, spritebatch);
            ball2.Draw(gameTime, spritebatch);

            spritebatch.DrawString(font, "Player 1 Score " + score, new Vector2(0,
                0), Color.White);
            spritebatch.DrawString(font, "Player 2 Score " + score2, new Vector2(0,
                700), Color.White);
            spritebatch.DrawString(font, "Time " +time, new Vector2(Game.GraphicsDevice.PresentationParameters.BackBufferWidth / 3 + 50,
                50), Color.White);

            spritebatch.End();

            base.Draw(gameTime);
        }
    }
}
