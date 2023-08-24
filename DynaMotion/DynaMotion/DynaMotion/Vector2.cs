using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
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

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.x, -v.y);
        }

        public static Vector2 operator *(Vector2 v1, float f)
        {
            return new Vector2(v1.x * f, v1.y * f);
        }

        public static Vector2 operator *(float f, Vector2 v1)
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

        public static float Distance(Vector2 v1, Vector2 v2)
        {
            return Magnitude(v2 - v1);
        }

        internal static Vector2 Transform(Vector2 v, Transform transform)
        {
            float rx = transform.Cos * v.x - transform.Sin * v.y;
            float ry = transform.Sin * v.x + transform.Cos * v.y;

            float tx = rx + transform.position.x;
            float ty = ry + transform.position.y;

            return new Vector2( tx, ty );
        }

        public static PointF[] ConvertToPointF(Vector2[] v)
        {
            List<PointF> list = new List<PointF>();
            for (int i = 0; i < v.Length; i++)
            {
                list.Add(new PointF(v[i].x, v[i].y));
            }
            return list.ToArray();
        }
    }
}
