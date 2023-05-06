using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Platformer
{
    public class Platform
    {
        public Texture2D PlatformTexture;
        public Vector2 PlatformPosition;
        public Rectangle PlayerRect;

        public bool IsPlayerOnTop;
        public bool IsPlayerOnOtherSide;

        public Platform(Texture2D texture,Vector2 vector )
        {
            PlatformTexture = texture;
            PlatformPosition = vector;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
           spriteBatch.Draw(PlatformTexture, PlatformPosition,Color.White);
        }

        public void Update(GameTime gameTime)
        {
            IsPlayerOnTop = IsPlayerOnOtherSide = false;
            if (PlayerRect.Intersects(new Rectangle((int)PlatformPosition.X, (int)PlatformPosition.Y, PlatformTexture.Width,1)))
            {
                IsPlayerOnTop = true;
            }

            if (PlayerRect.Intersects(new Rectangle((int)PlatformPosition.X, (int)PlatformPosition.Y, 1, PlatformTexture.Height)))
            {
                IsPlayerOnOtherSide = true;
            }

            if (PlayerRect.Intersects(new Rectangle((int)PlatformPosition.X + PlatformTexture.Width, (int)PlatformPosition.Y, 1, PlatformTexture.Height)))
            {
                IsPlayerOnOtherSide = true;
            }

            if (PlayerRect.Intersects(new Rectangle((int)PlatformPosition.X, (int)PlatformPosition.Y + PlatformTexture.Height, PlatformTexture.Width, 1)))
            {
                IsPlayerOnOtherSide = true;
            }
        }


    }
}