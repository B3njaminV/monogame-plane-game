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

        public TorpilleManager(Difficulte difficulte, SpriteBatch spriteBatch, Microsoft.Xna.Framework.Game game) : base(game, spriteBatch)
        {
            this.difficulte = difficulte;
            torpille = new Torpille(2000, spriteBatch, game);
            listeTorpilles.Add(torpille);
            torpille = new Torpille(3000, spriteBatch, game);
            listeTorpilles.Add(torpille);
            torpille = new Torpille(2500, spriteBatch, game);
            listeTorpilles.Add(torpille);
            torpille = new Torpille(3500, spriteBatch, game);
            listeTorpilles.Add(torpille);
        }

        public override void Update(GameTime gameTime)
        {
            if(difficulte == Difficulte.Facile)
            {
                foreach (Torpille torpille in listeTorpilles.SkipLast<Torpille>(2))
                {
                    torpille.Update(gameTime);
                }
            }
            else if(difficulte == Difficulte.Moyen)
            {
                foreach (Torpille torpille in listeTorpilles.SkipLast<Torpille>(1))
                {
                    torpille.Update(gameTime);
                }
            }
            else
            {
                foreach (Torpille torpille in listeTorpilles)
                {
                    torpille.Update(gameTime);
                }
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
