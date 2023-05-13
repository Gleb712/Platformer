using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;

namespace Platformer
{
    public class Coin
    {
        Texture2D coinTexture;

        public int collectedCoins;

        public Vector2 coinPosition;

        public Coin(Texture2D coinTexture, Vector2 coinPosition)
        {
            this.coinTexture = coinTexture;
            this.coinPosition = coinPosition;
        }
        public void Update(GameTime gameTime, Rectangle playerRect)
        {
            if (playerRect.Intersects(new Rectangle((int)coinPosition.X, (int)coinPosition.Y, coinTexture.Width, coinTexture.Height)))
            {
                collectedCoins++;
                coinPosition = new Vector2(-100, -100);
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(coinTexture, coinPosition, Color.White);
        }
    }
}
