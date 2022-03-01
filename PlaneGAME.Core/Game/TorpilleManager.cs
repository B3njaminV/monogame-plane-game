using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneGAME.Core.Game
{
    class TorpilleManager : GameObject
    {
        Torpille torpille;
        Vector2 torpillePosition = Vector2.Zero;
        Rectangle torpilleHitbox;
        Texture2D torpilleTexture;

        public TorpilleManager(Texture2D text ){
            torpilleTexture = text;
        }

        public void LoadContent()
        {
            torpille = new Torpille(2000);
            torpillePosition = new Vector2(torpille.torpillePosition.X, torpille.torpillePosition.Y);
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
