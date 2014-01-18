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
        public int selectedWeapon;

        public Player(Vector2 location)
        {
            this.location = location;
            speed = PLAYER_SPEED;
            velocity = Vector2.Zero;
            hitbox = new Rectangle((int)location.X, (int)location.Y, CHARACTER_SIZE, CHARACTER_SIZE);
            selectedWeapon = 0;
            color = YELLOW;
            destroyThis = false;
        }

        public void Collide(Blob b)
        {
            if (hitbox.Intersects(b.hitbox))
                destroyThis = true;
        }

        public void Update(GameTime gameTime, ref List<Bullet> bullets)
        {
            KeyboardState currentKeyboard = Keyboard.GetState();

            velocity = Vector2.Zero;
            if (currentKeyboard.IsKeyDown(Keys.W) || currentKeyboard.IsKeyDown(Keys.Up))
            {
                velocity.Y += -1;
            }
            if (currentKeyboard.IsKeyDown(Keys.A) || currentKeyboard.IsKeyDown(Keys.Left))
            {
                velocity.X += -1;
            }
            if (currentKeyboard.IsKeyDown(Keys.S) || currentKeyboard.IsKeyDown(Keys.Down))
            {
                velocity.Y += 1;
            }
            if (currentKeyboard.IsKeyDown(Keys.D) || currentKeyboard.IsKeyDown(Keys.Right))
            {
                velocity.X += 1;
            }
            if (currentKeyboard.IsKeyDown(Keys.D1))
            {
                selectedWeapon = 0;
            }
            if (currentKeyboard.IsKeyDown(Keys.D2))
            {
                selectedWeapon = 1;
            }
            if (currentKeyboard.IsKeyDown(Keys.D3))
            {
                selectedWeapon = 2;
            }

            if(velocity.X != 0 || velocity.Y != 0)
                velocity.Normalize();
            velocity *= speed;

            location.X += velocity.X;
            location.Y += velocity.Y;

            if (location.X < 0 + WALL_SIZE)
                location.X = 0 + WALL_SIZE;
            else if (location.X > HolySplitGame.SCREEN_WIDTH - CHARACTER_SIZE - WALL_SIZE)
                location.X = HolySplitGame.SCREEN_WIDTH - CHARACTER_SIZE - WALL_SIZE;
            if (location.Y < 0 + WALL_SIZE)
                location.Y = 0 + WALL_SIZE;
            else if (location.Y > HolySplitGame.SCREEN_HEIGHT - CHARACTER_SIZE - WALL_SIZE)
                location.Y = HolySplitGame.SCREEN_HEIGHT - CHARACTER_SIZE - WALL_SIZE;

            hitbox.X = (int)location.X;
            hitbox.Y = (int)location.Y;
        }
    }
}
