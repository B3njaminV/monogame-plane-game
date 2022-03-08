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
        public List<Torpille> listeTorpilles = new List<Torpille>();

        public TorpilleManager(SpriteBatch spriteBatch, Microsoft.Xna.Framework.Game game) : base(game, spriteBatch)
        {
            torpille = new Torpille(2000, spriteBatch, game);
            listeTorpilles.Add(torpille);
            torpille = new Torpille(2800, spriteBatch, game);
            listeTorpilles.Add(torpille);
            torpille = new Torpille(2400, spriteBatch, game);
            listeTorpilles.Add(torpille);
            torpille = new Torpille(3200, spriteBatch, game);
            listeTorpilles.Add(torpille);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Torpille torpille in listeTorpilles)
            {
                torpille.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Torpille torpille in listeTorpilles)
            {
                torpille.Draw(gameTime);
            }
        }
    }
}
