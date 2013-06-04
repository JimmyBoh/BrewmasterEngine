using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.DataTypes
{
    public interface IGameObject
    {
        string Name { get; set; }
        bool IsActive { get; set; }
        bool IsVisible { get; set; }
        bool IsFree { get; set; }
        int ZIndex { get; set; }

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
