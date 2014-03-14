using OpenTK;
using OpenTK.Graphics.OpenGL;
using Sjerrul.OpenTk.SolarSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.OpenTk.SolarSystem
{
	public class Block
	{
		private float _size;
		private Coordinates _coordinates;
        private float _rotation;
        private Color _color;
        public BoundingBox BoundingBox { get; private set; }

		public Block(float size, Color color, Coordinates coordinates)
		{
			_size = size;
            _coordinates = coordinates;
            _rotation = 0.0f;
            _color = color;
            BoundingBox = new BoundingBox(coordinates, _size);
		}

		public void Update(double elapsedTime)
		{
            _rotation = (_rotation > 360) ? 0 : (float)(_rotation + (50f * elapsedTime));
		}

		public void Render()
        {        
            GL.Color3(_color);

            GL.Translate(_coordinates.X, _coordinates.Y, _coordinates.Z);
            GL.Rotate(_rotation, 0f, 1f, 0f);

            DrawFrontFace();
            DrawBackFace();
            DrawLeftFace();
            DrawRightFace();
            DrawTopFace();
            DrawBottomFace();
            
            GL.Rotate(-_rotation, 0f, 1f, 0f);
            GL.Translate(-_coordinates.X, -_coordinates.Y, -_coordinates.Z);
        }

        private void DrawFrontFace()
        {
            //0.0, 0.0, 1.0
            GL.Color3(_color);
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Normal3(0.0, 0.0, 1.0);
                GL.Vertex3(-0.5, -0.5, 0.5);
                GL.Vertex3(0.5, -0.5, 0.5);
                GL.Vertex3(0.5, 0.5, 0.5);
                GL.Vertex3(-0.5, 0.5, 0.5);
            }
            GL.End();

            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.LineLoop);
            {
                GL.Vertex3(-0.5, -0.5, 0.5);
                GL.Vertex3(0.5, -0.5, 0.5);
                GL.Vertex3(0.5, 0.5, 0.5);
                GL.Vertex3(-0.5, 0.5, 0.5);
            }
            GL.End();
        }

        private void DrawBackFace()
        {
            //Zelfde als frontface, alleen Z *= -1
            GL.Color3(_color);
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Normal3(0.0, 0.0, 1.0);
                GL.Vertex3(-0.5, -0.5, -0.5);
                GL.Vertex3(-0.5, 0.5, -0.5);
                GL.Vertex3(0.5, 0.5, -0.5);
                GL.Vertex3(0.5, -0.5, -0.5);
                
            }
            GL.End();

            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.LineLoop);
            {
                GL.Vertex3(-0.5, -0.5, -0.5);
                GL.Vertex3(0.5, -0.5, -0.5);
                GL.Vertex3(0.5, 0.5, -0.5);
                GL.Vertex3(-0.5, 0.5, -0.5);
            }
            GL.End();
        }

        private void DrawLeftFace()
        {
            GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.AmbientAndDiffuse);
            //GL.Material(MaterialFace.Front, MaterialParameter.Specular, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
            //GL.Material(MaterialFace.Front, MaterialParameter.Emission, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
 
            GL.Color3(_color);
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Normal3(-1.0, 0.0, 0.0);
                GL.Vertex3(-0.5, -0.5, -0.5);
                GL.Vertex3(-0.5, -0.5, 0.5);
                GL.Vertex3(-0.5, 0.5, 0.5);
                GL.Vertex3(-0.5, 0.5, -0.5);

            }
            GL.End();

            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.LineLoop);
            {
                GL.Vertex3(-0.5, -0.5, -0.5);
                GL.Vertex3(-0.5, -0.5, 0.5);
                GL.Vertex3(-0.5, 0.5, 0.5);
                GL.Vertex3(-0.5, 0.5, -0.5);
            }
            GL.End();
        }

        private void DrawRightFace()
        {
            GL.Color3(_color);
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Normal3(1.0, 0.0, 0.0);
                GL.Vertex3(0.5, -0.5, -0.5);
                GL.Vertex3(0.5, 0.5, -0.5);
                GL.Vertex3(0.5, 0.5, 0.5);
                GL.Vertex3(0.5, -0.5, 0.5);
            }
            GL.End();

            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.LineLoop);
            {
                GL.Vertex3(0.5, -0.5, -0.5);
                GL.Vertex3(0.5, 0.5, -0.5);
                GL.Vertex3(0.5, 0.5, 0.5);
                GL.Vertex3(0.5, -0.5, 0.5);
            }
            GL.End();
        }

        private void DrawTopFace()
        {
            GL.Color3(_color);
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Normal3(0.0, 1.0, 0.0);
                GL.Vertex3(-0.5, 0.5, -0.5);
                GL.Vertex3(-0.5, 0.5, 0.5);
                GL.Vertex3(0.5, 0.5, 0.5);
                GL.Vertex3(0.5, 0.5, -0.5);
            }
            GL.End();

            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.LineLoop);
            {
                GL.Vertex3(-0.5, 0.5, -0.5);
                GL.Vertex3(-0.5, 0.5, 0.5);
                GL.Vertex3(0.5, 0.5, 0.5);
                GL.Vertex3(0.5, 0.5, -0.5);
            }
            GL.End();
        }

        private void DrawBottomFace()
        {
            GL.Color3(_color);
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Normal3(0.0, -1.0, 0.0);
                GL.Vertex3(-0.5, -0.5, -0.5);
                GL.Vertex3(0.5, -0.5, -0.5);
                GL.Vertex3(0.5, -0.5, 0.5);
                GL.Vertex3(-0.5, -0.5, 0.5);
            }
            GL.End();

            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.LineLoop);
            {
                GL.Vertex3(-0.5, -0.5, -0.5);
                GL.Vertex3(0.5, -0.5, -0.5);
                GL.Vertex3(0.5, -0.5, 0.5);
                GL.Vertex3(-0.5, -0.5, 0.5);
            }
            GL.End();
        }
	}
}
