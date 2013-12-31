using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using OpenTK.Input;

namespace Sjerrul.OpenTk
{
    class Program : GameWindow
    {
        private double second = 1;
        private double angle = 0;

        Clock clock;

        public Program()
            : base(800, 600, GraphicsMode.Default, "Hello OpenTK")
        {
            this.clock = new Clock(180);

            Keyboard.KeyDown += new EventHandler<KeyboardKeyEventArgs>(Keyboard_KeyDown);
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Exit();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Set opengl view options
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            clock.Update(e.Time);           
        }

        protected override void OnResize(EventArgs e)
        {
            // Standard OpenTK code for window resize
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Ortho(-this.Width / 2, this.Width / 2, -this.Height / 2, this.Height / 2, -1, 1); // 0,0 in center
            //GL.Ortho(0, w, 0, h, -1, 1) //0,0 in bottom Left
            GL.Viewport(0, 0, this.Width, this.Height);

            GL.ClearColor(Color.SkyBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Disable(EnableCap.DepthTest); // Important for 2d drawing

            clock.Render();

            SwapBuffers();
        }

        void drawCircle(float radius)
        {
            GL.Color3(Color.White);
            GL.Begin(PrimitiveType.TriangleFan);

            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * 3.1416 / 180;
                GL.Vertex2(Math.Cos(degInRad) * radius, Math.Sin(degInRad) * radius);
            }

            GL.End();
        }

        [STAThread]
        public static void Main()
        {
            using (Program p = new Program())
            {
                p.Run(80f);
            }
        }

        
    }
}

