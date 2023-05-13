using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace Platformer
{
    public class Player
    {
        public Texture2D _playerTexture;

        private Vector2 playerVelocity;
        public Vector2 _playerPosition;

        private float jumpVelocity = -18f; // Скорость прыжка
        private float playerSpeed = 5f; // Скорость персонажа
        private float gravity = 0.5f; // Сила гравитации
        private float jumpDuration = 1000; // Длительность прыжка в миллисекундах
        private float jumpTimer = 0;

        private bool onGround;
        private bool isJumping;
        public bool OnTop;
        public bool OnOther;

        private List<Platform> platforms;

        public Player(Texture2D playerTexture, List<Platform> platforms)
        {
            _playerTexture = playerTexture;
            this.platforms = platforms;
            _playerPosition = new Vector2(1150, 710); // Начальная позиция игрока
        }

        public Rectangle playerRectangle
        {
            get
            {
                return new Rectangle((int)_playerPosition.X, (int)_playerPosition.Y, _playerTexture.Width, _playerTexture.Height);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerTexture, _playerPosition, Color.White);
        }

        public void Update(GameTime gameTime, Rectangle playerRect)
        {
            // Коллизия с платформами
            if (platforms.Count > 0)
            {
                onGround = false;

                foreach (var platform in platforms)
                {
                    // Проверяем, пересекаются ли прямоугольники игрока и платформы
                    if (playerRectangle.Intersects(platform.PlatformRectangle))
                    {
                        // Определяем сторону, с которой находится игрок
                        float overlapLeft = playerRectangle.Right - platform.PlatformRectangle.Left;
                        float overlapRight = platform.PlatformRectangle.Right - playerRectangle.Left;
                        float overlapTop = playerRectangle.Bottom - platform.PlatformRectangle.Top;
                        float overlapBottom = platform.PlatformRectangle.Bottom - playerRectangle.Top;

                        float minOverlap = new List<float>() { overlapLeft, overlapRight, overlapTop, overlapBottom }.Min();

                        // Обновляем позицию игрока так, чтобы он не проходил сквозь платформу
                        if (minOverlap == overlapLeft)
                        {
                            _playerPosition.X -= minOverlap;
                        }
                        else if (minOverlap == overlapRight)
                        {
                            _playerPosition.X += minOverlap;
                        }
                        else if (minOverlap == overlapBottom)
                        {
                            onGround |= true;
                            playerVelocity.Y = 0;
                        }
                        else if (minOverlap == overlapTop)
                        {
                            playerVelocity.Y = 0;
                            _playerPosition.Y -= minOverlap;
                        }
                    }
                }
                if (!onGround)
                {
                    // Если игрок не стоит на земле, то применяем гравитацию
                    playerVelocity.Y += gravity;
                }
            }

            if (_playerPosition.Y + _playerTexture.Height >= 800) // Проверка на соприкосновение с нижней границей окна
            {
                _playerPosition = new Vector2(1150, 710);
            }

            // Ограничение движения персонажа у правой и левой границ окна
            _playerPosition.X = MathHelper.Clamp(_playerPosition.X, 0, 1200 - _playerTexture.Width);

            // Ограничение движения персонажа у верхней границы окна
            _playerPosition.Y = MathHelper.Clamp(_playerPosition.Y, 0, 800 - _playerTexture.Height);

            //Движение в стороны
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A))
            {
                playerVelocity.X = -playerSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                playerVelocity.X = playerSpeed;
            }
            else
            {
                playerVelocity.X = 0f;
            }

            // Обработка прыжка игрока
            if ((keyboardState.IsKeyDown(Keys.W) && isJumping == false))
            {
                isJumping = true;
                playerVelocity.Y = jumpVelocity;
                jumpTimer = 0;
            }
            if (isJumping)
            {
                jumpTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (jumpTimer > jumpDuration)
                {
                    isJumping = false;
                }
                else
                {
                    playerVelocity.Y += gravity; // Симуляция гравитации
                }
            }

            // Обновление положения игрока
            _playerPosition += playerVelocity;
        }
    }
}