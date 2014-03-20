using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.OpenTk
{
    public class Cell
    {
        private int _id;
        private bool _isHovering;
        private Color _color;
        private Vector2 _topLeftCoordinates;
        private float _size = 50f;

        public Cell(int id, Color color, Vector2 topLeft)
        {
            _id = id;
            _color = color;
            _topLeftCoordinates = topLeft;
        }

        public void Render()
        {
            //Normal rendering uses the cells normal color
            GL.Color3(_color);

            if (_isHovering)
            {
                GL.Color3(Color.Red);
            }

            GL.Begin(PrimitiveType.Quads);
            {
                GL.Vertex3(_topLeftCoordinates.X, _topLeftCoordinates.Y, 0);
                GL.Vertex3(_topLeftCoordinates.X + _size, _topLeftCoordinates.Y, 0);
                GL.Vertex3(_topLeftCoordinates.X + _size, _topLeftCoordinates.Y + _size, 0);
                GL.Vertex3(_topLeftCoordinates.X, _topLeftCoordinates.Y + _size, 0);
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

        /// <summary>
        /// Called when we need to do mousepicking. Use the Id of the square to generate a color. The mouse will 
        /// sample this and convert the color back in an Id to get this exact celll
        /// </summary>
        public void RenderPickingMode()
        {
            //Convert the id into RGB
            int r = (_id & 0x000000FF) >> 0;
            int g = (_id & 0x0000FF00) >> 8;
            int b = (_id & 0x00FF0000) >> 16;

            //Render the Cell with the Id-generated color
            GL.Color3(Color.FromArgb(1, r, g, b));
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Vertex3(_topLeftCoordinates.X, _topLeftCoordinates.Y, 0);
                GL.Vertex3(_topLeftCoordinates.X + _size, _topLeftCoordinates.Y, 0);
                GL.Vertex3(_topLeftCoordinates.X + _size, _topLeftCoordinates.Y + _size, 0);
                GL.Vertex3(_topLeftCoordinates.X, _topLeftCoordinates.Y + _size, 0);
            }
            GL.End();
        }

        /// <summary>
        /// Set wheter or not this cell is being hovered over 
        /// </summary>
        /// <param name="isHovering">true if being hovered over, false otherwise</param>
        public void SetHover(bool isHovering)
        {
            _isHovering = isHovering;
        }
    }
}
