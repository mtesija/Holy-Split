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
    class PhysicsObject
    {
        public const int CHARACTER_SIZE = 20;

        public Rectangle hitbox;
        public Vector2 velocity;
        public Vector2 location;
        public Color color;
        public int speed;

    }
}
