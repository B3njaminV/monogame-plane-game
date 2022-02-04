using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaneGAME.Core.Game
{
    class Torpille
    {
        Vector2 torpillePosition;
        float speed;

        public Torpille()
        {

        }

        protected void Update(GameTime gameTime)
        {
            torpillePosition.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // if collision blablabla
        }
    }
}

