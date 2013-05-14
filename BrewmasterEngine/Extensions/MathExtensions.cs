using System;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.Extensions
{
    public static class MathExtensions
    {
        private const float toRad = ((float) Math.PI)/180.0f;
        private const float toDeg = (180.0f/(float) Math.PI);

        public static float ToRadians(float degrees)
        {
            return degrees*toRad;
        }

        public static float ToRadians(double degrees)
        {
            return ToRadians((float) degrees);
        }

        public static float ToDegrees(float radians)
        {
            return radians*toDeg;
        }

        public static float ToDegrees(double radians)
        {
            return ToDegrees((float) radians);
        }

        public static float? Add(float? thing, float? other)
        {
            return (thing.HasValue ? thing.Value : 0.0f) + (other.HasValue ? other.Value : 0.0f);
        }
    }
}
