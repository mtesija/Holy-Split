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
    class Bullet : PhysicsObject
    {
        public Bullet(Vector2 location, Vector2 velocity, Color color)
        {
            speed = BULLET_SPEED;

            this.location = location;
            this.velocity = velocity * speed;
            this.color = color;

            hitbox = new Rectangle((int)location.X, (int)location.Y, BULLET_SIZE, BULLET_SIZE);

            destroyThis = false;
        }

        public void Update(GameTime gameTime)
        {
            this.location.X += this.velocity.X;
            this.location.Y += this.velocity.Y;

            if (location.X < 0 + WALL_SIZE)
                this.destroyThis = true;
            else if (location.X > HolySplitGame.SCREEN_WIDTH - CHARACTER_SIZE - WALL_SIZE)
                this.destroyThis = true;

            if (location.Y < 0 + WALL_SIZE)
                this.destroyThis = true;
            else if (location.Y > HolySplitGame.SCREEN_HEIGHT - CHARACTER_SIZE - WALL_SIZE)
                this.destroyThis = true;

            this.hitbox.X = (int)this.location.X;
            this.hitbox.Y = (int)this.location.Y;
        }
    }
}
