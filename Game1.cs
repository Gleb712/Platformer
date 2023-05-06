using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platformer
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        private Texture2D _backgroundSprite;
        private Rectangle _backgroundRect;

        private Player _player;

        private List<Platform> _platforms;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player = new Player(Content.Load<Texture2D>("player"));
            _player.playerPosition = new Vector2(600,800);

            _backgroundSprite = Content.Load<Texture2D>("background");
            _backgroundRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            // Список платформ
            _platforms = new List<Platform>
            {
                new Platform(Content.Load<Texture2D>("platform"),new Vector2(600,600))
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime);
            

            foreach (var platform in _platforms)
            {
                platform.PlayerRect = _player.Rectangle;
                platform.Update(gameTime);

                _player.OnTop = platform.IsPlayerOnTop;
                _player.OnOther = platform.IsPlayerOnOtherSide;
            }

            

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundSprite, _backgroundRect, Color.White);

            _player.Draw(gameTime, _spriteBatch);

            foreach (var platform in _platforms)
            {
                platform.Draw(gameTime, _spriteBatch);
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}