using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.OpenTk.SolarSystem
{
    class Body
    {
        private float _radius;
        private float _rotation;
        private float _rotationSpeed;
        private float _orbit;

        float[] cubeColors;

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

        float[] cube;

        public Body(float radius, float rotationSpeed, float orbit, Color color)
        {
            _radius = radius;
            _rotation = 0.0f;
            _rotationSpeed = rotationSpeed;
            _orbit = orbit;

            cube = new float[] {
			-_radius,  _radius,  _radius, // vertex[0]
			 _radius,  _radius,  _radius, // vertex[1]
			 _radius, -_radius,  _radius, // vertex[2]
			-_radius, -_radius,  _radius, // vertex[3]
			-_radius,  _radius, -_radius, // vertex[4]
			 _radius,  _radius, -_radius, // vertex[5]
			 _radius, -_radius, -_radius, // vertex[6]
			-_radius, -_radius, -_radius, // vertex[7]
		    };

            cubeColors =  new float[]  {
			    color.R, color.G, color.B, color.A,
			    color.R, color.G, color.B, color.A,
			    color.R, color.G, color.B, color.A,
			    color.R, color.G, color.B, color.A,
			    color.R, color.G, color.B, color.A,
			    color.R, color.G, color.B, color.A,
			    color.R, color.G, color.B, color.A,
			    color.R, color.G, color.B, color.A,
			
		    };
        }

        public void Update(double elapsedTime)
        {
            _rotation = (float)(_rotation + (_rotationSpeed * elapsedTime));

        }

        public void Render()
        {
            Matrix4 m = Matrix4.CreateRotationY(_rotation);

            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadMatrix(ref matrixModelview);

            GL.Rotate(_rotation, 0f, 1f, 0f);
            GL.Translate(_orbit, 0, 0);
            

            GL.VertexPointer(3, VertexPointerType.Float, 0, cube);
            GL.ColorPointer(4, ColorPointerType.Float, 0, cubeColors);
            GL.DrawElements(PrimitiveType.Triangles, 36, DrawElementsType.UnsignedByte, triangles);

            //GL.Rotate(-_rotation, 0f, 1f, 0f);
            //GL.Translate(-_orbit, 0, 0);
        }

    }
}
