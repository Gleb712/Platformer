using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;

namespace Platformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _backgroundSprite;
        private Rectangle _backgroundRect;

        private Texture2D _playerSprite;
        private Vector2 _playerPosition;
        private float _playerSpeed;

        private Texture2D _platformSprite;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 800;
        }

        protected override void Initialize()
        {
            _playerPosition = new Vector2(350, 600);
            _playerSpeed = 5f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _playerSprite = Content.Load<Texture2D>("player");
            _platformSprite = Content.Load<Texture2D>("platform");

            _backgroundSprite = Content.Load<Texture2D>("background");
            _backgroundRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Move player left
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _playerPosition.X -= _playerSpeed;
            }

            // Move player right
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _playerPosition.X += _playerSpeed;
            }

            // Move player up
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _playerPosition.Y -= _playerSpeed;
            }

            // Move player down
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _playerPosition.Y += _playerSpeed;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundSprite, _backgroundRect, Color.White);
            _spriteBatch.End();

            _spriteBatch.Begin();
            _spriteBatch.Draw(_playerSprite, _playerPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}