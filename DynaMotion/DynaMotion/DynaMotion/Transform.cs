using System;

namespace DynaMotion.DynaMotion
{
    internal  readonly struct Transform
    {
        public readonly Vector2 position;
        public readonly float Sin;
        public readonly float Cos;

        public Transform(Vector2 position, float angle)
        {
            this.position = position;
            this.Sin = (float)Math.Sin(angle);
            this.Cos = (float)Math.Cos(angle);
        }
    }
}
