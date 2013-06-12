using System;
using System.Collections.Generic;
using System.Linq;
using BrewmasterEngine.Framework;
using BrewmasterEngine.GUI;
using BrewmasterEngine.GUI.Interfaces;
using BrewmasterEngine.Graphics;
using BrewmasterEngine.Graphics.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Brewmaster.Sample.Win8.Scenes.MainMenu.Entities
{
    public class DemoButton : Element, IClickable
    {
        public DemoButton(string text, string sceneName)
        {
            Text = new StaticText("DebugFont", text);

            SceneName = sceneName;

            Rotation = CurrentGame.Random.Next(-3, 3)/100.0f;
            Scale = 1f;
        }

        public StaticText Text { get; set; }
        public string SceneName { get; set; }
        
        public float Rotation { get; set; }
        public float Scale { get; set; }

        public override void Update(GameTime gameTime)
        {
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
        public override void Reflow()
        {
            base.Reflow();

            Bounds = new Rectangle(0, 0, Parent.RenderBounds.Width, Parent.RenderBounds.Height);
            Text.CenterOn(Parent.RenderBounds.Center.ToVector2());
        }

        public override void Draw(GameTime gameTime)
        {
            var offset = (int)((1f - Scale)*100f);
            var shadow = new Rectangle(RenderBounds.X + 5+offset, RenderBounds.Y + 5+offset, (int)(RenderBounds.Width * Scale), (int)(RenderBounds.Height * Scale));
            var tile = new Rectangle(RenderBounds.X+offset, RenderBounds.Y+offset, (int)(RenderBounds.Width * Scale), (int)(RenderBounds.Height * Scale));
            spriteBatch.FillRectangle(shadow, Color.Gray * 0.5f, Rotation);
            spriteBatch.FillRectangle(tile, Color.DarkGray, Rotation);
            Text.Draw(gameTime);
        }

        public int? TouchID { get; set; }

        public void OnPress()
        {
            Scale -= 0.1f;
            Text.Scale -= new Vector2(0.1f);
        }

        public void OnDragOut()
        {
            Scale += 0.1f;
            Text.Scale += new Vector2(0.1f);
        }

        public void OnRelease()
        {
            CurrentGame.SceneManager.Load(SceneName);
        }
    }
}
