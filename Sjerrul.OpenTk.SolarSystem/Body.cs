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
        private float _orbitSpeed;
        private Coordinates _coordinates;
        private float _angle = 0;
        private Body _parent;


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

        public Body(Body parent, float radius, float rotationSpeed, float orbit, float orbitSpeed, Color color)
        {
            _radius = radius;
            _rotation = 0.0f;
            _rotationSpeed = rotationSpeed;
            _orbit = orbit;
            _coordinates = new Coordinates();
            _parent = parent;
            _orbitSpeed = orbitSpeed;

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
            _rotation = (_rotation > 360) ? 0 : (float)(_rotation + (_rotationSpeed * elapsedTime));

            _angle = (float)(_angle + (_orbitSpeed * elapsedTime ));

            _coordinates.X = _parent == null ? 0 : (_orbit * Math.Cos(_angle));
            _coordinates.Y = _parent == null ? 0 : (_orbit * Math.Sin(_angle));

            if (_parent != null)
            {
                _coordinates.X = _coordinates.X + _parent._coordinates.X;
                _coordinates.Y = _coordinates.Y + _parent._coordinates.Y;
            }

        }

        public void Render()
        {
            Matrix4 m = Matrix4.CreateRotationY(_rotation);

            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadMatrix(ref matrixModelview);

          

            GL.Translate(_coordinates.X, 0, _coordinates.Y);
            GL.Rotate(_rotation, 0f, 1f, 0f);
            //GL.Rotate(-_rotation, 0f, 1f, 0f);
           
            

            GL.VertexPointer(3, VertexPointerType.Float, 0, cube);
            GL.ColorPointer(4, ColorPointerType.Float, 0, cubeColors);

            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, 1);

            GL.DrawElements(PrimitiveType.Triangles, 36, DrawElementsType.UnsignedByte, triangles);

            GL.Rotate(-_rotation, 0f, 1f, 0f);
            GL.Translate(-_coordinates.X, 0, -_coordinates.Y);
        }

    }
}
