using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneGAME.Core.Game
{
    class Collisionneur : GameObject
    {
        private Microsoft.Xna.Framework.Game game;
        private List<Avion> listeAvion;
        private TorpilleManager torpilleM;
        private int largeur;
        private int hauteur;
        public bool dead = false;

        public Collisionneur(List<Avion> avions,TorpilleManager tm, int width, int height, SpriteBatch spriteBatch, Microsoft.Xna.Framework.Game game) : base(game, spriteBatch)
        {
            listeAvion = avions;
            torpilleM = tm;
            largeur = width;
            hauteur = height;
        }

        public override void Update(GameTime gameTime)
        {
            foreach(Avion avion in listeAvion)
            {
                if (avion.avionPosition.X > largeur - avion.avionTexture.Width / 2)
                    avion.avionPosition.X = largeur - avion.avionTexture.Width / 2;
                else if (avion.avionPosition.X < avion.avionTexture.Width / 4)
                    avion.avionPosition.X = avion.avionTexture.Width / 4;

                if (avion.avionPosition.Y > hauteur - avion.avionTexture.Height / 4)
                    avion.avionPosition.Y = hauteur - avion.avionTexture.Height / 4;
                else if (avion.avionPosition.Y < avion.avionTexture.Height / 4)
                    avion.avionPosition.Y = avion.avionTexture.Height / 4;

                foreach (Torpille torpille in torpilleM.listeTorpilles) {
                    if (avion.avionHitbox.Intersects(torpille.torpilleHitbox))
                    {
                        dead = true;

                    }
                }
            }
        }
    }
}
