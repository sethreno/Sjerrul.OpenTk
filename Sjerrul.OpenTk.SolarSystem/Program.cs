﻿using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Sjerrul.OpenTk.SolarSystem;

namespace Sjerrul.OpenTk
{
	class Program : GameWindow
    {
        private Body _sun;
        private Body _earth;
        private Body _moon;
        private Body _mars;
        private float _height = 5f;

        Matrix4 matrixModelview;
        float cameraRotation = 0f;
 
		public Program()
			: base(800, 600, GraphicsMode.Default, "Hello OpenTK")
		{
			Keyboard.KeyDown += new EventHandler<KeyboardKeyEventArgs>(Keyboard_KeyDown);

            _sun = new Body(null, 0.5f, 5, 0, 0, Color.Yellow);
            _earth = new Body(_sun, 0.2f, 60, 2, 1, Color.Green);
            _moon = new Body(_earth, 0.05f, 60, 1, 3, Color.White);
            _mars = new Body(_sun, 0.25f, 60, 3f, 0.75f, Color.Red);
		}

		void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Exit();

            if (e.Key == Key.Up)
            {
                _height = _height + 0.2f;
            }

            if (e.Key == Key.Down)
            {
                _height = _height - 0.2f;
            }
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
           // GL.Enable(EnableCap.Lighting);
           // GL.Enable(EnableCap.Light0);

            //GL.Light(LightName.Light0, LightParameter.Position, new Vector4(0,0,0,1)); 


            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
            _sun.Update(e.Time);
            _earth.Update(e.Time);
            _moon.Update(e.Time);
            _mars.Update(e.Time);
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
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            #region Camera

            Matrix4.CreateRotationY(cameraRotation, out matrixModelview);

            Vector3 eye = new Vector3(0f, _height, -10f);
            Vector3 target = new Vector3(0f, 0f, 0f);
            Vector3 up = new Vector3(0f, 1f, 0f);

            matrixModelview *= Matrix4.LookAt(eye, target, up);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref matrixModelview);

            #endregion

            _sun.Render();
            _earth.Render();
            _moon.Render();
            _mars.Render();

            SwapBuffers();
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

