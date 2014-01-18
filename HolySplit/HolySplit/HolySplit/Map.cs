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
        public Random random;

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
            player = new Player(new Vector2(400, 400));
            blobs = new List<Blob>();
            newBlobs = new List<Blob>();
            bullets = new List<Bullet>();
            random = new Random();

            blobs.Add(new Blob(new Vector2(100, 100), Color.Gray, 1));
            for (int i = 0; i < 100; ++i)//TESTCODE
            {
                blobs.Add( new Blob(new Vector2(10*i, 10*i), Color.Gray, 1));
            }//TESTCODE
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime, ref bullets);
            foreach (Blob b in blobs)
                b.Update(gameTime, ref player, ref random, ref newBlobs);
            foreach (Blob b in newBlobs)
                blobs.Add(b);
            newBlobs.Clear();
            for (int i = 0; i < blobs.Count; ++i)
                if (blobs[i].destroyThis)
                {
                    blobs.RemoveAt(i);
                    --i;
                }
        }
    }
}
