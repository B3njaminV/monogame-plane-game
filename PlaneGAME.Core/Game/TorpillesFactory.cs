using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PlaneGAME.Core.Game
{
    /**
     * Cette classe est une Factory qui permet de créer une liste avec un certain nombre de torpilles
     */
    class TorpillesFactory : GameObject
    {
        List<Torpille> listeTorpilles = new List<Torpille>();

        public TorpillesFactory(Difficulte difficulte, SpriteBatch spriteBatch, Microsoft.Xna.Framework.Game game) : base (game, spriteBatch)
        {
            Initialiser(difficulte);
        }

        public void Initialiser(Difficulte difficulte)
        {
            switch (difficulte)
            {
                case Difficulte.Facile:
                    Ajouter(2);
                    break;
                case Difficulte.Moyen:
                    Ajouter(3);
                    break;
                case Difficulte.Difficile:
                    Ajouter(4);
                    break;
                default:
                    break;
            }

        }

        /**
         * On ajoute (nombre) torpilles à notre liste de Torpilles
         */
        public void Ajouter(int nombre)
        {
            for(int i=0; i<nombre; i++)
            {
                listeTorpilles.Add(new Torpille(i*500+3000, spriteBatch, Game));
            }
        }

        /**
         * On récupère notre liste de Torpilles
         */
        public List<Torpille> GetListTorpille()
        {
            return listeTorpilles;
        }
    }
}
