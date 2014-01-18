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
    class Blob : PhysicsObject
    {
        Color BLUE = Color.Blue;
        Color YELLOW = Color.Yellow;
        Color RED = Color.Red;
        Color GREEN = Color.Green;
        Color PURPLE = Color.Purple;
        Color ORANGE = Color.Orange;
        Color GRAY = Color.Gray;

        Blob(Vector2 location, Color color, int speed)
        {
            this.location = location;
            this.color = color;
            this.hitbox = new Rectangle((int)location.X, (int)location.Y, CHARACTER_SIZE, CHARACTER_SIZE);
            this.velocity = Vector2.Zero;
            this.speed = speed;
        }

        void Split(ref List<Blob> blobs)
        {
            blobs.Add(new Blob(this.location, this.color, this.speed));
        }

        void Kill(ref List<Blob> blobs)
        {
            if (color == GRAY)
            {
                blobs.Add(new Blob(this.location, PURPLE, this.speed));
                blobs.Add(new Blob(this.location, GREEN, this.speed));
                blobs.Add(new Blob(this.location, ORANGE, this.speed));
            }
            else if (color == PURPLE)
            {
                blobs.Add(new Blob(this.location, RED, this.speed));
                blobs.Add(new Blob(this.location, BLUE, this.speed));
            }
            else if (color == GREEN)
            {
                blobs.Add(new Blob(this.location, BLUE, this.speed));
                blobs.Add(new Blob(this.location, YELLOW, this.speed));
            }
            else if (color == ORANGE)
            {
                blobs.Add(new Blob(this.location, YELLOW, this.speed));
                blobs.Add(new Blob(this.location, RED, this.speed));
            }

            blobs.Remove(this);
        }

        public void Update(GameTime gameTime, ref Player player)
        {


            if (color == GRAY)
            {
                this.velocity = new Vector2(player.location.X - this.location.X, player.location.Y - this.location.Y);
            }
            else if (color == RED)
            {
                this.velocity = new Vector2(player.location.X - this.location.X, player.location.Y - this.location.Y);
            }
            else if (color == BLUE)
            {
                this.velocity = new Vector2(player.location.X - this.location.X, player.location.Y - this.location.Y);
            }
            else if (color == YELLOW)
            {
                this.velocity = new Vector2(player.location.X - this.location.X, player.location.Y - this.location.Y);
            }
            else if (color == PURPLE)
            {
                this.velocity = new Vector2(player.location.X - this.location.X, player.location.Y - this.location.Y);
            }
            else if (color == GREEN)
            {
                this.velocity = new Vector2(player.location.X - this.location.X, player.location.Y - this.location.Y);
            }
            else if (color == ORANGE)
            {
                this.velocity = new Vector2(player.location.X - this.location.X, player.location.Y - this.location.Y);
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
