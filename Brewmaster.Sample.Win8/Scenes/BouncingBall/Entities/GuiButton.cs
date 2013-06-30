using System;
using BrewmasterEngine.GUI;

namespace Brewmaster.Sample.Win8.Scenes.BouncingBall.Entities
{
    public class GuiButton: Element, IClickable
    {
        public int? TouchID { get; set; }

        public void OnPress()
        {
            throw new NotImplementedException();
        }

        public void OnDragOut()
        {
            throw new NotImplementedException();
        }

        public void OnRelease()
        {
            throw new NotImplementedException();
        }
    }
}
