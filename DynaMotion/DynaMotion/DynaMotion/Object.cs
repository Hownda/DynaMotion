using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaMotion.DynaMotion
{
    public class Object
    {
        // Unique object id
        public int Id { get; private set; }
        // Object render mode
        public bool IsActive { get; private set; }

        // Transform values
        public Vector2 Position { get; private set; }
        public Vector2 Rotation { get; private set; }
        public Vector2 Scale { get; private set; }

        /// <summary>
        /// Constructs a new object in the scene.
        /// </summary>
        /// <param name="position">Objects position in scene.</param>
        /// <param name="rotation">Objects rotation in scene.</param>
        /// <param name="scale">Objects scale in scene.</param>
        public Object(Vector2 position, Vector2 rotation, Vector2 scale)
        {
            // Create a unique id
            Id = Program.ObjectsInScene;
            Program.ObjectsInScene++;

            // Add object to a list in PhysicsEngine
            PhysicsEngine.AddObject(this);

            // Set transform values
            SetPosition(position);
            SetRotation(rotation);
            SetScale(scale);

            // TODO Render object in scene
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void SetRotation(Vector2 rotation)
        {
            Rotation = rotation;
        }

        public void SetScale(Vector2 scale)
        {
            Scale = scale;
        }

        public void SetActive(bool visible)
        {
            IsActive = visible;
        }
    }
}
