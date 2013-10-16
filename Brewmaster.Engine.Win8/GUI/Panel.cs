using System;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.GUI
{
    public abstract class Panel : Element
    {
        #region Constructor

        protected Panel(int span, Element parent = null) : this(0,span,parent){}
        protected Panel(int preOffset, int span, Element parent = null):this(preOffset,span, 0, parent){}
        protected Panel(int preOffset, int span,int postOffset, Element parent = null)
        {
            PreOffset = preOffset;
            PostOffset = postOffset;
            Span = span;
            Parent = parent;
        }

        #endregion

        #region Properties

        

        #endregion

        #region Fields

        protected readonly Color color = Color.White.Randomize();

        #endregion

        #region Methods

        public override void Draw(GameTime gameTime)
        {
            var b = RenderBounds;
            var v = -10;// *RelativeLayer;
            //b.Inflate(v, v);
            //spriteBatch.DrawRectangle(b, Color.Purple*0.5f,3);

            //spriteBatch.DrawString(ContentHandler.Retrieve<SpriteFont>("DebugFont"), ID, b.Location.ToVector2(), Color.White);

            foreach (var child in Children)
                child.Draw(gameTime);
        }

        #endregion

    }
}
