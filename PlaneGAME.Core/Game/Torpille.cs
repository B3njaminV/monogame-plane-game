using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaneGAME.Core.Game
{
    public class Torpille
    {
        public Vector2 torpillePosition;
        float speed;

        public Torpille(float x)
        {
            Random rand = new Random();
            torpillePosition.Y = rand.Next(50,1030);
            torpillePosition.X = x;
            speed = 500f;
        }

        public Vector2 Update(GameTime gameTime)
        {
            Random rand = new Random();
            torpillePosition.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (torpillePosition.X <= -100){
                torpillePosition.X = 2000;
                torpillePosition.Y = rand.Next(50, 1030);
            }

            if(speed < 2000f)
            {
                speed += 1f;
            }
            return torpillePosition;

            // if collision blablabla
        }
    }
}

