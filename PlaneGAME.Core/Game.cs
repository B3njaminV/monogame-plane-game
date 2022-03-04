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
        int score = 0;
        int highScore = 0;
        SpriteFont scoreFont;
        //TorpilleManager tm;
        //MyoSharp.Device.PoseEventArgs currentPos;
        //static MyoManager mgr;
        //MyoSharp.Device.MyoEventArgs e;

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

            //mgr = new MyoManager();
            //mgr.Init();
            //mgr.MyoConnected += Mgr_MyoConnected;
            //mgr.MyoLocked += Mgr_MyoLocked;
            //mgr.MyoUnlocked += Mgr_MyoUnlocked;
            //mgr.PoseChanged += Mgr_PoseChanged;
            //mgr.HeldPoseTriggered += Mgr_HeldPoseTriggered;
            //mgr.PoseSequenceCompleted += Mgr_PoseSequenceCompleted;
            //mgr.MyoConnected += Mgr_MyoConnected1;
            //mgr.StartListening();
            //ReadKey();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("background");
            scoreFont = Content.Load<SpriteFont>("score");
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
                    score = 0;
                    dead = false;
                    pause = false;
                    Initialize();
                    ResetElapsedTime();
                }
            }

            if (!pause)
            {
                score += 1;

                /*if (avionHitbox.Intersects(torpilleHitbox) || avionHitbox.Intersects(torpilleHitbox2) || avionHitbox.Intersects(torpilleHitbox3) || avionHitbox.Intersects(torpilleHitbox4))
                {
                    dead = true;
                    pause = true;
                    if (score > highScore)
                    {
                        highScore = score;
                    }
                }*/
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            if (!dead)
            {
                //tm.Draw();
                _spriteBatch.DrawString(scoreFont, "Score : " + score.ToString(), new Vector2(20, 05), Color.Red);
                _spriteBatch.DrawString(scoreFont, "HighScore : " + highScore.ToString(), new Vector2(20, 85), Color.Red);
                _spriteBatch.DrawString(scoreFont, "Difficulte : " + difficulte.ToString(), new Vector2(420, 05), Color.Red);
            }
            else if (dead)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.DarkGray);
                _spriteBatch.DrawString(scoreFont, "HighScore : " + highScore.ToString(), new Vector2(660, 480), Color.Red);
                _spriteBatch.DrawString(scoreFont, "Score : " + score.ToString(), new Vector2(700, 400), Color.Red);
                _spriteBatch.DrawString(scoreFont, "Perdu !", new Vector2(725, 330), Color.Red);
                _spriteBatch.DrawString(scoreFont, "A: Facile   Z: Moyen   E: Difficile", new Vector2(500, 05), Color.Red);
                _spriteBatch.DrawString(scoreFont, "Difficulte : " + difficulte.ToString(), new Vector2(630, 85), Color.Red);
                _spriteBatch.DrawString(scoreFont, "R: Restart", new Vector2(700, 550), Color.Red);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }




        /*
        private static void Mgr_MyoConnected1(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            //mgr.SubscribeToOrientationData(0, (source, args) => WriteLine($"{args.Yaw:0.00} ; {args.Pitch:0.00} ; {args.Roll:0.00}"));
            
            mgr.SubscribeToAccelerometerData(0, (source, args) => WriteLine($"{args.Accelerometer.X:00.00} ; {args.Accelerometer.Y:00.00} ; {args.Accelerometer.Z:00.00}"));
        }

        private static void Mgr_PoseSequenceCompleted(object sender, PoseSequenceEventArgs e)
        {
            WriteLine($"Sequence completed : {e.Poses.Select(p => p.ToString()).Aggregate("", (chaine, s) => $"{chaine} {s}")}");
        }

        private static Dictionary<Pose, string> traductions = new Dictionary<Pose, string>()
        {
            [Pose.Fist] = "POING FERME",
            [Pose.FingersSpread] = "MAIN OUVERTE",
            [Pose.WaveOut] = "MAIN A DROITE"
        };


        private static void Mgr_HeldPoseTriggered(object sender, MyoSharp.Device.PoseEventArgs e)
        {
            WriteLine($"HeldPose : {traductions[e.Pose]}");
        }

        private static void Mgr_PoseChanged(object sender, MyoSharp.Device.PoseEventArgs e)
        {
            //WriteLine($"Pose : {e.Pose}");
            //currentPos = e;
        }

        private static void Mgr_MyoUnlocked(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            WriteLine($"{e.Myo} has been unlocked");
        }

        private static void Mgr_MyoLocked(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            WriteLine($"{e.Myo} has been locked");
        }



        private async static void Mgr_MyoConnected(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            WriteLine($"{e.Myo} connected ({e.Myo.Arm}, {e.Myo.Handle})");
            mgr.Vibrate(MyoSharp.Device.VibrationType.Long);
            await Task.Delay(2000);
            mgr.Vibrate(MyoSharp.Device.VibrationType.Medium);
            await Task.Delay(2000);
            mgr.Vibrate(MyoSharp.Device.VibrationType.Short);
            await Task.Delay(2000);
            mgr.VibrateAll();
            await Task.Delay(5000);
            mgr.Lock();
            await Task.Delay(5000);
            mgr.Unlock(MyoSharp.Device.UnlockType.Hold);
            mgr.AddHeldPose(mgr.Myos.First(), Pose.Fist, Pose.FingersSpread);
            await Task.Delay(10000);
            mgr.AddHeldPose(mgr.Myos.First(), Pose.Fist, Pose.WaveOut);
            await Task.Delay(10000);
            WriteLine("Pose Sequence");
            mgr.AddPoseSequence(mgr.Myos.First(), Pose.Fist, Pose.FingersSpread);
        }
        */
    }
}
