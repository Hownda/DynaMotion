using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DynaMotion.DynaMotion
{
    public static class Collisions
    {
        public static bool PolygonCircleCollision(Vector2 circleCenter, float circleRadius, Vector2[] vertices, out Vector2 normal, out float depth)
        {
            normal = Vector2.zero;
            depth = float.MaxValue;

            Vector2 axis = Vector2.zero;
            float axisDepth = 0;
            float min1, max1, min2, max2;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 v1 = vertices[i];
                Vector2 v2 = vertices[(i + 1) % vertices.Length];

                Vector2 edge = v2 - v1;
                axis = new Vector2(-edge.y, edge.x);

                ProjectVertices(vertices, axis, out min1, out max1);
                ProjectCircle(circleCenter, circleRadius, axis, out min2, out max2);

                if (min1 >= max2 || min2 >= max1)
                {
                    return false;
                }

                axisDepth = Math.Min(max2 - min1, max1 - min2);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }

            int closestPointIndex = FindClosestPointOnPolygon(circleCenter, vertices);
            Vector2 closestPoint = vertices[closestPointIndex];

            axis = closestPoint - circleCenter;

            ProjectVertices(vertices, axis, out min1, out max1);
            ProjectCircle(circleCenter, circleRadius, axis, out min2, out max2);

            if (min1 >= max2 || min2 >= max1)
            {
                return false;
            }

            axisDepth = Math.Min(max2 - min1, max1 - min2);

            if (axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }

            depth /= Vector2.Magnitude(normal);
            normal = Vector2.Normalize(normal);

            
            Vector2 polygonCenter = FindArithmeticMean(vertices);

            Vector2 direction = polygonCenter - circleCenter;

            if (Vector2.DotProduct(direction, normal) < 0)
            {
                normal = -normal;
            }

            return true;
        }

        private static void ProjectCircle(Vector2 center, float radius, Vector2 axis, out float min, out float max)
        {
            Vector2 direction = Vector2.Normalize(axis);
            Vector2 directionAndRadius = direction * radius;

            Vector2 p1 = center + directionAndRadius;
            Vector2 p2 = center - directionAndRadius;

            min = Vector2.DotProduct(p1, axis);
            max = Vector2.DotProduct(p2, axis);

            if (min > max)
            {
                float temp = min;
                min = max;
                max = temp;
            }

        }

        private static int FindClosestPointOnPolygon(Vector2 circleCenter, Vector2[] vertices)
        {
            int result = -1;
            float minDist = float.MaxValue;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 v = vertices[i];
                float distance = Vector2.Distance(v, circleCenter);

                if (distance < minDist)
                {
                    minDist = distance;
                    result = i;
                }
            }
            return result;
        }

        public static bool CircleCollision(Vector2 center1, float radius1, Vector2 center2, float radius2, out Vector2 normal, out float depth)
        {
            normal = Vector2.zero;
            depth = 0;

            float distance = Vector2.Distance(center1, center2);
            float radii = radius1 + radius2;

            if (distance >= radii)
            {
                return false;
            }

            normal = Vector2.Normalize(center2 - center1);
            depth = radii - distance;

            return true;
        }

        public static bool PolygonCollision(Vector2[] vertices1, Vector2[] vertices2, out Vector2 normal, out float depth)
        {
            normal = Vector2.zero;
            depth = float.MaxValue;

            for (int i = 0; i < vertices1.Length; i++)
            {
                Vector2 v1 = vertices1[i];
                Vector2 v2 = vertices1[(i + 1) % vertices1.Length];

                Vector2 edge = v2 - v1;
                Vector2 axis = new Vector2(-edge.y, edge.x);

                ProjectVertices(vertices1, axis, out float min1, out float max1);
                ProjectVertices(vertices2, axis, out float min2, out float max2);

                if (min1 >= max2 || min2 >= max1)
                {
                    return false;
                }

                float axisDepth = Math.Min(max2 - min1, max1 - min2);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }

            for (int i = 0; i < vertices2.Length; i++)
            {
                Vector2 v1 = vertices2[i];
                Vector2 v2 = vertices2[(i + 1) % vertices2.Length];

                Vector2 edge = v2 - v1;
                Vector2 axis = new Vector2(-edge.y, edge.x);

                ProjectVertices(vertices1, axis, out float min1, out float max1);
                ProjectVertices(vertices2, axis, out float min2, out float max2);

                if (min1 >= max2 || min2 >= max1)
                {
                    return false;
                }

                float axisDepth = Math.Min(max2 - min1, max1 - min2);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }
            depth /= Vector2.Magnitude(normal);
            normal = Vector2.Normalize(normal);

            Vector2 center1 = FindArithmeticMean(vertices1);
            Vector2 center2 = FindArithmeticMean(vertices2);

            Vector2 direction = center2 - center1;

            if (Vector2.DotProduct(direction, normal) < 0)
            {
                normal = -normal;
            }

            return true;
        }             

        private static void ProjectVertices(Vector2[] vertices, Vector2 axis, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 v = vertices[i];
                float proj = Vector2.DotProduct(v, axis);

                if (proj < min)
                {
                    min = proj;
                }
                if (proj > max)
                {
                    max = proj;
                }
            }
        }

        private static Vector2 FindArithmeticMean(Vector2[] vertices)
        {
            float sumX = 0;
            float sumY = 0;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 v = vertices[i];
                sumX += v.x;
                sumY += v.y;
            }

            return new Vector2(sumY / (float)vertices.Length, sumY / (float)vertices.Length);
        }
    }
}
