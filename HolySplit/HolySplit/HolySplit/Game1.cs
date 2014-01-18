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
        public const int SCREEN_WIDTH = 700;
        public const int SCREEN_HEIGHT = 700;

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
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;

            map = new Map();
            view = new View();
            gameState = GameState.Game;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            view.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameState == GameState.MainMenu)
            {
            }
            else if (gameState == GameState.Game)
            {
                map.Update(gameTime);
            }
            else if (gameState == GameState.ScoreScreen)
            {
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Orange);

            if (gameState == GameState.MainMenu)
            {
            }
            else if (gameState == GameState.Game)
            {
                view.Draw(spriteBatch, map);
            }
            else if (gameState == GameState.ScoreScreen)
            {
            }
            
            base.Draw(gameTime);
        }
    }
}
