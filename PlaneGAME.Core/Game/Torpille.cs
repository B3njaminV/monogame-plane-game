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
        Texture2D torpilleTexture;
        Vector2 torpillePosition = Vector2.Zero;
        Rectangle torpilleHitbox;
        float speed;

        public Torpille(float x,SpriteBatch spriteBatch, Microsoft.Xna.Framework.Game game) : base(game, spriteBatch)
        {
            torpillePosition.X = x;
            this.Initialize();
            this.LoadContent();
        }

        public override void Initialize()
        {
            Random rand = new Random();
            torpillePosition.Y = rand.Next(50, 1030);
            speed = 500f;
        }

        protected override void LoadContent()
        {
            torpilleTexture = Game.Content.Load<Texture2D>("torpedo_black_revert");
        }

        public override void Update(GameTime gameTime)
        {
            Random rand = new Random();
            torpillePosition.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (torpillePosition.X <= -100){
                torpillePosition.X = 2000;
                torpillePosition.Y = rand.Next(50, 1030);
            }

            torpilleHitbox = new Rectangle((int)torpillePosition.X - torpilleTexture.Width / 4, (int)torpillePosition.Y - torpilleTexture.Height / 4, torpilleTexture.Width / 3 + torpilleTexture.Width / 7, torpilleTexture.Height / 3 + torpilleTexture.Height / 8);

            if (speed < 2000f)
            {
                speed += 1f;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(
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
