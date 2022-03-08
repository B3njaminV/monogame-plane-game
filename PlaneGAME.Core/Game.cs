using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlaneGAME.Core.Game;
using MyoLib;
using MyoSharp;
using MyoSharp.Poses;
using static System.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneGAME
{
    public class PlaneGAMEGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D background;
        Difficulte difficulte = Difficulte.Facile;
        bool pause = false;
        bool dead = false;
        Score score = new Score();
        Score highScore = new Score();
        Persistence persistence = new Persistence();
        public String chemin = "Game/score.xml";
        SpriteFont textureText;

        public PlaneGAMEGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("background"); 
            textureText = Content.Load<SpriteFont>("score");
            chargeHighScore();
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
                if (kstate.IsKeyDown(Keys.A))
                {
                    difficulte = Difficulte.Facile;
                }
                if (kstate.IsKeyDown(Keys.Z))
                {
                    difficulte = Difficulte.Moyen;
                }
                if (kstate.IsKeyDown(Keys.E))
                {
                    difficulte = Difficulte.Difficile;
                }
                if (kstate.IsKeyDown(Keys.R))
                {
                    score.reinitScore();
                    dead = false;
                    pause = false;
                    Initialize();
                    ResetElapsedTime();
                }
            }

            if (pause)
            {
                if(score.getPoint() > highScore.getPoint())
                {
                    persistence.Save(chemin, score);
                }
                chargeHighScore();
            }
            else
            {
                score.addPoint();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            if (!dead)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
                _spriteBatch.DrawString(textureText, "Score : " + score.getPoint(), new Vector2(20, 05), Color.Red);
                _spriteBatch.DrawString(textureText, "HighScore : " + highScore.getPoint(), new Vector2(20, 85), Color.Red);
                _spriteBatch.DrawString(textureText, "Difficulte : " + difficulte.ToString(), new Vector2(420, 05), Color.Red);
            }
            else if (dead)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.DarkGray);
                _spriteBatch.DrawString(textureText, "HighScore : " + highScore.ToString(), new Vector2(660, 480), Color.Red);
                _spriteBatch.DrawString(textureText, "Score : " + score.ToString(), new Vector2(700, 400), Color.Red);
                _spriteBatch.DrawString(textureText, "Perdu !", new Vector2(725, 330), Color.Red);
                _spriteBatch.DrawString(textureText, "A: Facile   Z: Moyen   E: Difficile", new Vector2(500, 05), Color.Red);
                _spriteBatch.DrawString(textureText, "Difficulte : " + difficulte.ToString(), new Vector2(630, 85), Color.Red);
                _spriteBatch.DrawString(textureText, "R: Restart", new Vector2(700, 550), Color.Red);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void chargeHighScore()
        {
            try
            {
                highScore = persistence.Load(chemin);
            }
            catch(Exception e)
            {
                Console.WriteLine("ERREUR PERSISTENCE : " + e);
            }
        }
    }
}
