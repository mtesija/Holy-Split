using System;
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
        public MouseState previousMouse;
        public Vector2 aim;

        public Player(Vector2 location)
        {
            this.location = location;
            speed = PLAYER_SPEED;
            velocity = Vector2.Zero;
            hitbox = new Rectangle((int)location.X, (int)location.Y, CHARACTER_SIZE, CHARACTER_SIZE);
            selectedWeapon = 0;
            color = RED;
            destroyThis = false;
            previousMouse = Mouse.GetState();
            aim = new Vector2(0, 1);
        }

        public void Collide(Blob b)
        {
            if (hitbox.Intersects(b.hitbox))
                destroyThis = true;
        }

        public void Update(GameTime gameTime, ref List<Bullet> bullets, ref SoundEffect shoot)
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
                color = RED;
            }
            if (currentKeyboard.IsKeyDown(Keys.D2))
            {
                selectedWeapon = 1;
                color = YELLOW;
            }
            if (currentKeyboard.IsKeyDown(Keys.D3))
            {
                selectedWeapon = 2;
                color = BLUE;
            }

            //Aiming
            MouseState mouse = new MouseState();
            mouse = Mouse.GetState();
            aim.X = mouse.X;
            aim.Y = mouse.Y;
            if (aim.X < 0 + WALL_SIZE)
            {
                aim.X = 0 + WALL_SIZE;
                Mouse.SetPosition(0 + WALL_SIZE, (int)aim.Y);
            }
            else if (aim.X > HolySplitGame.SCREEN_WIDTH - CHARACTER_SIZE - WALL_SIZE)
            {
                aim.X = HolySplitGame.SCREEN_WIDTH - CHARACTER_SIZE - WALL_SIZE;
                Mouse.SetPosition(HolySplitGame.SCREEN_WIDTH - CHARACTER_SIZE - WALL_SIZE, (int)aim.Y);
            }

            if (aim.Y < 0 + WALL_SIZE)
            {
                aim.Y = 0 + WALL_SIZE;
                Mouse.SetPosition((int)aim.X, 0 + WALL_SIZE);
            }
            else if (aim.Y > HolySplitGame.SCREEN_HEIGHT - CHARACTER_SIZE - WALL_SIZE)
            {
                aim.Y = HolySplitGame.SCREEN_HEIGHT - CHARACTER_SIZE - WALL_SIZE;
                Mouse.SetPosition((int)aim.X, HolySplitGame.SCREEN_HEIGHT - CHARACTER_SIZE - WALL_SIZE);
            }

            //Check for mouse click to shoot balls
            if (mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released)
            {
                Vector2 bulletVelocity = aim - location;
                if(bulletVelocity.X != 0 || bulletVelocity.Y != 0)
                {
                    bulletVelocity.Normalize();
                    bullets.Add(new Bullet(this.location, bulletVelocity, this.color));
                    shoot.Play();
                }
            }
            previousMouse = mouse;

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
