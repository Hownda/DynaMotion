using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaMotion.DynaMotion;

namespace DynaMotion
{
    class Demo : PhysicsEngine
    {
        public Demo() : base(new Vector2(615, 515), "Physics Engine Demo") { }

        public override void OnLoad()
        {
            Console.WriteLine("Hello World");
            BackgroundColor = Color.Black;
        }

        public override void OnDraw()
        {
            
        }        

        int frame = 0;
        public override void OnUpdate()
        {
            Console.WriteLine($"Frame Count: {frame}");
            frame++;
        }
    }
}
