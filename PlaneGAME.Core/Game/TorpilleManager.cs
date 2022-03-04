using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneGAME.Core.Game
{
    public class TorpilleManager : GameObject
    {
        Torpille torpille;
        Vector2 torpillePosition = Vector2.Zero;
        Rectangle torpilleHitbox;
        Texture2D torpilleTexture;
        List<Torpille> listeTorpilles;

        public TorpilleManager(Texture2D text, SpriteBatch spriteBatch, Microsoft.Xna.Framework.Game game) : base(game, spriteBatch)
        {
            listeTorpilles.Add(new Torpille(2000, spriteBatch, game));
            listeTorpilles.Add(new Torpille(3000, spriteBatch, game));
            listeTorpilles.Add(new Torpille(4000, spriteBatch, game));
            listeTorpilles.Add(new Torpille(5000, spriteBatch, game));
        }

        public void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
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
