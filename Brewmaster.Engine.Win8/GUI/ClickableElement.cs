using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewmasterEngine.GUI
{
    public interface IClickable
    {
        int? TouchID { get; set; }
        
        void OnPress();
        void OnDragOut();
        void OnRelease();
    }
}
