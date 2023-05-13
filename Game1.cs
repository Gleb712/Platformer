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

        private int collectedCoins;
        private int Deaths;

        public Vector2 _exitPosition;
        public static int ScreenWidth;
        public static int ScreenHeight;

        private Texture2D _backgroundSprite;
        private Texture2D _exitTexture;

        private Rectangle _backgroundRect;

        private Player player;
        private SpriteFont _font;

        public List<Trap> _traps;
        public List<Coin> _coins;
        public List<Platform> _platforms;

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

            _backgroundSprite = Content.Load<Texture2D>("background");
            _backgroundRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            _exitTexture = Content.Load<Texture2D>("exit");
            _exitPosition = new Vector2(730, 20);

            Texture2D platformTexture = Content.Load<Texture2D>("Platform");
            Texture2D playerTexture = Content.Load<Texture2D>("Player");
            _font = Content.Load<SpriteFont>("Font");

            // Добавление в список платформ
            _platforms = new List<Platform>
            {
                // Пол
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(0, 777)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(100, 777)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(200, 777)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(300, 777)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(400, 777)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(500, 777)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(600, 777)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(700, 777)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(800, 777)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(900, 777)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(1000, 777)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(1100, 777)),
                // Летающие платформы
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(1100, 480)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(1000, 620)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(850, 650)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(650, 550)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(500, 600)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(400, 550)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(50, 680)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(130, 525)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(300, 370)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(450, 250)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(700, 250)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(900, 150)),
                new Platform(Content.Load<Texture2D>("platform"), new Vector2(700, 80))
            };

            // Создаем экземпляр класса Player и передаем ему список платформ
            player = new Player(playerTexture, _platforms);

            // Добавление в список ловушек(шипов)
            _traps = new List<Trap>
            {
                // Правая часть платформы = X += 47, Y -= 10
                // Левая часть платформы = X не меняем, Y -= 10

                // Низ
                new Trap(Content.Load<Texture2D>("trap"), new Vector2(120, 767)),
                new Trap(Content.Load<Texture2D>("trap"), new Vector2(260, 767)),
                new Trap(Content.Load<Texture2D>("trap"), new Vector2(450, 767)),
                new Trap(Content.Load<Texture2D>("trap"), new Vector2(750, 767)),
                new Trap(Content.Load<Texture2D>("trap"), new Vector2(1050, 767)),
                // Остальное
                new Trap(Content.Load<Texture2D>("trap"), new Vector2(897, 640)),
                new Trap(Content.Load<Texture2D>("trap"), new Vector2(450, 540)),
                new Trap(Content.Load<Texture2D>("trap"), new Vector2(947, 140))
            };
            // Добавление в список монет
            _coins = new List<Coin>
            {
                // X += от 10 до 80, Y -= 40
                new Coin(Content.Load<Texture2D>("coin"), new Vector2(860, 610)),
                new Coin(Content.Load<Texture2D>("coin"), new Vector2(1150, 440)),
                new Coin(Content.Load<Texture2D>("coin"), new Vector2(1050, 580)),
                new Coin(Content.Load<Texture2D>("coin"), new Vector2(700, 510)),
                new Coin(Content.Load<Texture2D>("coin"), new Vector2(180, 485)),
                new Coin(Content.Load<Texture2D>("coin"), new Vector2(500, 210)),
                new Coin(Content.Load<Texture2D>("coin"), new Vector2(920, 110))
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime, player.playerRectangle);

            foreach (var trap in _traps)
            {
                trap.Update(gameTime, player.playerRectangle,player._playerPosition);
                player._playerPosition = trap.newPlayerPos;
                Deaths += trap.Deaths;
                trap.Deaths = 0;
            }

            foreach (var coin in _coins)
            {
                coin.Update(gameTime, player.playerRectangle);
                collectedCoins += coin.collectedCoins;
                coin.collectedCoins = 0;
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundSprite, _backgroundRect, Color.White);

            _spriteBatch.DrawString(_font, "Coins: " + collectedCoins.ToString(), new Vector2(920, 20), Color.Yellow);
            _spriteBatch.DrawString(_font, "Deaths: " + Deaths.ToString(), new Vector2(1050, 20), Color.Red);

            player.Draw(gameTime, _spriteBatch);

            foreach (var platform in _platforms)
            {
                platform.Draw(gameTime, _spriteBatch);
            }

            foreach (var trap in _traps)
            {
                trap.Draw(gameTime,_spriteBatch);
            }

            foreach (var coin in _coins)
            {
                coin.Draw(gameTime, _spriteBatch);
            }

            _spriteBatch.Draw(_exitTexture, _exitPosition, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}