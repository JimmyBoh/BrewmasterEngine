using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.DataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace BrewmasterEngine.GUI
{
    public abstract class Element : GameObject
    {
        #region Constructor

        protected Element()
        {
            Position = Vector2.Zero;
            Size = Vector2.Zero;
            reflowScale = 1f;

            LayoutStyle = LayoutStyle.Absolute;
            Span = 1;
            PreOffset = 0;

            Parent = null;
            Children = new List<Element>();
            spanCount = 0;
        }

        #endregion

        #region Properties

        public string ID { get; set; }

        public LayoutStyle LayoutStyle { get; set; }
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

        protected float reflowScale;

        public int PreOffset { get; set; }
        public int Span { get; set; }
        public int PostOffset { get; set; }
        

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

        public virtual void Reflow()
        {
            switch (LayoutStyle)
            {
                case LayoutStyle.Layered:
                    reflowLayered();
                    break;
                case LayoutStyle.Vertical:
                    reflowVertical();
                    break;
                case LayoutStyle.Horizontal:
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
                child.Reflow();
            }
        }

        private void reflowVertical()
        {
            var childY = 0;

            foreach (var child in Children)
            {
                childY += child.PreOffset*spanHeight;
                child.Bounds = new Rectangle(0, childY, Bounds.Width, child.Span*spanHeight);
                childY += (child.Span+child.PostOffset)*spanHeight;

                child.Reflow();
            }
        }

        private void reflowHorizontal()
        {
            var childX = 0;

            foreach (var child in Children)
            {
                childX += child.PreOffset*spanWidth;
                child.Bounds = new Rectangle(childX, 0, child.Span*spanWidth, Bounds.Height);
                childX += (child.Span + child.PostOffset) * spanWidth;

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

        /// <summary>
        /// Adds a child to the parent element in a chainable manner.
        /// </summary>
        /// <param name="element">The element to be added to the parent.</param>
        /// <param name="callback">Delegate that give access to the new child.</param>
        /// <returns>The parent element .</returns>
        public Element AddChild(Element element, Action<Element> callback = null)
        {
            element.Parent = this;
            element.ID = ID + "_" + Children.Count;

            Children.Add(element);
            spanCount += element.PreOffset + element.Span + element.PostOffset;

            Reflow();

            if (callback != null) callback(element);

            return this;
        }

        /// <summary>
        /// Adds a layout panel to the parent.
        /// </summary>
        /// <param name="preOffset">Spacers before the child.</param>
        /// <param name="span">The relative width of the child.</param>
        /// <param name="postOffset">Spacers after the child.</param>
        /// <param name="layoutStyle">The layout style of the child, defaulting to Layered.</param>
        /// <param name="callback">Delegate that give access to the new child.</param>
        /// <returns>The parent element.</returns>
        public Element AddPanel(int preOffset, int span, int postOffset, LayoutStyle layoutStyle = LayoutStyle.Layered, Action<Element> callback = null)
        {
            return AddChild(new Panel(preOffset, span, postOffset, layoutStyle), callback);
        }

        /// <summary>
        /// Adds a layout panel to the parent.
        /// </summary>
        /// <param name="preOffset">Spacers before the child.</param>
        /// <param name="span">The relative width of the child.</param>
        /// <param name="layoutStyle">The layout style of the child, defaulting to Layered.</param>
        /// <param name="callback">Delegate that give access to the new child.</param>
        /// <returns>The parent element.</returns>
        public Element AddPanel(int preOffset, int span, LayoutStyle layoutStyle = LayoutStyle.Layered, Action<Element> callback = null)
        {
            return AddPanel(preOffset, span,0, layoutStyle, callback);
        }

        /// <summary>
        /// Adds a layout panel to the parent.
        /// </summary>
        /// <param name="span">The relative width of the child.</param>
        /// <param name="layoutStyle">The layout style of the child, defaulting to Layered.</param>
        /// <param name="callback">Delegate that give access to the new child.</param>
        /// <returns>The parent element.</returns>
        public Element AddPanel(int span, LayoutStyle layoutStyle = LayoutStyle.Layered, Action<Element> callback = null)
        {
            return AddPanel(0, span, 0, layoutStyle, callback);
        }

        #endregion

        #region Game

        public override void Update(GameTime gameTime)
        {
            foreach (var child in Children)
                child.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var child in Children)
                child.Draw(gameTime);
        }

        #endregion

        #endregion
    }
}
