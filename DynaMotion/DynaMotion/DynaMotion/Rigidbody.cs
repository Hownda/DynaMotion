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
        public Vector2 Rotation { get; private set; }
        public Vector2 Scale { get; private set; }

        public Vector2 velocity { get; private set; }
        public float angularVelocity { get; private set; }

        /// <summary>
        /// Constructs a new object in the scene.
        /// </summary>
        /// <param name="position">Objects position in scene.</param>
        /// <param name="rotation">Objects rotation in scene.</param>
        /// <param name="scale">Objects scale in scene.</param>
        public Rigidbody(Vector2 position, Vector2 rotation, Vector2 scale)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;

            // Create a unique id
            Id = Program.ObjectsInScene;
            Program.ObjectsInScene++;

            // Add object to a list in PhysicsEngine
            Debug.Log($"[Rigidbody] - Has been instantiated");
            PhysicsEngine.AddRigidbody(this);
        }

        public void Move(Vector2 amount)
        {
            this.Position += amount;
        }
    }
}
