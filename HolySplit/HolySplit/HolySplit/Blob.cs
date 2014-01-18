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
            //SPLIT HERE

            this.velocity = new Vector2(player.location.X - this.location.X, player.location.Y - this.location.Y);

            if (color == RED)
            {
                Matrix rotation = new Matrix(.866f, .5f, 0, 0, -.5f, .866f, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                this.velocity = Vector2.Transform(this.velocity, rotation);
            }
            else if (color == BLUE)
            {
                Matrix rotation = new Matrix(.866f, -.5f, 0, 0, .5f, .866f, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                this.velocity = Vector2.Transform(this.velocity, rotation);
            }
            else if (color == YELLOW)
            {

            }
            else if (color == PURPLE)
            {
                Matrix rotation = new Matrix(.174f, -.985f, 0, 0, .985f, .174f, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                this.velocity = Vector2.Transform(this.velocity, rotation);
            }
            else if (color == GREEN)
            {
                Matrix rotation = new Matrix(.174f, .985f, 0, 0, -.985f, .174f, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                this.velocity = Vector2.Transform(this.velocity, rotation);
            }
            else if (color == ORANGE)
            {

            }

            this.velocity.Normalize();
            this.velocity *= speed;

            this.location.X += this.velocity.X;
            this.location.Y += this.velocity.Y;

            this.hitbox.X = (int)this.location.X;
            this.hitbox.Y = (int)this.location.Y;
        }
    }
}
