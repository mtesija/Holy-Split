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
        const int SOURCE_SIZE = 100;
        const int DESTINATION_SIZE = 20;

        private Texture2D player, blob, tiles, bullet;
        Rectangle sourceRect;

        public View()
        {
            sourceRect = new Rectangle(0, 0, SOURCE_SIZE, SOURCE_SIZE);
        }

        public void LoadContent(ContentManager content)
        {
            player = content.Load<Texture2D>("images/player");
            blob = content.Load<Texture2D>("images/blob");
            tiles = content.Load<Texture2D>("images/tiles");
            bullet = content.Load<Texture2D>("images/bullet");
        }

        public void Draw(SpriteBatch spriteBatch, Map map)
        {
            spriteBatch.Begin();

            for(int i = 0; i < Map.MAP_WIDTH; ++i)
                for(int j = 0; j < Map.MAP_HEIGHT; ++j)
                {
                    sourceRect.X = map.tiles[i, j].tileType * SOURCE_SIZE;
                    spriteBatch.Draw(tiles, new Rectangle(i * DESTINATION_SIZE, j * DESTINATION_SIZE, DESTINATION_SIZE, DESTINATION_SIZE), sourceRect, Color.White);
                }

            spriteBatch.End();
        }
    }
}
