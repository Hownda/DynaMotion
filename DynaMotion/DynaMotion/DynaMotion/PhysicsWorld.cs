using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DynaMotion.DynaMotion
{
    public static class PhysicsWorld
    {
        public readonly static List<Rigidbody> RigidbodiesInScene = new List<Rigidbody>();
        private static Vector2 gravity = new Vector2(0, 9.81f);

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

        public static void Step(float deltaTime)
        {
            // Rigidbody operations
            for (int i = 0; i < RigidbodiesInScene.Count; i++)
            {
                // Movement
                RigidbodiesInScene[i].Step(deltaTime);

                // Collision Check
                Rigidbody rb1 = RigidbodiesInScene[i];
                for (int j = i + 1; j < RigidbodiesInScene.Count; j++)
                {
                    Rigidbody rb2 = RigidbodiesInScene[j];
                    if (Collide(rb1, rb2, out Vector2 normal, out float depth))
                    {
                        // Moves both rigidbodies by half the intersection depth
                        rb1.Move(-normal * depth / 2);
                        rb2.Move(normal * depth / 2);

                        ResolveCollision(rb1, rb2, normal, depth);
                    }
                }
            }            
        }

        // Checks for collisions between all different shape types.
        private static bool Collide(Rigidbody rb1, Rigidbody rb2, out Vector2 normal, out float depth)
        {
            normal = Vector2.zero;
            depth = 0;

            if (rb1.shapeType is ShapeType.Rect)
            {
                if (rb2.shapeType is ShapeType.Rect)
                {
                    return Collisions.PolygonCollision(rb1.GetTransformedVertices(), rb2.GetTransformedVertices(), out normal, out depth);
                }
                else if (rb2.shapeType is ShapeType.Circle)
                {
                    bool result = Collisions.PolygonCircleCollision(rb2.Position + new Vector2(rb2.Scale.x / 2, rb2.Scale.y / 2), rb2.Scale.x / 2, rb1.GetTransformedVertices(), out normal, out depth);

                    normal = -normal;
                    return result;
                }
            }
            else if (rb1.shapeType is ShapeType.Circle)
            {
                if ( rb2.shapeType is ShapeType.Rect)
                {
                    return Collisions.PolygonCircleCollision(rb1.Position + new Vector2(rb1.Scale.x / 2, rb1.Scale.y / 2), rb1.Scale.x / 2, rb2.GetTransformedVertices(), out normal, out depth);
                }
                else if (rb2.shapeType is ShapeType.Circle)
                {
                    return Collisions.CircleCollision(rb1.Position + new Vector2(rb1.Scale.x / 2, rb1.Scale.y / 2), rb1.Scale.x / 2, rb2.Position + new Vector2(rb2.Scale.x / 2, rb2.Scale.y / 2), rb2.Scale.x / 2, out normal, out depth);
                }
            }

            return false;
        }

        private static void ResolveCollision(Rigidbody rb1, Rigidbody rb2, Vector2 normal, float depth)
        {
            Vector2 relativeVelocity = rb2.velocity - rb1.velocity;

            float e = Math.Min(rb1.restitution, rb2.restitution);

            float j = -(1 + e) * Vector2.DotProduct(relativeVelocity, normal);
            j /= (1f / rb1.mass) + (1 / rb2.mass);

            rb1.velocity -= j / rb1.mass * normal;
            rb2.velocity += j / rb2.mass * normal;
        }
    }
}
