using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Platformer
{
    public class Platform
    {
        public Texture2D _platformTexture;

        public Vector2 _platformPosition;

        public Rectangle PlayerRect;

        public Platform(Texture2D platformTexture, Vector2 platformPosition)
        {
            _platformTexture = platformTexture;
            _platformPosition = platformPosition;
        }

        public Rectangle PlatformRectangle
        {
            get
            {
                return new Rectangle((int)_platformPosition.X, (int)_platformPosition.Y, _platformTexture.Width, _platformTexture.Height);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_platformTexture, _platformPosition, Color.White);
        }
    }
}