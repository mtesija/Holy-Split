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
        //Color
        public Color GRAY = Color.Gray;
        //Primary
        public Color RED = Color.Red;
        public Color BLUE = Color.Blue;
        public Color YELLOW = Color.Yellow;
        //Secondary
        public Color PURPLE = Color.DarkViolet;
        public Color GREEN = Color.ForestGreen;
        public Color ORANGE = Color.DarkOrange;
        //Tertiary
        public Color REDORANGE = Color.OrangeRed;
        public Color REDPURPLE = Color.MediumVioletRed;
        public Color BLUEPURPLE = Color.BlueViolet;
        public Color BLUEGREEN = Color.MediumTurquoise;
        public Color YELLOWGREEN = Color.YellowGreen;
        public Color YELLOWORANGE = Color.Gold;

        public const int CHARACTER_SIZE = 35;
        public const int WALL_SIZE = 35;
        public const int PLAYER_SPEED = 3;
        public const int BULLET_SPEED = 6;

        public Rectangle hitbox;
        public Vector2 velocity;
        public Vector2 location;
        public Color color;
        public float speed;
        public bool destroyThis;
    }
}
