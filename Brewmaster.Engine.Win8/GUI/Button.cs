using System.Linq;
using BrewmasterEngine.Framework;
using BrewmasterEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace BrewmasterEngine.GUI
{
    public abstract class Button : Element, IClickable
    {
        public int? TouchID { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (TouchID.HasValue)
            {
                var releases = CurrentGame.TouchState.Where(t => t.State == TouchLocationState.Released && t.Id == TouchID.Value).ToList();

                if (releases.Any())
                {
                    if (RenderBounds.Contains(releases.First().Position))
                        OnRelease();

                    TouchID = null;
                }
                else
                {
                    var moves = CurrentGame.TouchState.Where(t => t.State == TouchLocationState.Moved && t.Id == TouchID.Value).ToList();

                    if (moves.Any())
                    {
                        var currTouch = moves.First();
                        TouchLocation prevTouch;

                        if (currTouch.TryGetPreviousLocation(out prevTouch) && (RenderBounds.Contains(prevTouch.Position) && !RenderBounds.Contains(currTouch.Position)))
                        {
                            TouchID = null;
                            OnDragOut();
                        }
                    }
                }
            }
            else
            {
                var presses = CurrentGame.TouchState.Where(t => t.State == TouchLocationState.Pressed && RenderBounds.Contains(t.Position)).ToList();

                if (presses.Any())
                {
                    TouchID = presses.First().Id;
                    OnPress();
                }
            }
        }


        public virtual void OnPress()
        {
            
        }

        public virtual void OnDragOut()
        {
            
        }

        public virtual void OnRelease()
        {
            
        }
    }
}
