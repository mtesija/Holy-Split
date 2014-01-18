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
    class Blob : Character
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

        void Split(ref List<Blob> blobs, ref Random random)
        {
            random.Next(7);
        }

        void Update(GameTime gameTime, ref Player player)
        {
            if (color == RED)
            {
                this.velocity = new Vector2(player.location.X - this.location.X, player.location.Y - this.location.Y);
            }
            else if (color == BLUE)
            {
            }

            location.X += velocity.X;
            location.Y += velocity.Y;

            hitbox.X = (int)location.X;
            hitbox.Y = (int)location.Y;
        }
    }
}
