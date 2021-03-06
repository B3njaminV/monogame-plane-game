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
        public bool pause = false;
        Difficulte difficulte = Difficulte.Facile;
        Score score = new Score();
        Score highScore = new Score();
        Persistence persistence = new Persistence();
        public String chemin = "Game/score.xml";
        SpriteFont textureText;
        List<GameObject> listeGameObject = new List<GameObject>();
        List<Avion> listeAvion = new List<Avion>();
        private Collisionneur collisionneur;
        TorpilleManager tm;

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
            listeGameObject.Clear();
            listeAvion.Clear();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Avion avion = new Avion(_spriteBatch, this);
            tm = new TorpilleManager(difficulte,_spriteBatch, this);
            listeGameObject.Add(avion);
            listeGameObject.Add(tm);
            listeAvion.Add(avion);
            collisionneur = new Collisionneur(listeAvion,tm, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight,_spriteBatch,this);
            listeGameObject.Add(collisionneur);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            background = Content.Load<Texture2D>("background");
            textureText = Content.Load<SpriteFont>("score");
            ChargeHighScore();
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


            if (collisionneur.dead)
            {
                pause = true;
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
                    score.ReinitScore();
                    Initialize();
                    ResetElapsedTime();
                    pause = false;
                    collisionneur.dead = false;
                }
            }

            if (pause)
            {
                if (score.GetPoint() > highScore.GetPoint())
                {
                    persistence.Save(chemin, score);
                }
                ChargeHighScore();
            }
            else
            {
                score.AddPoint();
                foreach (GameObject gameObject in listeGameObject)
                {
                    gameObject.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            if (!collisionneur.dead)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(textureText, "Score : " + score.GetPoint(), new Vector2(20, 05), Color.Red);
                _spriteBatch.DrawString(textureText, "HighScore : " + highScore.GetPoint(), new Vector2(20, 85), Color.Red);
                _spriteBatch.DrawString(textureText, "Difficulte : " + difficulte.ToString(), new Vector2(420, 05), Color.Red);

                foreach (GameObject gameObject in listeGameObject)
                {
                    gameObject.Draw(gameTime);
                }
            }
            else if (collisionneur.dead)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.DarkGray);
                _spriteBatch.DrawString(textureText, "HighScore : " + highScore.GetPoint(), new Vector2(620, 480), Color.Red);
                _spriteBatch.DrawString(textureText, "Score : " + score.GetPoint(), new Vector2(700, 400), Color.Red);
                _spriteBatch.DrawString(textureText, "Perdu !", new Vector2(725, 330), Color.Red);
                _spriteBatch.DrawString(textureText, "A: Facile   Z: Moyen   E: Difficile", new Vector2(500, 05), Color.Red);
                _spriteBatch.DrawString(textureText, "Difficulte : " + difficulte.ToString(), new Vector2(630, 85), Color.Red);
                _spriteBatch.DrawString(textureText, "R: Restart", new Vector2(700, 550), Color.Red);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ChargeHighScore()
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
