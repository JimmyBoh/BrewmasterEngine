using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.GUI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Windows.Data.Xml.Dom;

namespace BrewmasterEngine.GUI
{
    public abstract class Element
    {
        #region Constructor

        protected Element()
        {
            Position = Vector2.Zero;
            Size = Vector2.Zero;

            Layout = Layout.Absolute;
            Span = 1;
            Offset = 0;

            Parent = null;
            Children = new List<Element>();
            spanCount = 0;
        }

        #endregion

        #region Properties

        public string ID { get; set; }

        public Layout Layout { get; set; }
        public Element Parent { get; set; }
        public List<Element> Children { get; set; }

        public Rectangle Bounds { get; set; }
        public Vector2 Position
        {
            get { return Bounds.Location.ToVector2(); }
            set { Bounds = new Rectangle((int) value.X, (int) value.Y, Bounds.Width, Bounds.Height); }
        }
        public Vector2 Size
        {
            get { return Bounds.GetSize(); }
            set { Bounds = new Rectangle(Bounds.X, Bounds.Y, (int) value.X, (int) value.Y); }
        }
        public Rectangle RenderBounds
        {
            get
            {
                return Parent == null ? Bounds : new Rectangle(Parent.RenderBounds.X + Bounds.X, Parent.RenderBounds.Y + Bounds.Y, Bounds.Width, Bounds.Height);
            }
        }

        public int Span { get; set; }
        public int Offset { get; set; }

        protected int spanCount;
        protected int spanHeight
        {
            get { return Bounds.Height / spanCount; }
        }

        protected int spanWidth
        {
            get { return Bounds.Width / spanCount; }
        }

        #endregion

        #region Methods

        #region Reflow

        public void Reflow()
        {
            switch (Layout)
            {
                case Layout.Layered:
                    reflowLayered();
                    break;
                case Layout.Vertical:
                    reflowVertical();
                    break;
                case Layout.Horizontal:
                    reflowHorizontal();
                    break;
                default:
                    reflowAbsolute();
                    break;
            }
        }

        private void reflowLayered()
        {
            foreach (var child in Children)
            {
                child.CenterOn(this);

                child.Reflow();
            }
        }

        private void reflowVertical()
        {
            var childY = 0;

            foreach (var child in Children)
            {
                childY += child.Offset*spanHeight;
                child.Bounds = new Rectangle(0, childY, Bounds.Width, child.Span*spanHeight);
                childY += child.Span*spanHeight;

                child.Reflow();
            }
        }

        private void reflowHorizontal()
        {
            var childX = 0;

            foreach (var child in Children)
            {
                childX += child.Offset*spanWidth;
                child.Bounds = new Rectangle(childX, 0, child.Span*spanWidth, Bounds.Height);
                childX += child.Span*spanWidth;

                child.Reflow();
            }
        }

        private void reflowAbsolute()
        {
            foreach (var child in Children)
                child.Reflow();
        }

        #endregion

        #region Hierarchy

        public Element AddChild(Element element, Action<Element> callback = null)
        {
            element.Parent = this;
            element.ID = ID + "_" + Children.Count;
            
            Children.Add(element);
            spanCount += element.Offset + element.Span;

            Reflow();

            if (callback != null) callback(element);

            return this;
        }

        public Element AddPanel(int span, Layout layout = Layout.Layered, Action<Element> callback = null)
        {
            return AddChild(new Panel(span, layout), callback);
        }

        public Element AddPanel(int offset, int span, Layout layout = Layout.Layered, Action<Element> callback = null)
        {
            return AddChild(new Panel(offset, span,layout), callback);
        }

        #endregion

        #region Game

        public void Update(GameTime gameTime)
        {
            foreach (var child in Children)
                child.Update(gameTime);
        }

        public abstract void Render(SpriteBatch spriteBatch, GameTime gameTime);

        #endregion

        #region Helpers

        public void CenterOn(Element element)
        {
            CenterOn(element.Bounds.Center.ToVector2());
        }

        public void CenterOn(Vector2 center)
        {
            var posX = center.X - (Size.X/2);
            var posY = center.Y - (Size.Y/2);
            Position = new Vector2(posX, posY);
        }

        #endregion

        #endregion
    }
}
