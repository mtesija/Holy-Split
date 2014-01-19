using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        int highScore, finalScore;

        SpriteFont smallFont, mediumFont, largeFont;

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

            SoundEffect.MasterVolume = 0.5f;
            MediaPlayer.IsRepeating = true;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            view.LoadContent(Content);
            smallFont = Content.Load<SpriteFont>("fonts/smallfont");
            mediumFont = Content.Load<SpriteFont>("fonts/mediumFont");
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
                    finalScore = map.CalculateFinalScore();
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
                DrawScores();
                DrawStringCentered(largeFont, "YOUR SCORE: " + finalScore.ToString(), 375, Color.Red);
                DrawStringCentered(largeFont, "HIGH SCORE: " + highScore.ToString(), 475, Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawStringCentered(SpriteFont spriteFont, String text, Single y, Color color)
        {
            Vector2 textBounds = spriteFont.MeasureString(text);
            Single centerX = spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.5f - textBounds.X * 0.5f;

            spriteBatch.DrawString(spriteFont, text, new Vector2(centerX, y), color);
        }

        public void DrawScores()
        {
            DrawStringCentered(mediumFont, "Number of enemies killed: " + map.score.enemiesKilled.ToString(), 150, Color.White);
            DrawStringCentered(mediumFont, "Time survived: " + map.score.timeSurvived.ToString() + " seconds", 180, Color.White);
            DrawStringCentered(mediumFont, "Number of enemy splits: " + map.score.numberSplits.ToString(), 210, Color.White);
            DrawStringCentered(mediumFont, "Max number of living enemies: " + map.score.mostEnemiesAlive.ToString(), 240, Color.White);
            if(map.score.eradication)
                DrawStringCentered(mediumFont, "Eradication Bonus!", 270, Color.White);
        }
    }
}
