using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Sjerrul.OpenTk.SolarSystem;
using System.Collections.Generic;
using Sjerrul.OpenTk.SolarSystem.Utilities;
using System.Drawing.Drawing2D;

namespace Sjerrul.OpenTk
{
	class Program : GameWindow
    {
        IList<Block> _blocks = new List<Block>();
        Sun sun = new Sun();
        Player player = new Player();

        float _rotation = 0f;

        private float _height = 5f;
        Matrix4 matrixModelview;
        float cameraRotation = 4f;
 
		public Program()
			: base(800, 600, GraphicsMode.Default, "Hello OpenTK")
		{
            Keyboard.KeyDown += new EventHandler<KeyboardKeyEventArgs>(Keyboard_KeyDown);
            Mouse.Move += new EventHandler<MouseMoveEventArgs>(Mouse_Move);

            Random random = new Random();
            for (int x = 0; x < 5; x++)
            {
                for (int z = 0; z < 5; z++)
                {
                    int y = random.Next(0, 2);

                    _blocks.Add(new Block(0.5f, Color.Yellow, new Coordinates(x, y, z)));
                }

            }          
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

            if (e.Key == Key.Left)
            {
                cameraRotation = cameraRotation + 0.1f;
            }

            if (e.Key == Key.Right)
            {
                cameraRotation = cameraRotation - 0.1f;
            }
		}

        private void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
            if (Mouse[MouseButton.Left])
            {
                int dX = e.XDelta;
                int dY = e.YDelta;

                _height += (float)dY / 50f;
                cameraRotation += (float)dX / 50f;
            }           
        }


		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

            GL.ShadeModel(ShadingModel.Smooth);
            

            GL.ClearColor(Color.Black);

            GL.Light(LightName.Light0, LightParameter.Position, sun.Position);
            GL.Light(LightName.Light0, LightParameter.Ambient, sun.Ambient);
            GL.Light(LightName.Light0, LightParameter.Specular, sun.Specular);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.CullFace);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
            _rotation = (float)(_rotation + (e.Time));

            float x = (float)Math.Sin(_rotation);
            float y = (float)Math.Cos(_rotation);

            sun.Position = new Vector4(x, y, 0.0f, 0.0f);
            GL.Light(LightName.Light0, LightParameter.Position, sun.Position);

            foreach (Block block in _blocks)
            {
                //player.TestCollision(block.BoundingBox);
            }
            
            player.Update(e.Time);
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

            foreach (Block block in _blocks)
            {
                block.Render();
            }

            player.Render();

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

