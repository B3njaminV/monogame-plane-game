using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        }

        protected override void LoadContent()
        {
            avionTexture = Game.Content.Load<Texture2D>("plane");
        }

        public override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                avionPosition.Y -= speed* (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down))
                avionPosition.Y += speed* (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left))
            {
                avionPosition.X -= speed* (float) gameTime.ElapsedGameTime.TotalSeconds;
                avionTexture = Game.Content.Load<Texture2D>("plane_revert");
            }

            if (kstate.IsKeyDown(Keys.Right))
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
        }
    }
}


