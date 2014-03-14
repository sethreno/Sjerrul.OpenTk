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
    public class Player
    {
        public Vector4 Coordinates { get; set; }

        public Player()
        {
            this.Coordinates = new Vector4(0.0f, 2.0f, 0.0f, 1.0f);
        }

        public void Update(double elapsedTime)
        {
            UpdateCoordinates(0.0f, -(float)(elapsedTime), 0.0f);
        }

        public void Render()
        {
            GL.Translate(Coordinates.X, Coordinates.Y, Coordinates.Z);

           
            GL.Color3(Color.BlanchedAlmond);
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Normal3(0.0, 1.0, 0.0);
                GL.Vertex3(-0.3, 0.3, -0.3);
                GL.Vertex3(-0.3, 0.3, 0.3);
                GL.Vertex3(0.3, 0.3, 0.3);
                GL.Vertex3(0.3, 0.3, -0.3);
            }
            GL.End();

            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.LineLoop);
            {
                GL.Vertex3(-0.3, 0.3, -0.3);
                GL.Vertex3(-0.3, 0.3, 0.3);
                GL.Vertex3(0.3, 0.3, 0.3);
                GL.Vertex3(0.3, 0.3, -0.3);
            }
            GL.End();

            GL.Translate(-Coordinates.X, -Coordinates.Y, -Coordinates.Z);  
        }

        public void TestCollision(BoundingBox box)
        {
            double lr = this.Coordinates.X - box.Right;
            double rl = box.Left - this.Coordinates.X;
            double bt = this.Coordinates.Y - box.Top;
            double tb = box.Bottom - this.Coordinates.Y;
            double fb = this.Coordinates.Z - box.Back;
            double bf = box.Front - this.Coordinates.Z;

            if (lr > 0 || rl > 0 || bt > 0 || tb > 0 || bf > 0 || fb > 0)
                return;

            float max = (float)Math.Max(lr, Math.Max(rl, Math.Max(bt, Math.Max(tb, Math.Max(bf, fb)))));

            if (max == lr)
                UpdateCoordinates(max, 0.0f, 0.0f);
            else if (max == rl)
                UpdateCoordinates(-max, 0.0f, 0.0f);
            else if (max == bt)
                UpdateCoordinates(0.0f, max, 0.0f);
            else if (max == tb)
                UpdateCoordinates(0.0f, -max, 0.0f);
            else if (max == fb)
                UpdateCoordinates(0.0f, 0.0f, max);
            else if (max == bf)
                UpdateCoordinates(0.0f, 0.0f, -max);
        }

        private void UpdateCoordinates(float dX, float dY, float dZ)
        {
            var position = this.Coordinates;
            var v = new Vector4(dX, dY, dZ, 0.0f);
            Vector4.Add(ref position, ref v, out position);

            this.Coordinates = position;

        }
    }
}
