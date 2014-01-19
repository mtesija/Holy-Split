using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HolySplit
{
    public class HolySplitGame : Microsoft.Xna.Framework.Game
    {
        private KeyboardState previousKeyboardState;

        public const int SCREEN_WIDTH = 700;
        public const int SCREEN_HEIGHT = 700;

        string scores;
        int highScore, finalScore;

        SpriteFont smallFont;
        SpriteFont largeFont;

        SoundEffect destroy, split, shoot, death;
        Song menuSong, gameSong, scoreSong;

        private Texture2D mainMenu;
        private Texture2D scoreScreen;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState
        {
            MainMenu,
            Game,
            ScoreScreen
        }

        Map map;
        View view;
        GameState gameState;

        public HolySplitGame()
        {
            previousKeyboardState = Keyboard.GetState();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            highScore = finalScore = 0;

            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT + PhysicsObject.CHARACTER_SIZE;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;

            view = new View();
            gameState = GameState.MainMenu;
            scores = String.Empty;

            SoundEffect.MasterVolume = 0.5f;
            MediaPlayer.IsRepeating = true;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            view.LoadContent(Content);
            smallFont = Content.Load<SpriteFont>("fonts/smallfont");
            largeFont = Content.Load<SpriteFont>("fonts/largefont");
            mainMenu = Content.Load<Texture2D>("images/mainmenu");
            destroy = Content.Load<SoundEffect>("sounds/explosion");
            split = Content.Load<SoundEffect>("sounds/split4");
            shoot = Content.Load<SoundEffect>("sounds/shoot");
            death = Content.Load<SoundEffect>("sounds/death");
            scoreScreen = Content.Load<Texture2D>("images/scorescreen");
            menuSong = Content.Load<Song>("music/thesplittening");
            gameSong = Content.Load<Song>("music/splittington");
            scoreSong = Content.Load<Song>("music/supersplitter");

            MediaPlayer.Play(menuSong);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState curKeyboard = Keyboard.GetState();

            if (gameState == GameState.MainMenu)
            {
                if (curKeyboard.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                {
                    MediaPlayer.Play(gameSong);
                    map = new Map(ref destroy, ref shoot, ref split);
                    gameState = GameState.Game;
                }
                else if (curKeyboard.IsKeyDown(Keys.Back) && previousKeyboardState.IsKeyUp(Keys.Back))
                {
                    gameState = GameState.ScoreScreen;
                    MediaPlayer.Play(scoreSong);
                }
                else if (curKeyboard.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
                {
                    Exit();
                }
            }
            else if (gameState == GameState.Game)
            {
                if (curKeyboard.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
                {
                    gameState = GameState.ScoreScreen;
                }
                map.Update(gameTime, ref death);
                if (map.player.destroyThis)
                {
                    gameState = GameState.ScoreScreen;
                    finalScore = 0;
                    scores = map.CalculateFinalScore(ref finalScore);
                    if (finalScore > highScore)
                    {
                        highScore = finalScore;
                    }
                    MediaPlayer.Play(scoreSong);
                }
            }
            else if (gameState == GameState.ScoreScreen)
            {
                if (curKeyboard.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                {
                    MediaPlayer.Play(menuSong);
                    gameState = GameState.MainMenu;
                }
            }
            previousKeyboardState = curKeyboard;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();

            if (gameState == GameState.MainMenu)
            {
                spriteBatch.Draw(mainMenu, new Vector2(0, 0), Color.White);
            }
            else if (gameState == GameState.Game)
            {
                view.Draw(spriteBatch, map);
            }
            else if (gameState == GameState.ScoreScreen)
            {
                spriteBatch.Draw(scoreScreen, new Vector2(0, 0), Color.White );
                spriteBatch.DrawString(smallFont, scores, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(largeFont, "YOUR SCORE: " + finalScore.ToString(), new Vector2(0, 400), Color.Red);
                spriteBatch.DrawString(largeFont, "HIGH SCORE: " + highScore.ToString(), new Vector2(0, 500), Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
