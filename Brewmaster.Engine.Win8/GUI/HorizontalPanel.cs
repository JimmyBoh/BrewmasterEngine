using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.GUI
{
    public class HorizontalPanel : Panel
    {
        public HorizontalPanel(int span, Element parent = null) : base(span, parent){}
        public HorizontalPanel(int preOffset, int span, Element parent = null) : base(preOffset, span, parent){}
        public HorizontalPanel(int preOffset, int span, int postOffset, Element parent = null) : base(preOffset, span, postOffset, parent){}

        public override void Reflow(Rectangle area)
        {
            var childX = 0;

            foreach (var child in Children)
            {
                childX += child.PreOffset * spanWidth;
                var suggestedBounds = child.Bounds = new Rectangle(childX, 0, child.Span * spanWidth, Bounds.Height);
                childX += (child.Span + child.PostOffset) * spanWidth;

                child.Reflow(suggestedBounds);
            }
        }
    }
}
