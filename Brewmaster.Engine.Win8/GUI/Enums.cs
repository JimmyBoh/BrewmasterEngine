

using System;

namespace BrewmasterEngine.GUI
{
    public enum LayoutStyle
    {
        /// <summary>
        /// Children in this element are all center aligned.
        /// </summary>
        Centered   = 0,

        /// <summary>
        /// Children in this element are stacked vertially.
        /// </summary>
        Vertical   = 1 << 0,

        /// <summary>
        /// Children in this element are stacked horizontally.
        /// </summary>
        Horizontal = 1 << 1
    }
}