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

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            foreach(Torpille torpille in listeTorpilles)
            {
                torpille.Update(gameTime);
            }
        }

        protected void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            foreach (Torpille torpille in listeTorpilles)
            {
                torpille.Draw(gameTime);
            }
        }
    }
}
