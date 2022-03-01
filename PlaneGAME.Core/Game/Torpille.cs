using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaneGAME.Core.Game
{
    public class Torpille : GameObject
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D torpilleTexture;
        Vector2 torpillePosition = Vector2.Zero;
        Rectangle torpilleHitbox;
        float speed;

        public Torpille(float x)
        {
            Random rand = new Random();
            torpillePosition.Y = rand.Next(50,1030);
            torpillePosition.X = x;
            speed = 500f;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            torpilleTexture = Content.Load<Texture2D>("torpedo_black_revert");
        }

        public override void Update(GameTime gameTime)
        {
            Random rand = new Random();
            torpillePosition.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (torpillePosition.X <= -100){
                torpillePosition.X = 2000;
                torpillePosition.Y = rand.Next(50, 1030);
            }

            if(speed < 2000f)
            {
                speed += 1f;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
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
        }
    }
}
