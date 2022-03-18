using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyoLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaneGAME.Core.Game
{
    public class Avion : GameObject
    {
        public Vector2 avionPosition;
        public Texture2D avionTexture;
        public Rectangle avionHitbox;
        float speed; 
        SpriteFont textureText;

        MyoManager mgr;
        MyoSharp.Device.MyoEventArgs ear;
        MyoSharp.Poses.Pose pose;

        public Avion(SpriteBatch spriteBatch, Microsoft.Xna.Framework.Game game) : base(game, spriteBatch)
        {
            this.Initialize();
            this.LoadContent();
        }
        public override void Initialize()
        {
            avionPosition.Y = 540;
            avionPosition.X = 960;
            speed = 1000f;

            mgr = new MyoManager();
            mgr.Init();
            mgr.UnlockAll(MyoSharp.Device.UnlockType.Hold);
            mgr.MyoLocked += Mgr_MyoLocked;
            //mgr.PoseChanged += Mgr_PoseChanged;
            mgr.MyoConnected += Mgr_MyoConnected1;
            mgr.StartListening();
        }


        protected override void LoadContent()
        {
            avionTexture = Game.Content.Load<Texture2D>("plane");
            textureText = Game.Content.Load<SpriteFont>("score");
        }

        public override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up) || pose == MyoSharp.Poses.Pose.)
                    avionPosition.Y -= speed* (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down) || pose == MyoSharp.Poses.Pose.WaveIn)
                avionPosition.Y += speed* (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left) /*|| pose == MyoSharp.Poses.Pose.WaveIn*/)
            {
                avionPosition.X -= speed* (float) gameTime.ElapsedGameTime.TotalSeconds;
                avionTexture = Game.Content.Load<Texture2D>("plane_revert");
            }

            if (kstate.IsKeyDown(Keys.Right) /*|| pose == MyoSharp.Poses.Pose.WaveOut*/)
            {
                avionPosition.X += speed* (float) gameTime.ElapsedGameTime.TotalSeconds;
                avionTexture = Game.Content.Load<Texture2D>("plane");
            }

            avionHitbox = new Rectangle((int)avionPosition.X - avionTexture.Width / 4, (int)avionPosition.Y - avionTexture.Height / 4, avionTexture.Width / 3 + avionTexture.Width / 7, avionTexture.Height / 3 + avionTexture.Height / 8);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(
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
            spriteBatch.DrawString(textureText, pose.ToString(), new Vector2(630, 85), Color.Red);
        }

        /*private static void Mgr_MyoConnected1(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            //mgr.SubscribeToOrientationData(0, (source, args) => WriteLine($"{args.Yaw:0.00} ; {args.Pitch:0.00} ; {args.Roll:0.00}"));

            mgr.SubscribeToAccelerometerData(0, (source, args) => WriteLine($"{args.Accelerometer.X:00.00} ; {args.Accelerometer.Y:00.00} ; {args.Accelerometer.Z:00.00}"));
        }*/

        private void Mgr_PoseChanged(object sender, MyoSharp.Device.PoseEventArgs e)
        {
            pose = e.Pose;
        }

        private void Mgr_MyoLocked(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            mgr.UnlockAll(MyoSharp.Device.UnlockType.Hold);
            Console.WriteLine($"{e.Myo} has been locked");
        }
    }
}


