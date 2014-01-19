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
        const int DESTINATION_SIZE = 35;
        const int GUN_SIZE = 70;
        const int FONT_LOCATION = 40;
        const int AIM_SIZE = 25;

        private Texture2D player, blob, tiles, bullet, gun, select, aim;
        private SpriteFont largeFont;
        Rectangle sourceRect, gunLocation, selectLocation;
        Vector2 fontLocation;
        Timer animationTimer;
        int animationFrame;

        public View()
        {
            sourceRect = new Rectangle(0, 0, SOURCE_SIZE, SOURCE_SIZE);
            gunLocation = new Rectangle((HolySplitGame.SCREEN_WIDTH * 3 / 4) - (GUN_SIZE / 2) - GUN_SIZE, HolySplitGame.SCREEN_HEIGHT - GUN_SIZE / 2, GUN_SIZE, GUN_SIZE);
            fontLocation = new Vector2(FONT_LOCATION, HolySplitGame.SCREEN_HEIGHT - FONT_LOCATION);
            animationTimer = new Timer(0.2f);
            animationFrame = 0;
        }

        public void LoadContent(ContentManager content)
        {
            player = content.Load<Texture2D>("images/player");
            blob = content.Load<Texture2D>("images/blob");
            tiles = content.Load<Texture2D>("images/tiles");
            bullet = content.Load<Texture2D>("images/bullet");
            gun = content.Load<Texture2D>("images/gun");
            select = content.Load<Texture2D>("images/select");
            aim = content.Load<Texture2D>("images/aim");
            largeFont = content.Load<SpriteFont>("fonts/largefont");
        }

        public void Draw(SpriteBatch spriteBatch, Map map)
        {
            if (animationTimer.CheckTimer())
            {
                animationFrame++;
                if (animationFrame > 3)
                    animationFrame = 0;
            }

            for(int i = 0; i < Map.MAP_WIDTH; ++i)
                for(int j = 0; j < Map.MAP_HEIGHT; ++j)
                {
                    sourceRect.X = map.tiles[i, j].tileType * SOURCE_SIZE;
                    spriteBatch.Draw(tiles, new Rectangle(i * DESTINATION_SIZE, j * DESTINATION_SIZE, DESTINATION_SIZE, DESTINATION_SIZE), sourceRect, Color.White);
                }
            sourceRect.X = 1 * SOURCE_SIZE;
            for (int i = 0; i < Map.MAP_WIDTH; ++i)
            {
                spriteBatch.Draw(tiles, new Rectangle(i * DESTINATION_SIZE, HolySplitGame.SCREEN_HEIGHT, DESTINATION_SIZE, DESTINATION_SIZE), sourceRect, Color.White);
            }

            foreach (Blob b in map.blobs)
                spriteBatch.Draw(blob, b.hitbox,new Rectangle(animationFrame * SOURCE_SIZE, 0, SOURCE_SIZE, SOURCE_SIZE), b.color);

            foreach (Bullet b in map.bullets)
                spriteBatch.Draw(bullet, b.hitbox, b.color);

            spriteBatch.Draw(aim, new Rectangle((int)map.player.aim.X, (int)map.player.aim.Y, AIM_SIZE, AIM_SIZE), map.player.color);
            spriteBatch.Draw(player, map.player.hitbox, new Rectangle(animationFrame * SOURCE_SIZE, 0, SOURCE_SIZE, SOURCE_SIZE), map.player.color);

            spriteBatch.Draw(select, gunLocation, Color.DarkGray);
            spriteBatch.Draw(gun, gunLocation, map.player.RED);
            gunLocation.X += GUN_SIZE;
            spriteBatch.Draw(select, gunLocation, Color.DarkGray);
            spriteBatch.Draw(gun, gunLocation, map.player.YELLOW);
            gunLocation.X += GUN_SIZE;
            spriteBatch.Draw(select, gunLocation, Color.DarkGray);
            spriteBatch.Draw(gun, gunLocation, map.player.BLUE);
            gunLocation.X -= (GUN_SIZE * 2);

            selectLocation = new Rectangle(gunLocation.X + (map.player.selectedWeapon * GUN_SIZE), gunLocation.Y, GUN_SIZE, GUN_SIZE);
            spriteBatch.Draw(select, selectLocation, Color.White);
            spriteBatch.Draw(gun, selectLocation, map.player.color);

            spriteBatch.DrawString(largeFont, "Kills: " + map.score.enemiesKilled.ToString(), fontLocation, Color.White);
        }
    }
}
