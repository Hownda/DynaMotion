using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaMotion.DynaMotion
{
    public struct TimeStep
    {
        public float Dt; // time step
        public float Inv_Dt; // inverse time step (0 if dt == 0).
        public float DtRatio;   // dt * inv_dt0
        public int VelocityIterations;
        public int PositionIterations;
    }

    public static class PhysicsWorld
    {
        public readonly static List<Rigidbody> RigidbodiesInScene = new List<Rigidbody>();
        private static float _inv_dt0 = 0.0f;
        private static bool _lock;

        public static void AddRigidbody(Rigidbody rigidbody)
        {
            RigidbodiesInScene.Add(rigidbody);
        }

        private static void DetectCollisions()
        {
            for (int i = 0; i < RigidbodiesInScene.Count - 1; i++)
            {
                Rigidbody rb1 = RigidbodiesInScene[i];
                for (int j = i + 1; j < RigidbodiesInScene.Count; j++)
                {
                    Rigidbody rb2 = RigidbodiesInScene[j];

                    if (rb1.shapeType == ShapeType.Rect && rb2.shapeType == ShapeType.Circle)
                    {
                        if (Collisions.PolygonCircleCollision(rb2.Position, rb2.Scale.x / 2, rb1.GetTransformedVertices(), out Vector2 normal, out float depth))
                        {
                            rb1.Move(normal * depth / 2);
                            rb2.Move(-normal * depth / 2);
                        }
                    }
                    else if (rb2.shapeType == ShapeType.Rect && rb1.shapeType == ShapeType.Circle)
                    {
                        if (Collisions.PolygonCircleCollision(rb1.Position, rb1.Scale.x / 2, rb2.GetTransformedVertices(), out Vector2 normal, out float depth))
                        {
                            rb1.Move(-normal * depth / 2);
                            rb2.Move(normal * depth / 2);
                        }
                    }
                    else if (rb1.shapeType == ShapeType.Rect && rb2.shapeType == ShapeType.Rect)
                    {
                        if (Collisions.PolygonCollision(rb1.GetTransformedVertices(), rb2.GetTransformedVertices(), out Vector2 normal, out float depth))
                        {
                            rb1.Move(-normal * depth / 2);
                            rb2.Move(normal * depth / 2);
                        }
                    }
                    else
                    {
                        if (Collisions.CircleCollision(rb1.Position, rb1.Scale.x / 2, rb2.Position, rb2.Scale.x / 2, out Vector2 normal, out float depth))
                        {
                            rb1.Move(-normal * depth / 2);
                            rb2.Move(normal * depth / 2);
                        }
                    }
                }
            }
        }

        public static void Step()
        {
            DetectCollisions();
        }
    }
}
