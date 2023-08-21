using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaMotion.DynaMotion
{
    public readonly struct Vector2
    {
        public readonly float x;
        public readonly float y;

        public static readonly Vector2 zero = new Vector2(0f, 0f);

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 operator *(Vector2 v1, float f)
        {
            return new Vector2(v1.x * f, v1.y * f);
        }

        public static Vector2 operator /(Vector2 v1, float f)
        {
            return new Vector2(v1.x / f, v1.y / f);
        }

        public static float Magnitude(Vector2 vector)
        {
            return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y);
        }

        public static Vector2 Normalize(Vector2 vector)
        {
            var magnitude = Magnitude(vector);
            if (magnitude > 0)
            {
                return vector / magnitude;
            }
            else
            {
                Debug.LogError($"Can't normalize vector when it's magnitude is <= 0.");
                return vector;
            }
        }

        public static float DotProduct(Vector2 v1, Vector2 v2)
        {
            return v1.x * v2.x + v1.y * v2.y;
        }

        public static float CrossProduct(Vector2 v1, Vector2 v2)
        {
            // TODO Check right way to calculate.
            return v1.x * v2.y - v1.y * v2.x;
            // return Magnitude(v1) * Magnitude(v2);
        }

        public static float Angle(Vector2 v1, Vector2 v2)
        {
            return (float)Math.Acos((DotProduct(v1, v2)) / (Magnitude(v1) * Magnitude(v2)));
        }
    }
}
