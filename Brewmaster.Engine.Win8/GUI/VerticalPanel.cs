using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.GUI
{
    public class VerticalPanel : Panel
    {
        public VerticalPanel(int span, Element parent = null) : base(span, parent){}
        public VerticalPanel(int preOffset, int span, Element parent = null) : base(preOffset, span, parent){}
        public VerticalPanel(int preOffset, int span, int postOffset, Element parent = null) : base(preOffset, span, postOffset, parent){}

        public override void Reflow(Rectangle area)
        {
            var childY = 0;

            foreach (var child in Children)
            {
                childY += child.PreOffset * spanHeight;
                var suggestedBounds = child.Bounds = new Rectangle(0, childY, area.Width, child.Span * spanHeight);
                childY += (child.Span + child.PostOffset) * spanHeight;

                child.Reflow(suggestedBounds);
            }
        }
    }
}
