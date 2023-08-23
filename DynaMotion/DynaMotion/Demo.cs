using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DynaMotion.DynaMotion;

namespace DynaMotion
{
    public class Demo : PhysicsEngine
    {
        private int rigidbodyCount = 10;
        private Rigidbody ground = null;

        // Camera Movement
        bool left;
        bool right;
        bool up;
        bool down;

        public Demo() : base(new Vector2(615, 515), "Physics Engine Demo") { }

        public override void OnLoad()
        {
            BackgroundColor = Color.Black;

            ground = new Rigidbody(new Vector2(0, 300), 0, new Vector2(1000, 20), ShapeType.Rect);

            Random rnd = new Random();
            for (int i = 0; i < rigidbodyCount; i++)
            {            
                Vector2 randomPosition = new Vector2(rnd.Next(10, 512), rnd.Next(10, 512));
                Rigidbody rigidbody = new Rigidbody(randomPosition, 0, new Vector2(20, 20), ShapeType.Rect);
            }    
        }

        public override void OnDraw()
        {
            
        }

        public override void OnUpdate()
        {       
            if (up)
            {
                //CameraPosition += new Vector2(0, 1);
                ground.Move(new Vector2(0, -1));
            }
            if (down)
            {
                CameraPosition -= new Vector2(0, 1);
            }
            if (left)
            {
                CameraPosition += new Vector2(1, 0);
            }
            if (right)
            {
                CameraPosition -= new Vector2(1, 0);
            }
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = true; }
            if (e.KeyCode == Keys.S) { down = true; }
            if (e.KeyCode == Keys.A) { left = true; }
            if (e.KeyCode == Keys.D) { right = true; }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = false; }
            if (e.KeyCode == Keys.S) { down = false; }
            if (e.KeyCode == Keys.A) { left = false; }
            if (e.KeyCode == Keys.D) { right = false; }
        }
    }
}
