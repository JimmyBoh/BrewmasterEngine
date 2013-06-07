

using System;

namespace BrewmasterEngine.GUI
{
    public enum Layout
    {
        Layered   = 0,
        Vertical   = 1 << 0,
        Horizontal = 1 << 1,
        Absolute   = 1 << 2
    }
}