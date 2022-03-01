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
        float speed;
        public Avion()
        {
            avionPosition.Y = 540;
            avionPosition.X = 960;
            speed = 1000f;
        }

        /*public Vector2 update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                avionPosition.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down))
                avionPosition.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left))
            {
                avionPosition.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                avionTexture = Content.Load<Texture2D>("plane_revert");
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                avionPosition.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                avionTexture = Content.Load<Texture2D>("plane");
            }

            if (avionPosition.X > _graphics.PreferredBackBufferWidth - avionTexture.Width / 5)
                avionPosition.X = _graphics.PreferredBackBufferWidth - avionTexture.Width / 5;
            else if (avionPosition.X < avionTexture.Width / 5)
                avionPosition.X = avionTexture.Width / 5;

            if (avionPosition.Y > _graphics.PreferredBackBufferHeight - avionTexture.Height / 5)
                avionPosition.Y = _graphics.PreferredBackBufferHeight - avionTexture.Height / 5;
            else if (avionPosition.Y < avionTexture.Height / 5)
                avionPosition.Y = avionTexture.Height / 5;
        }*/

    }
}


