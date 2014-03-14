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
        private float _height = 1.5f;
        private float _distance = -5f;
        #region Coordinates 
        
		byte[] triangles =
		{
			1, 0, 2, // front
			3, 2, 0,
			6, 4, 5, // back
			4, 6, 7,
			4, 7, 0, // left
			7, 3, 0,
			1, 2, 5, //right
			2, 6, 5,
			0, 1, 5, // top
			0, 5, 4,
			2, 3, 6, // bottom
			3, 7, 6,
		};

        int[] normals =
		{
			1, 0, 0, // front
			-1, 0, 0,
			0, -1, 0, // back
			0, 1, 0,
			0, 0, 1, // left
			0, 0, -1,			
		};

		float[] cube = {
			-0.5f,  0.5f,  0.5f, // vertex[0]
			 0.5f,  0.5f,  0.5f, // vertex[1]
			 0.5f, -0.5f,  0.5f, // vertex[2]
			-0.5f, -0.5f,  0.5f, // vertex[3]
			-0.5f,  0.5f, -0.5f, // vertex[4]
			 0.5f,  0.5f, -0.5f, // vertex[5]
			 0.5f, -0.5f, -0.5f, // vertex[6]
			-0.5f, -0.5f, -0.5f, // vertex[7]
		};
#endregion
        
        Matrix4 matrixProjection, matrixModelview;
        float cameraRotation = 0f;
 
		public Program()
			: base(800, 600, GraphicsMode.Default, "Hello OpenTK")
		{

			Keyboard.KeyDown += new EventHandler<KeyboardKeyEventArgs>(Keyboard_KeyDown);
		}

		void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Exit();

            
            if (e.Key == Key.Q)
            {
                GL.Light(LightName.Light0, LightParameter.Position, new Vector4(0, 0, 0, 1)); 
            }

            if (e.Key == Key.W)
            {
                GL.Light(LightName.Light0, LightParameter.Position, new Vector4(0, 1, 0, 1));
            }

            if (e.Key == Key.E)
            {
                GL.Light(LightName.Light0, LightParameter.Position, new Vector4(0, 0, 1, 1));
            }

            if (e.Key == Key.R)
            {
                GL.Light(LightName.Light0, LightParameter.Position, new Vector4(1, 0, 0, 1));
            }
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
            GL.ClearColor(Color.CornflowerBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
            cameraRotation = (float)(cameraRotation + e.Time); 
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

            Vector3 eye = new Vector3(0f, _height, _distance);
            Vector3 target = new Vector3(0f, 0f, 0f);
            Vector3 up = new Vector3(0f, 1f, 0f);

            matrixModelview *= Matrix4.LookAt(eye, target, up);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref matrixModelview);

            #endregion

            

            #region Draw cube

            GL.VertexPointer(3, VertexPointerType.Float, 0, cube);
            GL.NormalPointer(NormalPointerType.Int, 0, normals);
            GL.DrawElements(PrimitiveType.Triangles, 36, DrawElementsType.UnsignedByte, triangles);

            #endregion

            GL.Color3(Color.Green);
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Vertex3(1, -0.5, 1);
                GL.Vertex3(1, -0.5, -1);
                GL.Vertex3(-1, -0.5, -1);
                GL.Vertex3(-1, -0.5, 1);
            }
            GL.End();

            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Vertex3(-1, -0.51, 1);
                GL.Vertex3(-1, -0.51, -1);
                GL.Vertex3(1, -0.51, -1);
                GL.Vertex3(1, -0.51, 1);
                
               
                
            }
            GL.End();

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

