using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlaneGAME
{
    public class PlaneGAMEGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D ballTexture;
        Texture2D background;
        Vector2 ballPosition;
        float ballSpeed;
        bool pause = false;

        public PlaneGAMEGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
            _graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 500f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("background");
            ballTexture = Content.Load<Texture2D>("plane_red");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.P))
            {
                pause = !pause;
            }

            if (!pause)
            {
                if (kstate.IsKeyDown(Keys.Up))
                    ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (kstate.IsKeyDown(Keys.Down))
                    ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (kstate.IsKeyDown(Keys.Left))
                {
                    ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    ballTexture = Content.Load<Texture2D>("plane_red_invert");
                }

                if (kstate.IsKeyDown(Keys.Right))
                {
                    ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    ballTexture = Content.Load<Texture2D>("plane_red");
                }

                if (ballPosition.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 10)
                    ballPosition.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 10;
                else if (ballPosition.X < ballTexture.Width / 10)
                    ballPosition.X = ballTexture.Width / 10;

                if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 10)
                    ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 10;
                else if (ballPosition.Y < ballTexture.Height / 10)
                    ballPosition.Y = ballTexture.Height / 10;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
            _spriteBatch.Draw(
                ballTexture,
                ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                new Vector2((float)0.2, (float)0.2),
                SpriteEffects.None,
                0f
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
