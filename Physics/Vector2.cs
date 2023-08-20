using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Physics
{
    public readonly struct Vector2
    {
        public readonly float x;
        public readonly float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = ;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y, a.y);
        }
    }
}