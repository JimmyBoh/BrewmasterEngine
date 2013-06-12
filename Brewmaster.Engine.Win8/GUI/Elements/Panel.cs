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

        public Panel(int span, LayoutStyle layoutStyle, Element parent = null) : this(0,span,layoutStyle,parent){}
        public Panel(int offset, int span, LayoutStyle layoutStyle, Element parent = null)
        {
            Offset = offset;
            Span = span;
            LayoutStyle = layoutStyle;
            Parent = parent;
        }

        public Panel(Vector2 position, Vector2 size, LayoutStyle layoutStyle, Element parent = null)
        {
            Position = position;
            Size = size;
            LayoutStyle = layoutStyle;
            Parent = parent;
        }

        public Panel(Rectangle bounds, LayoutStyle layoutStyle, Element parent = null)
        {
            Bounds = bounds;
            LayoutStyle = layoutStyle;
            Parent = parent;
        }

        #endregion

        #region Properties

        private readonly Color color = Color.White.Randomize();

        #endregion

        public override void Draw(GameTime gameTime)
        {
            //spriteBatch.DrawRectangle(RenderBounds, color);

            foreach (var child in Children)
                child.Draw(gameTime);
        }
    }
}
