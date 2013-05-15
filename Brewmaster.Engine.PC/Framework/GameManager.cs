using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.Framework
{
    public abstract class GameManager
    {
        public bool DebugMode { get; set; }

        protected SpriteBatch spriteBatch
        {
            get { return CurrentGame.SpriteBatch; }
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void Unload();
    }
}
