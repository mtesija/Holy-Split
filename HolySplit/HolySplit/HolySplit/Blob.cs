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
        Timer splitTimer;
        Timer directionTimer;
        Timer positiveTimer;
        int positive = 0;

        public Blob(Vector2 location, Color color, int speed)
        {
            this.location = location;
            this.color = color;
            this.hitbox = new Rectangle((int)location.X, (int)location.Y, CHARACTER_SIZE, CHARACTER_SIZE);
            this.velocity = Vector2.Zero;
            this.speed = speed;
            splitTimer = new Timer(Map.random.Next(8, 12));
            directionTimer = new Timer(.1f);
            positiveTimer = new Timer(.1f);
            destroyThis = false;
        }

        public void Collide(Blob b)
        {
            if (b.hitbox.Intersects(hitbox))
            {
                Vector2 difference = b.location - this.location;
                if (difference.X != 0 || difference.Y != 0)
                {
                    difference.Normalize();
                }

                difference *= -1;

                this.location.X += difference.X;
                this.location.Y += difference.Y;

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

        public void Split(ref List<Blob> blobs)
        {
            if (Color.Equals(this.color, GRAY))
            {
                this.Kill(ref blobs);
            }
            else
            {
                blobs.Add(new Blob(this.location, this.color, this.speed));
            }
        }

        public void Kill(ref List<Blob> blobs)
        {
            if (color == GRAY)
            {
                //blobs.Add(new Blob(this.location, REDORANGE, this.speed));
                //blobs.Add(new Blob(this.location, REDPURPLE, this.speed));
                //blobs.Add(new Blob(this.location, BLUEGREEN, this.speed));
                //blobs.Add(new Blob(this.location, BLUEPURPLE, this.speed));
                //blobs.Add(new Blob(this.location, YELLOWGREEN, this.speed));
                //blobs.Add(new Blob(this.location, YELLOWORANGE, this.speed));
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
        
        public void BulletCollide(Bullet b, ref List<Blob> blobs)
        {
            if (b.hitbox.Intersects(hitbox))
            {
                if (b.color == RED)
                {
                    if (Color.Equals(RED, this.color) || Color.Equals(ORANGE, this.color) 
                        || Color.Equals(PURPLE, this.color) || Color.Equals(GRAY, this.color))
                        this.Kill(ref blobs);
                    else
                        this.Split(ref blobs);
                }
                else if (this.color == BLUE)
                {
                    if (Color.Equals(BLUE, this.color) || Color.Equals(GREEN, this.color) 
                        || Color.Equals(PURPLE, this.color) || Color.Equals(GRAY, this.color))
                        this.Kill(ref blobs);
                    else
                        this.Split(ref blobs);
                }
                else if (this.color == YELLOW)
                {
                    if (Color.Equals(YELLOW, this.color) || Color.Equals(ORANGE, this.color) 
                        || Color.Equals(GREEN, this.color) || Color.Equals(GRAY, this.color))
                        this.Kill(ref blobs);
                    else
                        this.Split(ref blobs);
                }
            }
        }

        private void rotateVec(Vector2 vec, double deg)
        {
            double rad = deg * Math.PI / 180;
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);
            Matrix rotation = new Matrix(cos, sin, 0, 0, -sin, cos, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            this.velocity = Vector2.Transform(vec, rotation);
            return;
        }

        public void Update(GameTime gameTime, ref Player player, ref List<Blob> blobs)
        {
            if (splitTimer.CheckTimer())
            {
                Split(ref blobs);
                splitTimer.resetTimer(Map.random.Next(8, 12));
            }

            if (positiveTimer.CheckTimer())
            {
                this.positive = Map.random.Next(0, 2);
                positiveTimer.resetTimer(Map.random.Next(4, 6));
            }

            if (color == GRAY)
            {
                this.velocity = player.location - this.location;
            }
            else if (color == RED)
            {
                if (this.positive == 0)
                    this.direction = Map.random.Next(-45, -15);
                else
                    this.direction = Map.random.Next(15, 45);

                this.velocity = player.location - this.location;
                rotateVec(this.velocity, this.direction);
            }
            else if (color == BLUE)
            {
                if (this.positive == 0)
                    this.direction = Map.random.Next(-60, -30);
                else
                    this.direction = Map.random.Next(30, 60);

                this.velocity = player.location - this.location;
                rotateVec(this.velocity, this.direction);
            }
            else if (color == YELLOW)
            {
                if (this.positive == 0)
                    this.direction = Map.random.Next(-90, -45);
                else
                    this.direction = Map.random.Next(45, 90);

                this.velocity = player.location - this.location;
                rotateVec(this.velocity, this.direction);
            }
            else if (color == PURPLE)
            {
                if (directionTimer.CheckTimer())
                {
                    this.direction = Map.random.Next(-180, 180);
                    directionTimer.resetTimer(Map.random.Next(12, 16));
                }

                this.velocity = new Vector2(1, 0);
                rotateVec(this.velocity, this.direction);
            }
            else if (color == GREEN)
            {
                if (directionTimer.CheckTimer())
                {
                    this.direction = Map.random.Next(-180, 180);
                    directionTimer.resetTimer(Map.random.Next(4, 8));
                }

                this.velocity = new Vector2(1, 0);
                rotateVec(this.velocity, this.direction);
            }
            else if (color == ORANGE)
            {
                if (directionTimer.CheckTimer())
                {
                    this.direction = Map.random.Next(-180, 180);
                    directionTimer.resetTimer(Map.random.Next(6, 11));
                }

                this.velocity = new Vector2(1, 0);
                rotateVec(this.velocity, this.direction);
            }
            else if (color == REDORANGE)
            {

            }
            else if (color == REDPURPLE)
            {

            }
            else if (color == BLUEGREEN)
            {

            }
            else if (color == BLUEPURPLE)
            {

            }
            else if (color == YELLOWGREEN)
            {

            }
            else if (color == YELLOWORANGE)
            {

            }

            if (velocity.X != 0 || velocity.Y != 0)
                this.velocity.Normalize();

            this.velocity *= speed;

            this.location.X += this.velocity.X;
            this.location.Y += this.velocity.Y;

            if (location.X < 0 + WALL_SIZE)
            {
                this.location.X = 0 + WALL_SIZE;
                this.direction += 180;
                directionTimer.resetTimer(6);
            }
            else if (location.X > HolySplitGame.SCREEN_WIDTH - CHARACTER_SIZE - WALL_SIZE)
            {
                this.location.X = HolySplitGame.SCREEN_WIDTH - CHARACTER_SIZE - WALL_SIZE;
                this.direction += 180;
                directionTimer.resetTimer(6);
            }

            if (location.Y < 0 + WALL_SIZE)
            {
                this.location.Y = 0 + WALL_SIZE;
                this.direction += 180;
                directionTimer.resetTimer(6);
            }
            else if (location.Y > HolySplitGame.SCREEN_HEIGHT - CHARACTER_SIZE - WALL_SIZE)
            {
                this.location.Y = HolySplitGame.SCREEN_HEIGHT - CHARACTER_SIZE - WALL_SIZE;
                this.direction += 180;
                directionTimer.resetTimer(6);
            }

            this.hitbox.X = (int)this.location.X;
            this.hitbox.Y = (int)this.location.Y;
        }
    }
}
