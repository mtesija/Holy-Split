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
        int direction = 0;
        int directionMod = 0;
        Timer splitTimer;

        public Blob(Vector2 location, Color color, int speed)
        {
            this.location = location;
            this.color = color;
            this.hitbox = new Rectangle((int)location.X, (int)location.Y, CHARACTER_SIZE, CHARACTER_SIZE);
            this.velocity = Vector2.Zero;
            this.speed = speed;
            splitTimer = new Timer(5.0f);
            destroyThis = false;
        }

        public void Split(ref List<Blob> blobs)
        {
            blobs.Add(new Blob(this.location, this.color, this.speed));
        }

        public void Kill(ref List<Blob> blobs)
        {
            if (color == GRAY)
            {
                blobs.Add(new Blob(this.location, REDORANGE, this.speed));
                blobs.Add(new Blob(this.location, REDPURPLE, this.speed));
                blobs.Add(new Blob(this.location, BLUEGREEN, this.speed));
                blobs.Add(new Blob(this.location, BLUEPURPLE, this.speed));
                blobs.Add(new Blob(this.location, YELLOWGREEN, this.speed));
                blobs.Add(new Blob(this.location, YELLOWORANGE, this.speed));
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
            else if (color == REDORANGE)
            {
                blobs.Add(new Blob(this.location, ORANGE, this.speed));
                blobs.Add(new Blob(this.location, RED, this.speed));
            }
            else if (color == REDPURPLE)
            {
                blobs.Add(new Blob(this.location, PURPLE, this.speed));
                blobs.Add(new Blob(this.location, RED, this.speed));
            }
            else if (color == BLUEPURPLE)
            {
                blobs.Add(new Blob(this.location, PURPLE, this.speed));
                blobs.Add(new Blob(this.location, BLUE, this.speed));
            }
            else if (color == BLUEGREEN)
            {
                blobs.Add(new Blob(this.location, GREEN, this.speed));
                blobs.Add(new Blob(this.location, BLUE, this.speed));
            }
            else if (color == YELLOWORANGE)
            {
                blobs.Add(new Blob(this.location, ORANGE, this.speed));
                blobs.Add(new Blob(this.location, YELLOW, this.speed));
            }
            else if (color == YELLOWGREEN)
            {
                blobs.Add(new Blob(this.location, GREEN, this.speed));
                blobs.Add(new Blob(this.location, YELLOW, this.speed));
            }

            destroyThis = true;
        }

        private void rotateVec(Vector2 vec, double deg)
        {
            double rad = deg * Math.PI / 180;
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);
            Matrix rotation = new Matrix(cos, sin, 0, 0, -sin, cos, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Vector2.Transform(vec, rotation);
            return;
        }

        public void Update(GameTime gameTime, ref Player player, ref Random random, ref List<Blob> blobs)
        {
            //SPLIT HERE TIMER BASED
            if (splitTimer.CheckTimer())
            {
                Kill(ref blobs);
            }

            this.velocity = player.location - this.location;

            //MAKE ALL THESE RANDOM NUMBER REFRESH TIMER BASED
            this.directionMod = random.Next(-5, 5);

            if (color == RED)
            {
                rotateVec(this.velocity, 70 + this.directionMod);
            }
            else if (color == BLUE)
            {
                rotateVec(this.velocity, -15 + this.directionMod);
            }
            else if (color == YELLOW)
            {
                rotateVec(this.velocity, 40 + this.directionMod);
            }
            else if (color == PURPLE)
            {
                rotateVec(this.velocity, -40 + this.directionMod);
            }
            else if (color == GREEN)
            {
                rotateVec(this.velocity, -70 + this.directionMod);
            }
            else if (color == ORANGE)
            {
                rotateVec(this.velocity, 15 + this.directionMod);
            }
            else if (color == REDORANGE)
            {
                this.direction = random.Next(0, 30);
                rotateVec(this.velocity, this.direction);
            }
            else if (color == REDPURPLE)
            {
                this.direction = random.Next(60, 90);
                rotateVec(this.velocity, this.direction);
            }
            else if (color == BLUEGREEN)
            {
                this.direction = random.Next(30, 60);
                rotateVec(this.velocity, this.direction);
            }
            else if (color == BLUEPURPLE)
            {
                this.direction = random.Next(-30, 0);
                rotateVec(this.velocity, this.direction);
            }
            else if (color == YELLOWGREEN)
            {
                this.direction = random.Next(-60, -30);
                rotateVec(this.velocity, this.direction);
            }
            else if (color == YELLOWORANGE)
            {
                this.direction = random.Next(-90, -60);
                rotateVec(this.velocity, this.direction);
            }

            if (velocity.X != 0 && velocity.Y != 0)
                this.velocity.Normalize();
            this.velocity *= speed;

            this.location.X += this.velocity.X;
            this.location.Y += this.velocity.Y;

            this.hitbox.X = (int)this.location.X;
            this.hitbox.Y = (int)this.location.Y;
        }
    }
}
