using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Platformer
{
    public class Player
    {
        public Texture2D playerTexture;
        private Vector2 playerVelocity;
        public Vector2 playerPosition;
        private float jumpVelocity = -18f; // Скорость прыжка
        private float playerSpeed = 5f; // Скорость персонажа
        private bool isJumping;
        private float jumpDuration = 1000; // Длительность прыжка в миллисекундах
        private float jumpTimer = 0;

        public bool OnTop;
        public bool OnOther;

        public Player(Texture2D texture)
        {
            playerTexture = texture;
        }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerTexture.Width, playerTexture.Height);
            }

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, playerPosition, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            // Коллизия игрока с платформой
            if (OnTop)
            {
                playerVelocity = new Vector2(0, 0);

            }
            if (OnOther)
            {
                playerVelocity = new Vector2(0, 0);
                playerVelocity.Y += 0.6f; // Симуляция гравитации

            }

            // Ограничение движения персонажа по горизонтали
            playerPosition.X = MathHelper.Clamp(playerPosition.X, 0, 1200 - playerTexture.Width);

            // Ограничение движения персонажа по вертикали
            playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, 800 - playerTexture.Height);

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
                    playerVelocity.Y += 0.6f; // Симуляция гравитации
                }
            }

            // Обновление положения игрока
            playerPosition += playerVelocity;

        }
    }
}   
