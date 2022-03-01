using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlaneGAME.Core.Game
{
    public class GameObject : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }


        public GameObject(Microsoft.Xna.Framework.Game game, SpriteBatch spritebatch) : base(game)
        {
            spriteBatch = spritebatch;
        }
    }
}
