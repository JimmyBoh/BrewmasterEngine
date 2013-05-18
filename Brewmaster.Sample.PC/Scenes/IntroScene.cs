using System;
using System.Collections.Generic;
using BrewmasterEngine.Framework;
using BrewmasterEngine.Scenes;
using Microsoft.Xna.Framework;
using SampleGame.Menu.Widgets;

namespace SampleGame.Scenes
{
    public class IntroScene : Scene
    {
        public IntroScene() : base("intro")
        {
            
        }

        private float remaining;

        protected override void Load(Action done)
        {
            var windowBounds = CurrentGame.Window.ClientBounds;
            var windowSize = new Vector2(windowBounds.Width, windowBounds.Height);

            this.Add(new MenuText("JimmyBoh", windowSize*new Vector2(0.5f, 0.2f)));
            this.Add(new MenuText("Loading...", windowSize*new Vector2(0.1f, 0.9f)));

            remaining = 2000;

            done();
        }

        public override void Update(GameTime gameTime)
        {
            remaining -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(gameTime);

            if(remaining <= 0)
                CurrentGame.SceneManager.LoadNextScene();
                
            
        }
    }
}