using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.OpenTk
{
    public class Hand
    {
        private int _angle;
        private int _length;

        public Hand(int length)
        {
            this._length = length;
        }

        public void Render()
        {
            GL.Color3(Color.Red);
            GL.Begin(PrimitiveType.Lines);
            {
                double degInRad = _angle * 3.1416 / 180;
                GL.Vertex2(0, 0);
                GL.Vertex2(Math.Sin(degInRad) * _length, Math.Cos(degInRad) * 170);
            }
            GL.End();


            //Minute
            //GL.Color3(Color.Red);
            //GL.Begin(PrimitiveType.Triangles);
            //{
            //    GL.Vertex2(-2, 0);
            //    GL.Vertex2(0, 100);
            //    GL.Vertex2(+2, 0);
            //}
            //GL.End();
        }

        public void IncreaseAngle(int increment)

        {
            _angle += increment;

            if (_angle > 360)
            {
                _angle = 0;
            }
        }

        public int GetAngle()
        {
            return _angle;

        }
    }
}
