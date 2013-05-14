using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewmasterEngine.Scenes
{
    public interface INotPausable
    {
        void OnPause();

        void OnUnpause();
    }
}
