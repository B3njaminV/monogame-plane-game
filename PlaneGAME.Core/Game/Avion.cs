using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaneGAME.Core.Game
{
    public class Avion
    {
        public Vector2 avionPosition;

        public Avion()
        {
            avionPosition.Y = 500;
            avionPosition.X = 2000;
        }

        public Vector2 Update(GameTime gameTime)
        {
            Random rand = new Random();
            avionPosition.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (torpillePosition.X <= -100)
            {
                torpillePosition.X = 2000;
                torpillePosition.Y = rand.Next(50, 1030);
            }

            if (speed < 2000f)
            {
                speed += 1f;
            }
            return torpillePosition;

            // if collision blablabla
        }
    }
}

