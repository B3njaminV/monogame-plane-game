using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlaneGAME.Core.Game;
using System;

namespace PlaneGAME
{
    public class PlaneGAMEGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D avionTexture;
        Texture2D torpilleTexture;
        Texture2D background;
        Avion avion;
        Rectangle avionHitbox;
        Vector2 avionPosition;
        float speed;
        Torpille torpille;
        Vector2 torpillePosition = Vector2.Zero;
        Rectangle torpilleHitbox;
        Torpille torpille2;
        Vector2 torpillePosition2 = Vector2.Zero;
        Rectangle torpilleHitbox2;
        Torpille torpille3;
        Vector2 torpillePosition3 = Vector2.Zero;
        Rectangle torpilleHitbox3;
        bool pause = false;
        bool dead = false;
        int score = 0;
        int highScore = 0;
        SpriteFont scoreFont;

        public PlaneGAMEGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            //_graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            avion = new Avion();
            avionPosition = new Vector2(avion.avionPosition.X,avion.avionPosition.Y);
            speed = 1000f;
            torpille = new Torpille(2000);
            torpillePosition = new Vector2(torpille.torpillePosition.X, torpille.torpillePosition.Y);
            torpille2 = new Torpille(2500);
            torpillePosition2 = new Vector2(torpille2.torpillePosition.X, torpille2.torpillePosition.Y);
            torpille3 = new Torpille(3000);
            torpillePosition3 = new Vector2(torpille3.torpillePosition.X, torpille3.torpillePosition.Y);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            torpilleTexture = Content.Load<Texture2D>("torpedo_black_revert");
            background = Content.Load<Texture2D>("background");
            avionTexture = Content.Load<Texture2D>("plane");
            scoreFont = Content.Load<SpriteFont>("score");
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

            if (dead)
            {
                if (kstate.IsKeyDown(Keys.R))
                {
                    score = 0;
                    Initialize();
                    ResetElapsedTime();
                    dead = false;
                    pause = false;
                }
            }

            if (!pause)
            {
                score += 1;

                if (avionHitbox.Intersects(torpilleHitbox) || avionHitbox.Intersects(torpilleHitbox2) || avionHitbox.Intersects(torpilleHitbox3))
                {
                    dead = true;
                    pause = true;
                    if (score > highScore)
                    {
                        highScore = score;
                    }
                }

                if (kstate.IsKeyDown(Keys.Up))
                    avionPosition.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (kstate.IsKeyDown(Keys.Down))
                    avionPosition.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (kstate.IsKeyDown(Keys.Left))
                {
                    avionPosition.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    avionTexture = Content.Load<Texture2D>("plane_revert");
                }

                if (kstate.IsKeyDown(Keys.Right))
                {
                    avionPosition.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    avionTexture = Content.Load<Texture2D>("plane");
                }

                if (avionPosition.X > _graphics.PreferredBackBufferWidth - avionTexture.Width / 5)
                    avionPosition.X = _graphics.PreferredBackBufferWidth - avionTexture.Width / 5;
                else if (avionPosition.X < avionTexture.Width / 5)
                    avionPosition.X = avionTexture.Width / 5;

                if (avionPosition.Y > _graphics.PreferredBackBufferHeight - avionTexture.Height / 5)
                    avionPosition.Y = _graphics.PreferredBackBufferHeight - avionTexture.Height / 5;
                else if (avionPosition.Y < avionTexture.Height / 5)
                    avionPosition.Y = avionTexture.Height / 5;

                torpillePosition = torpille.Update(gameTime);
                torpillePosition2 = torpille2.Update(gameTime);
                torpillePosition3 = torpille3.Update(gameTime);

                avionHitbox = new Rectangle((int)avionPosition.X - avionTexture.Width/4, (int)avionPosition.Y - avionTexture.Height/4, avionTexture.Width/3 ,avionTexture.Height/3);
                torpilleHitbox = new Rectangle((int)torpillePosition.X - torpilleTexture.Width/4, (int)torpillePosition.Y - torpilleTexture.Height/4, torpilleTexture.Width/3, torpilleTexture.Height/3);
                torpilleHitbox2 = new Rectangle((int)torpillePosition2.X - torpilleTexture.Width / 4, (int)torpillePosition2.Y - torpilleTexture.Height / 4, torpilleTexture.Width / 3, torpilleTexture.Height / 3);
                torpilleHitbox3 = new Rectangle((int)torpillePosition3.X - torpilleTexture.Width / 4, (int)torpillePosition3.Y - torpilleTexture.Height / 4, torpilleTexture.Width / 3, torpilleTexture.Height / 3);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            if (!dead)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
                _spriteBatch.Draw(
                    avionTexture,
                    avionPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(avionTexture.Width / 2, avionTexture.Height / 2),
                    new Vector2((float)0.5, (float)0.5),
                    SpriteEffects.None,
                    0f
                );
                _spriteBatch.Draw(
                    torpilleTexture,
                    torpillePosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(torpilleTexture.Width / 2, torpilleTexture.Height / 2),
                    new Vector2((float)0.5, (float)0.5),
                    SpriteEffects.None,
                    0f
                );
                _spriteBatch.Draw(
                    torpilleTexture,
                    torpillePosition2,
                    null,
                    Color.White,
                    0f,
                    new Vector2(torpilleTexture.Width / 2, torpilleTexture.Height / 2),
                    new Vector2((float)0.5, (float)0.5),
                    SpriteEffects.None,
                    0f
                );
                _spriteBatch.Draw(
                    torpilleTexture,
                    torpillePosition3,
                    null,
                    Color.White,
                    0f,
                    new Vector2(torpilleTexture.Width / 2, torpilleTexture.Height / 2),
                    new Vector2((float)0.5, (float)0.5),
                    SpriteEffects.None,
                    0f
                );
                _spriteBatch.DrawString(scoreFont, "Score : " + score.ToString(), new Vector2(20, 05), Color.Red);
                _spriteBatch.DrawString(scoreFont, "HighScore : " + highScore.ToString(), new Vector2(20, 85), Color.Red);
                _spriteBatch.End();
            }
            else if (dead)
            {
                _spriteBatch.Begin();

                _spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.DarkGray);
                _spriteBatch.DrawString(scoreFont, "HighScore : " + highScore.ToString(), new Vector2(860, 580), Color.Red);
                _spriteBatch.DrawString(scoreFont, "Score : " + score.ToString(), new Vector2(900,500), Color.Red);
                _spriteBatch.DrawString(scoreFont, "Perdu !", new Vector2(925, 430), Color.Red);

                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
