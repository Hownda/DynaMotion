using System;
using System.Collections.Generic;
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
        // Camera Movement
        bool left;
        bool right;
        bool up;
        bool down;

        public Demo() : base(new Vector2(615, 515), "Physics Engine Demo") { }

        public override void OnLoad()
        {
            BackgroundColor = Color.Black;

            Rigidbody rigidbody = new Rigidbody(new Vector2(10, 10), new Vector2(0, 0), new Vector2(20, 20));
        }

        public override void OnDraw()
        {
            
        }

        int time = 0;
        public override void OnUpdate()
        {
            if (up)
            {
                CameraPosition += new Vector2(0, 1);
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
