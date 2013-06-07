using System;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.GUI.Elements
{
    public class Panel : Element
    {
        #region Constructor

        public Panel(int span, Layout layout, Element parent = null) : this(0,span,layout,parent){}
        public Panel(int offset, int span, Layout layout, Element parent = null)
        {
            Offset = offset;
            Span = span;
            Layout = layout;
            Parent = parent;
        }

        public Panel(Vector2 position, Vector2 size, Layout layout, Element parent = null)
        {
            Position = position;
            Size = size;
            Layout = layout;
            Parent = parent;
        }

        public Panel(Rectangle bounds, Layout layout, Element parent = null)
        {
            Bounds = bounds;
            Layout = layout;
            Parent = parent;
        }

        #endregion

        public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawRectangle(RenderBounds, Color.Blue);
            spriteBatch.DrawString(ContentHandler.Retrieve<SpriteFont>("DebugFont"), ID, RenderBounds.Location.ToVector2(), Color.Black);

            foreach (var child in Children)
                child.Render(spriteBatch, gameTime);
        }
    }
}
