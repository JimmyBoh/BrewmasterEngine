using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewmasterEngine.GUI.Events
{
    public interface IClickable
    {
        bool WasClicked { get; set; }

        void OnPress();
        void OnRelease(bool releasedOn);

    }
}
