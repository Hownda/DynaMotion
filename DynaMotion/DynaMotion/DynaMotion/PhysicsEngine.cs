using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace DynaMotion.DynaMotion
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }
    public abstract class PhysicsEngine
    {
        public Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "Physics Engine";
        private Canvas Window = null;
        private Thread PhysicsLoopThread = null;

        public Color BackgroundColor = Color.Green;

        private static List<Object> ObjectsInScene = new List<Object>();

        public PhysicsEngine(Vector2 ScreenSize, string Title)
        {
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);
            Window.Text = this.Title;
            Window.Paint += Renderer;

            PhysicsLoopThread = new Thread(PhysicsLoop);
            PhysicsLoopThread.Start();

            Application.Run(Window);
        }

        public static void AddObject(Object newObject)
        {
            ObjectsInScene.Add(newObject);
        }

        void PhysicsLoop()
        {
            OnLoad();
            while(PhysicsLoopThread.IsAlive)
            {
                try
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(1);
                }
                catch
                {
                    Console.WriteLine("Eninge is loading...");
                }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackgroundColor);
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
    }
}
