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
        private bool bResetMap;

        public const int SCREEN_WIDTH = 700;
        public const int SCREEN_HEIGHT = 700;

        const string GAME_TITLE = "Holy Split!";
        const string CONTROLS = "WASD or Arrow Keys - Movement\nMouse - Aim\nLeft Click - Shoot\n1,2,3 or Scroll Wheel - Switch Weapon";
        const string HOW_TO_PLAY = "    You are equipped with the finest Ultratech(tm) Laser with three stellar blaster settings.\nThe evil Queen Blobulon is invading your planet with his ugly minions.\nHer minions separate when shot unless you match your blaster color setting to the color of the blob.\nShooting a blob with the wrong color setting will cause them to seperate further (note there are more blob colors than blaster settings so you will have to split them).\nYour score increases based on how long you survive and how many blobs you kill so it will be beneficial to shoot some with the wrong color to prolong the game!";
        const string CONTINUE = "Press SPACE to continue";
        const string HIGH_SCORE = "HIGH SCORES:";
        string scores;

        SpriteFont smallFont;
        SpriteFont largeFont;

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

            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT + PhysicsObject.CHARACTER_SIZE;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;

            view = new View();
            gameState = GameState.MainMenu;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            view.LoadContent(Content);
            smallFont = Content.Load<SpriteFont>("fonts/smallfont");
            largeFont = Content.Load<SpriteFont>("fonts/largefont");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState curKeyboard = Keyboard.GetState();

            if (gameState == GameState.MainMenu)
            {
                if (curKeyboard.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                {
                    map = new Map();
                    gameState = GameState.Game;
                }
                else if (curKeyboard.IsKeyDown(Keys.Back) && previousKeyboardState.IsKeyUp(Keys.Back))
                {
                    gameState = GameState.ScoreScreen;
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
                map.Update(gameTime);
                if (map.player.destroyThis)
                    gameState = GameState.ScoreScreen;
            }
            else if (gameState == GameState.ScoreScreen)
            {
                if (curKeyboard.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                {
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
                spriteBatch.DrawString(largeFont, GAME_TITLE, new Vector2(0, 0), Color.Black);
                spriteBatch.DrawString(smallFont, HOW_TO_PLAY, new Vector2(0, 70), Color.Black);
                spriteBatch.DrawString(smallFont, CONTROLS, new Vector2(0, 200), Color.Black);
                spriteBatch.DrawString(smallFont, CONTINUE, new Vector2(0, 400), Color.Black);
            }
            else if (gameState == GameState.Game)
            {
                view.Draw(spriteBatch, map);
            }
            else if (gameState == GameState.ScoreScreen)
            {
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
