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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteManager spriteManager;
        BloomComponent bloom;
        Texture2D backgroundTexture, startTexture, endTexture;
        enum Gamestate {Startscreen, Playing, Endscreen }
        Gamestate currentState = Gamestate.Startscreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 750;
            graphics.PreferredBackBufferWidth = 650;

            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);
            spriteManager.Enabled = false ;
            spriteManager.Visible = false ;

            bloom = new BloomComponent(this);
            Components.Add(bloom);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTexture = Content.Load<Texture2D>("background");
            startTexture = Content.Load<Texture2D>("startscreen");
            endTexture = Content.Load<Texture2D>("endscreen");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            bloom.BeginDraw();
            GraphicsDevice.Clear(Color.Teal);

            // TODO: Add your drawing code here
            //Switches between states
            switch(currentState){
                    //Start screen
                case Gamestate.Startscreen:
                    spriteBatch.Begin();
                    spriteBatch.Draw(startTexture, new Rectangle(0, 0, 650, 750), Color.White);
                    spriteBatch.End();
                    if(Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        spriteManager.Enabled = true;
                        spriteManager.Visible = true;
                        currentState=Gamestate.Playing;
                    }
                    break;
                case Gamestate.Playing:
                    //In-game state
                    spriteBatch.Begin();
                    spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 650, 750), Color.White);
                    spriteBatch.End();
                    if (spriteManager.time == 1)
                    {
                        spriteManager.Enabled = false;
                        spriteManager.Visible = false;
                        currentState = Gamestate.Endscreen;
                    }
                    break;
                case Gamestate.Endscreen:
                    //End screen, allows user to play again
                    spriteBatch.Begin();
                    spriteBatch.Draw(endTexture, new Rectangle(0, 0, 650, 750), Color.White);
                    spriteBatch.End();
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        currentState = Gamestate.Playing;
                        spriteManager.Enabled = true;
                        spriteManager.Visible = true;
                        spriteManager.time = 60;
                        spriteManager.score = 0;
                        spriteManager.score2 = 0;
                        
                    }
                    break;
            }
            base.Draw(gameTime);
        }
    }
}
