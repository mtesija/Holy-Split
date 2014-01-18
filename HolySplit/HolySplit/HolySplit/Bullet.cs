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
    class Bullet : PhysicsObject
    {
        public Bullet(Vector2 location)
        {
            speed = BULLET_SPEED;
            destroyThis = false;
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
