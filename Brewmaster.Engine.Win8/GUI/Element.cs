using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.DataTypes;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrewmasterEngine.GUI
{
    public abstract class Element : GameObject
    {
        #region Constructor

        protected Element()
        {
            Position = Vector2.Zero;
            Size = Vector2.Zero;
            Scale = Vector2.One;
            ReflowScale = 1f;
            
            PreOffset = 0;
            Span = 1;
            PostOffset = 0;

            Parent = null;
            Children = new List<Element>();
            spanCount = 0;
        }

        #endregion

        #region Properties

        public string ID { get; set; }

        public Element Parent { get; set; }
        public List<Element> Children { get; set; }

        /// <summary>
        /// The relative bounds of the element with respect to the parent.
        /// </summary>
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

        /// <summary>
        /// The absolute bounds of the element.
        /// </summary>
        public Rectangle RenderBounds
        {
            get
            {
                return Parent == null ? Bounds : new Rectangle(Parent.RenderBounds.X + Bounds.X, Parent.RenderBounds.Y + Bounds.Y, Bounds.Width, Bounds.Height);
            }
        }

        public int RelativeIndex
        {
            get
            {
                return Parent.Children.IndexOf(this);
            }
        }

        public int RelativeLayer
        {
            get { return Parent == null ? 0 : 1 + Parent.RelativeIndex; }
        }

        public Vector2 Scale { get; set; }
        public float ReflowScale { get; set; }

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

        public virtual void Reflow(Rectangle area)
        {
            foreach (var child in Children)
            {
                child.Reflow(RenderBounds);
            }
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

            Reflow(Bounds);

            if (callback != null) callback(element);

            return this;
        }

        /// <summary>
        /// Adds a vertical layout panel to the parent.
        /// </summary>
        /// <param name="preOffset">Spacers before the child.</param>
        /// <param name="span">The relative size of the child.</param>
        /// <param name="postOffset">Spacers after the child.</param>
        /// <param name="callback">Delegate that give access to the new child.</param>
        /// <returns>The parent element.</returns>
        public Element AddVerticalPanel(int preOffset, int span, int postOffset, Action<Element> callback = null)
        {
            return AddChild(new VerticalPanel(preOffset, span, postOffset), callback);
        }

        /// <summary>
        /// Adds a vertical layout panel to the parent.
        /// </summary>
        /// <param name="preOffset">Spacers before the child.</param>
        /// <param name="span">The relative size of the child.</param>
        /// <param name="callback">Delegate that give access to the new child.</param>
        /// <returns>The parent element.</returns>
        public Element AddVerticalPanel(int preOffset, int span, Action<Element> callback = null)
        {
            return AddVerticalPanel(preOffset, span,0, callback);
        }

        /// <summary>
        /// Adds a vertical layout panel to the parent.
        /// </summary>
        /// <param name="span">The relative size of the child.</param>
        /// <param name="callback">Delegate that give access to the new child.</param>
        /// <returns>The parent element.</returns>
        public Element AddVerticalPanel(int span, Action<Element> callback = null)
        {
            return AddVerticalPanel(0, span, 0, callback);
        }

        /// <summary>
        /// Adds a horizontal layout panel to the parent.
        /// </summary>
        /// <param name="preOffset">Spacers before the child.</param>
        /// <param name="span">The relative size of the child.</param>
        /// <param name="postOffset">Spacers after the child.</param>
        /// <param name="callback">Delegate that give access to the new child.</param>
        /// <returns>The parent element.</returns>
        public Element AddHorizontalPanel(int preOffset, int span, int postOffset, Action<Element> callback = null)
        {
            return AddChild(new HorizontalPanel(preOffset, span, postOffset), callback);
        }

        /// <summary>
        /// Adds a horizontal layout panel to the parent.
        /// </summary>
        /// <param name="preOffset">Spacers before the child.</param>
        /// <param name="span">The relative size of the child.</param>
        /// <param name="callback">Delegate that give access to the new child.</param>
        /// <returns>The parent element.</returns>
        public Element AddHorizontalPanel(int preOffset, int span, Action<Element> callback = null)
        {
            return AddVerticalPanel(preOffset, span, 0, callback);
        }

        /// <summary>
        /// Adds a horizontal layout panel to the parent.
        /// </summary>
        /// <param name="span">The relative size of the child.</param>
        /// <param name="callback">Delegate that give access to the new child.</param>
        /// <returns>The parent element.</returns>
        public Element AddHorizontalPanel(int span, Action<Element> callback = null)
        {
            return AddVerticalPanel(0, span, 0, callback);
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
