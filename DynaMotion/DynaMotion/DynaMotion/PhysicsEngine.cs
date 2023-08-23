using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

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

        private static List<Rigidbody> RigidbodiesInScene = new List<Rigidbody>();

        public Vector2 CameraPosition = Vector2.zero;
        public float CameraRotation = 0;

        public PhysicsEngine(Vector2 ScreenSize, string Title)
        {
            Debug.Log("Physics Engine starting...");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)this.ScreenSize.x, (int)this.ScreenSize.y);
            Window.Text = this.Title;
            Window.Paint += Renderer;
            Window.KeyDown += WindowKeyDown;
            Window.KeyUp += WindowKeyUp;
            Window.FormClosing += WindowFormClosing;
            Window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            PhysicsLoopThread = new Thread(PhysicsLoop);
            PhysicsLoopThread.Start();

            Application.Run(Window);
        }

        private void WindowFormClosing(object sender, FormClosingEventArgs e)
        {
            PhysicsLoopThread.Abort();
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        private void WindowKeyUp(object sender, KeyEventArgs e) 
        {
            GetKeyUp(e);
        }

        public static void AddRigidbody(Rigidbody newObject)
        {
            RigidbodiesInScene.Add(newObject);
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
                    DetectCollisions();
                    OnUpdate();
                    Thread.Sleep(1);
                }
                catch
                {
                    Debug.LogError("Window has not been found!");
                }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackgroundColor);

            // Camera transform
            g.TranslateTransform(CameraPosition.x, CameraPosition.y);
            g.RotateTransform(CameraRotation);

            // Render objects
            foreach (Rigidbody rb in RigidbodiesInScene)
            {
                if (rb.shapeType == ShapeType.Circle)
                {
                    g.FillEllipse(new SolidBrush(Color.Blue), rb.Position.x, rb.Position.y, rb.Scale.x, rb.Scale.y);
                }
                if (rb.shapeType == ShapeType.Rect)
                {
                    g.FillPolygon(new SolidBrush(Color.Green), Vector2.ConvertToPointF(rb.GetTransformedVertices()));
                }
            }
            
        }

        private void DetectCollisions()
        {
            for (int i = 0; i < RigidbodiesInScene.Count - 1 ; i++)
            {
                Rigidbody rb1 = RigidbodiesInScene[i];
                for (int j = i + 1; j < RigidbodiesInScene.Count; j++)
                {
                    Rigidbody rb2 = RigidbodiesInScene[j];

                    if (Collisions.PolygonCollision(rb1.GetTransformedVertices(), rb2.GetTransformedVertices(), out Vector2 normal, out float depth))                     
                    {
                        rb1.Move(-normal * depth / 2);
                        rb2.Move(normal * depth / 2);
                    }
                }
            }
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
    }
}
