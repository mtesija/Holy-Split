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

    struct Score
    {
        public int timeSurvived;
        public int enemiesKilled;
        public int mostEnemiesAlive;
        public int numberSplits;
        public bool eradication;
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
        public Score score;
        public DateTime startTime;

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

            score = new Score();
            score.enemiesKilled = 0;
            score.eradication = false;
            score.mostEnemiesAlive = 1;
            score.timeSurvived = 0;
            score.numberSplits = 0;

            startTime = DateTime.Now;

            blobs.Add(new Blob(new Vector2(HolySplitGame.SCREEN_WIDTH / 2, HolySplitGame.SCREEN_HEIGHT / 5), Color.Gray, 1.3f));

            //blobs.Add(new Blob(new Vector2(HolySplitGame.SCREEN_WIDTH / 3, HolySplitGame.SCREEN_HEIGHT / 4), Color.Gray, .7f + (float)random.NextDouble() * .6f));
            //blobs.Add(new Blob(new Vector2(HolySplitGame.SCREEN_WIDTH * 2 / 3, HolySplitGame.SCREEN_HEIGHT / 4), Color.Gray, .7f + (float)random.NextDouble() * .6f));
            //blobs.Add(new Blob(new Vector2(HolySplitGame.SCREEN_WIDTH * 3 / 4, HolySplitGame.SCREEN_HEIGHT / 3), Color.Gray, .7f + (float)random.NextDouble() * .6f));
            //blobs.Add(new Blob(new Vector2(HolySplitGame.SCREEN_WIDTH / 4, HolySplitGame.SCREEN_HEIGHT / 3), Color.Gray, .7f + (float)random.NextDouble() * .6f));
        }

        public string CalculateFinalScore(ref int finalScore)
        {
            finalScore = (score.enemiesKilled * 100) - (score.numberSplits) + (score.mostEnemiesAlive * 10) + (score.timeSurvived * 100);
            if (score.eradication)
                finalScore *= 2;
            return "Number of enemies killed: " + score.enemiesKilled.ToString() + '\n'
                +  "Time survived: " + score.timeSurvived.ToString() + '\n'
                +  "Number of enemy splits: " + score.numberSplits.ToString() + '\n'
                +  "Max number of living enemies: " + score.mostEnemiesAlive.ToString() + '\n'
                +  "Eradication bonus (2X score): " + score.eradication.ToString() + '\n'
                +  "FINAL SCORE: " + finalScore.ToString();
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime, ref bullets);
            foreach (Bullet b in bullets)
                b.Update(gameTime);
            foreach (Blob b in blobs)
            {
                b.Update(gameTime, ref player, ref newBlobs);
                for (int i = 0; i < bullets.Count; ++i)
                {
                    b.BulletCollide(bullets[i], ref newBlobs);
                    if (bullets[i].destroyThis)
                    {
                        bullets.RemoveAt(i);
                        --i;
                    }
                }
                player.Collide(b);
            }
            foreach (Blob b in newBlobs)
            {
                score.numberSplits++;
                blobs.Add(b);
            }
            newBlobs.Clear();
            for (int i = 0; i < blobs.Count; ++i)
                if (blobs[i].destroyThis)
                {
                    blobs.RemoveAt(i);
                    --i;
                    score.enemiesKilled++;
                }
            for(int i = blobs.Count - 1; i >= 0; --i)
                for (int j = 0; j < i; ++j)
                {
                    blobs[i].Collide(blobs[j]);
                }

            if (blobs.Count > score.mostEnemiesAlive)
                score.mostEnemiesAlive = blobs.Count;
            if (blobs.Count == 0)
            {
                score.eradication = true;
                player.destroyThis = true;
            }
            if (player.destroyThis)
            {
                TimeSpan t = DateTime.Now - startTime;
                score.timeSurvived = t.Seconds;
            }
        }
    }
}
