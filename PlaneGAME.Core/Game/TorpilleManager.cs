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
        public Difficulte difficulte;
        Torpille torpille;
        public List<Torpille> listeTorpilles = new List<Torpille>();
        TorpillesFactory factory;

        public TorpilleManager(Difficulte difficulte, SpriteBatch spriteBatch, Microsoft.Xna.Framework.Game game) : base(game, spriteBatch)
        {
            this.difficulte = difficulte;
            factory = new TorpillesFactory(difficulte, spriteBatch, game);
            listeTorpilles = factory.GetListTorpille();
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

        public void setDifficulte(Difficulte difficulte)
        {
            this.difficulte = difficulte;
        }
    }
}
