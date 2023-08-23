using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaMotion.DynaMotion
{
    public sealed class Rigidbody
    {
        // Unique object id
        public int Id { get; private set; }
        // Object render mode
        public bool IsActive { get; private set; }

        // Transform values
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public Vector2 Scale { get; private set; }

        public Vector2 velocity { get; private set; }
        public float angularVelocity { get; private set; }

        public ShapeType shapeType { get; private set; }

        private readonly Vector2[] vertices;
        public readonly int[] triangles;
        private Vector2[] transformedVertices;

        private bool transformUpdateRequired = true;


        /// <summary>
        /// Constructs a new object in the scene.
        /// </summary>
        /// <param name="position">Objects position in scene.</param>
        /// <param name="rotation">Objects rotation in scene.</param>
        /// <param name="scale">Objects scale in scene.</param>
        public Rigidbody(Vector2 position, float rotation, Vector2 scale, ShapeType shapeType)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
            this.shapeType = shapeType;

            if (this.shapeType == ShapeType.Rect)
            {
                this.vertices = CreateVertices(this.Scale.x, this.Scale.y);
                this.triangles = TriangulateBox();
                this.transformedVertices = new Vector2[this.vertices.Length];
            }
            else
            {
                vertices = null;
                triangles = null;
                this.transformedVertices = null;
            }

            this.transformUpdateRequired = true;

            // Create a unique id
            Id = Program.ObjectsInScene;
            Program.ObjectsInScene++;

            // Add object to a list in PhysicsEngine
            Debug.Log($"[Rigidbody] - Has been instantiated");
            PhysicsEngine.AddRigidbody(this);
        }

        private static Vector2[] CreateVertices(float width, float height)
        {
            float left = -width / 2f;
            float right = left + width;
            float bottom = -height / 2;
            float top = bottom + height;

            Vector2[] vertices = new Vector2[4];
            vertices[0] = new Vector2(left, top);
            vertices[1] = new Vector2(right, top);
            vertices[2] = new Vector2(right, bottom);
            vertices[3] = new Vector2(left, bottom);

            return vertices;
        }

        private static int[] TriangulateBox()
        {
            int[] triangles = new int[6];
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;
            return triangles;
        }

        public Vector2[] GetTransformedVertices()
        {
            if (this.transformUpdateRequired)
            {
                Transform transform = new Transform(this.Position, this.Rotation);
                for (int i = 0; i < this.vertices.Length; i++)
                {
                    Vector2 v = this.vertices[i];
                    this.transformedVertices[i] = Vector2.Transform(v, transform);
                }
            }
            this.transformUpdateRequired = false;
            return this.transformedVertices;
        }

        public void Move(Vector2 amount)
        {
            this.Position += amount;
            this.transformUpdateRequired = true;
        }

        public void Rotate(float amount)
        {
            this.Rotation += amount;
            this.transformUpdateRequired = true;
        }
    }
}
