using System;
using Microsoft.Xna.Framework;

namespace BrewmasterEngine.Extensions
{
    public static class MathExtensions
    {
        private const double toRad = (Math.PI)/180.0f;
        private const double toDeg = (180.0f/Math.PI);

        public static double ToRadians(double degrees)
        {
            return degrees*toRad;
        }

        public static float ToRadians(float degrees)
        {
            return ToRadians(degrees);
        }

        public static double ToDegrees(double radians)
        {
            return radians*toDeg;
        }

        public static float ToDegrees(float radians)
        {
            return ToDegrees(radians);
        }

        public static float? Add(float? thing, float? other)
        {
            return (thing.HasValue ? thing.Value : 0.0f) + (other.HasValue ? other.Value : 0.0f);
        }
    }
}
