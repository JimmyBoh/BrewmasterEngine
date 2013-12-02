using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGame.Scenes.BouncingBall
{
    public static class EventManager
    {
        public static event AlterBallCountEvent AlterBallCount;
    }

    public delegate void AlterBallCountEvent(object sender, AlterBallCountEventArgs args);
    public class AlterBallCountEventArgs
    {
        public int Changes { get; set; }
    }
}
