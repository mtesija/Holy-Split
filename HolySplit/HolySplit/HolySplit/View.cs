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
    class View
    {
        private Texture2D player, blob, tiles, bullet;

        void LoadContent(ContentManager content)
        {
            player = content.Load<Texture2D>("images/player");
            blob = content.Load<Texture2D>("images/blob");
            tiles = content.Load<Texture2D>("images/tiles");
            bullet = content.Load<Texture2D>("images/bullet");
        }
    }
}
