using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sjerrul.OpenTk
{
    class Clock
    {
        private Hand _seconds;
        private Hand _hour;
        private Hand _minute;

        private int _radius;
        private double _elapsedTime = 0;

        public Clock(int radius)
        {
            _radius = radius;

            _seconds = new Hand(radius - 10);
            _hour = new Hand(radius - 50);
            _minute = new Hand(radius - 10);
        }

        public void Render()
        {
            this.DrawFace();

            _seconds.Render();
            _minute.Render();
        }

        public void Update(double elapsedTime)
        {
            _elapsedTime += elapsedTime;

            if (_elapsedTime > 1)
            {
                _seconds.IncreaseAngle(360 / 60);
                if (_seconds.GetAngle() == 0)
                {
                    _minute.IncreaseAngle(360 / 60);

                }

                _elapsedTime = 0;
            }
        }

        private void DrawFace()
        {
            GL.Color3(Color.White);
            GL.Begin(PrimitiveType.TriangleFan);
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * 3.1416 / 180;
                GL.Vertex2(Math.Cos(degInRad) * _radius, Math.Sin(degInRad) * _radius);
            }
            GL.End();

            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.Lines);
            {
                // draw \
                GL.Vertex2(0, 175);
                GL.Vertex2(5, 165);

                // draw /
                GL.Vertex2(5, 175);
                GL.Vertex2(0, 165);

                // draw |
                GL.Vertex2(7, 175);
                GL.Vertex2(7, 165);

                // draw |
                GL.Vertex2(9, 175);
                GL.Vertex2(9, 165);
            }
            GL.End();

        }
    }
}
