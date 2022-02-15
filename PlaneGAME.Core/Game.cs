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
        static MyoManager mgr;

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
            // TODO: Add your initialization logic here
            avion = new Avion();
            avionPosition = new Vector2(avion.avionPosition.X,avion.avionPosition.Y);
            speed = 1000f;
            torpille = new Torpille(2000);
            torpillePosition = new Vector2(torpille.torpillePosition.X, torpille.torpillePosition.Y);
            torpille2 = new Torpille(2640);
            torpillePosition2 = new Vector2(torpille2.torpillePosition.X, torpille2.torpillePosition.Y);
            torpille3 = new Torpille(3280);
            torpillePosition3 = new Vector2(torpille3.torpillePosition.X, torpille3.torpillePosition.Y);
            mgr = new MyoManager();

            mgr.Init();
            //mgr.MyoConnected += Mgr_MyoConnected;
            //mgr.MyoLocked += Mgr_MyoLocked;
            //mgr.MyoUnlocked += Mgr_MyoUnlocked;
            //mgr.PoseChanged += Mgr_PoseChanged;
            //mgr.HeldPoseTriggered += Mgr_HeldPoseTriggered;
            //mgr.PoseSequenceCompleted += Mgr_PoseSequenceCompleted;
            mgr.MyoConnected += Mgr_MyoConnected1;
            mgr.StartListening();
            ReadKey();
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

                avionHitbox = new Rectangle((int)avionPosition.X - avionTexture.Width/4, (int)avionPosition.Y - avionTexture.Height/4, avionTexture.Width/3 + avionTexture.Width/7,avionTexture.Height/3 + avionTexture.Height/8);
                torpilleHitbox = new Rectangle((int)torpillePosition.X - torpilleTexture.Width/4, (int)torpillePosition.Y - torpilleTexture.Height/4, torpilleTexture.Width/3 + torpilleTexture.Width/7, torpilleTexture.Height/3 + torpilleTexture.Height/8);
                torpilleHitbox2 = new Rectangle((int)torpillePosition2.X - torpilleTexture.Width / 4, (int)torpillePosition2.Y - torpilleTexture.Height / 4, torpilleTexture.Width / 3 + torpilleTexture.Width / 7, torpilleTexture.Height/3 + torpilleTexture.Height / 8);
                torpilleHitbox3 = new Rectangle((int)torpillePosition3.X - torpilleTexture.Width / 4, (int)torpillePosition3.Y - torpilleTexture.Height / 4, torpilleTexture.Width / 3 + torpilleTexture.Width / 7, torpilleTexture.Height/3 + torpilleTexture.Height / 8);
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
                _spriteBatch.DrawString(scoreFont, "HighScore : " + highScore.ToString(), new Vector2(660, 480), Color.Red);
                _spriteBatch.DrawString(scoreFont, "Score : " + score.ToString(), new Vector2(700,400), Color.Red);
                _spriteBatch.DrawString(scoreFont, "Perdu !", new Vector2(725, 330), Color.Red);

                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private static void Mgr_MyoConnected1(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            //mgr.SubscribeToOrientationData(0, (source, args) => WriteLine($"{args.Yaw:0.00} ; {args.Pitch:0.00} ; {args.Roll:0.00}"));
            //mgr.SubscribeToGyroscopeData(0, (source, args) => WriteLine($"{args.Gyroscope.X:00.00} ; {args.Gyroscope.Y:00.00} ; {args.Gyroscope.Z:00.00}"));
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
            WriteLine($"{e.Pose}");
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

    }
}
