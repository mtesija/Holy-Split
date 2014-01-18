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
    struct Tile
    {
        public int tileType; //0 for wall, 1 for empty
    }

    class Map
    {
        public const int MAP_WIDTH = 20;
        public const int MAP_HEIGHT = 20;

        public Tile[,] tiles;
        public Player player;
        public List<Blob> blobs, newBlobs;
        public List<Bullet> bullets;
        public static Random random;
        public int score;

        public Map()
        {
            tiles = new Tile[MAP_WIDTH, MAP_HEIGHT];
            for (int i = 0; i < MAP_WIDTH; ++i)
            {
                for (int j = 0; j < MAP_HEIGHT; ++j)
                {
                    tiles[i, j] = new Tile();
                    tiles[i, j].tileType = 0;
                    if (i == 0 || j == 0 || i == MAP_WIDTH - 1 || j == MAP_HEIGHT - 1)
                        tiles[i, j].tileType = 1;
                }
            }
            player = new Player(new Vector2(HolySplitGame.SCREEN_WIDTH / 2, HolySplitGame.SCREEN_HEIGHT * 3 / 4));
            blobs = new List<Blob>();
            newBlobs = new List<Blob>();
            bullets = new List<Bullet>();
            random = new Random();

            blobs.Add(new Blob(new Vector2(HolySplitGame.SCREEN_WIDTH / 2, HolySplitGame.SCREEN_HEIGHT / 5), Color.Gray, 1));
            blobs.Add(new Blob(new Vector2(HolySplitGame.SCREEN_WIDTH / 3, HolySplitGame.SCREEN_HEIGHT / 4), Color.Gray, 1));
            blobs.Add(new Blob(new Vector2(HolySplitGame.SCREEN_WIDTH * 2 / 3, HolySplitGame.SCREEN_HEIGHT / 4), Color.Gray, 1));
            blobs.Add(new Blob(new Vector2(HolySplitGame.SCREEN_WIDTH * 3 / 4, HolySplitGame.SCREEN_HEIGHT / 3), Color.Gray, 1));
            blobs.Add(new Blob(new Vector2(HolySplitGame.SCREEN_WIDTH / 4, HolySplitGame.SCREEN_HEIGHT / 3), Color.Gray, 1));
            //for (int i = 0; i < 15; ++i)//TESTCODE
            //{
            //    blobs.Add( new Blob(new Vector2(random.Next(0, HolySplitGame.SCREEN_WIDTH - 50), random.Next(0, HolySplitGame.SCREEN_HEIGHT - 50)), Color.Gray, 1));
            //}//TESTCODE
        }

        public string CalculateFinalScore()
        {
            return "";
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime, ref bullets);
            foreach (Blob b in blobs)
            {
                b.Update(gameTime, ref player, ref newBlobs);
                //player.Collide(b);
            }
            foreach (Blob b in newBlobs)
                blobs.Add(b);
            newBlobs.Clear();
            for (int i = 0; i < blobs.Count; ++i)
                if (blobs[i].destroyThis)
                {
                    blobs.RemoveAt(i);
                    --i;
                }
            for(int i = blobs.Count - 1; i >= 0; --i)
                for (int j = 0; j < i; ++j)
                {
                    blobs[i].Collide(blobs[j]);
                }
        }
    }
}
