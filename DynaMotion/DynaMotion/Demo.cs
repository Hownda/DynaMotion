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
            
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            Debug.LogWarning("Input System not implemented");
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            Debug.LogWarning("Input System not implemented");
        }
    }
}
