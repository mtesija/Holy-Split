﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HolySplit
{
    class Player : PhysicsObject
    {

        public Player(Vector2 location)
        {
            this.location = location;
            speed = 1;
            velocity = Vector2.Zero;
            hitbox = new Rectangle((int)location.X, (int)location.Y, CHARACTER_SIZE, CHARACTER_SIZE);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboard = Keyboard.GetState();

            velocity = Vector2.Zero;
            if (currentKeyboard.IsKeyDown(Keys.W))
            {
                velocity.Y = -1;
            }
            if (currentKeyboard.IsKeyDown(Keys.A))
            {
            }
            if (currentKeyboard.IsKeyDown(Keys.S))
            {
            }
            if (currentKeyboard.IsKeyDown(Keys.D))
            {
            }
            if (currentKeyboard.IsKeyDown(Keys.NumPad1))
            {
            }
            if (currentKeyboard.IsKeyDown(Keys.NumPad2))
            {
            }
            if (currentKeyboard.IsKeyDown(Keys.NumPad3))
            {
            }
            velocity.Normalize();
            velocity *= speed;

            location.X += velocity.X;
            location.Y += velocity.Y;

            hitbox.X = (int)location.X;
            hitbox.Y = (int)location.Y;
        }
    }
}
