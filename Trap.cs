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
    public class Trap
    {
        Texture2D trapTexture;

        Vector2 trapPosition;

        public Vector2 newPlayerPos;

        public int Deaths;

        public Trap(Texture2D trapTexture, Vector2 trapPosition)
        {
            this.trapTexture = trapTexture;
            this.trapPosition = trapPosition;
        }

        public void Update(GameTime gameTime,Rectangle playerRect,Vector2 playerPosition)
        {
            if (playerRect.Intersects(new Rectangle((int)trapPosition.X, (int)trapPosition.Y, trapTexture.Width, trapTexture.Height)))
            {
                Deaths++;
                newPlayerPos = new Vector2(1150, 710);
            }
            else newPlayerPos = playerPosition; // При соприкосновении с ловушкой, возвращаем игрока в начальнуюю точку
        }
        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(trapTexture, trapPosition, Color.White);
        }


    }
}
