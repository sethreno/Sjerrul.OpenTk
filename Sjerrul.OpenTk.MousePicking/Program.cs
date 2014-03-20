using System;
using System.Drawing;
using System.Collections;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using OpenTK.Input;
using QuickFont;
using System.Collections.Generic;

namespace Sjerrul.OpenTk
{
    public class Program : GameWindow
    {
        Dictionary<int, Cell> cells = new Dictionary<int, Cell>();
        QFont mainText;

        public Program()
            : base(800, 600, GraphicsMode.Default, "Hello OpenTK")
        {
            //Create 5 cells, and stagger them
            for (int i = 0; i < 5; i++)
            {
                Cell cell = new Cell(i, Color.AliceBlue, new Vector2(i * 50f, i * 50f));
                cells.Add(i, cell);
            }

            //Add listeners
            Keyboard.KeyDown += new EventHandler<KeyboardKeyEventArgs>(Keyboard_KeyDown);
            Mouse.Move += new EventHandler<MouseMoveEventArgs>(Mouse_Move);
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Exit();
        }

        void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
            //We are going to check if we are hovering over a cell, so first reset all cells
            foreach (var kvp in cells)
            {
                kvp.Value.SetHover(false);
            }

            //Draw the "Picking Scene (White background and square color based on ID)" on the backbuffer 
            drawPicking();

            byte[] pixel = new byte[3];
            // Read pixel color, remember: Flip Y-axis (Windows <-> OpenGL)
            GL.ReadPixels(Mouse.X, Height - Mouse.Y, 1, 1, PixelFormat.Rgb, PixelType.UnsignedByte, pixel);

            //Convert the color beneath the mouse back to the id
            int id = (int)pixel[0] + (((int)pixel[1]) << 8) + ((((int)pixel[2]) << 16));

            if (cells.ContainsKey(id))
            {
                //We can access the cell straight from the dictionary using the Id
                Cell cell = cells[id];
                cell.SetHover(true);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Black);
            GL.Disable(EnableCap.DepthTest);

            //Set Font settings
            var builderConfig = new QFontBuilderConfiguration(false);
            mainText = new QFont("Fonts/times.ttf", 14, builderConfig);
            mainText.Options.Colour = new Color4(1f, 1f, 1f, 1.0f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
           
        }

        protected override void OnResize(EventArgs e)
        {
            // Standard OpenTK code for window resize
            base.OnResize(e);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Ortho(-this.Width / 2, this.Width / 2, -this.Height / 2, this.Height / 2, -1, 1); // 0,0 in center
            GL.Viewport(0, 0, this.Width, this.Height);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            //During regular rendering, we render the normal scene
            drawRegular();
        }

        [STAThread]
        public static void Main()
        {
            using (Program p = new Program())
            {
                p.Run(80f);
            }
        }

        /// <summary>
        /// Draw the regular scene. This is what the user sees. That's why this ends with SwapBuffers()
        /// </summary>
        void drawRegular()
        {
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushAttrib(AttribMask.ColorBufferBit); //Needed so text.Print() doesn't clear the screen
            QFont.Begin();
                GL.PushMatrix();
                    GL.Translate(30f, 0f, 0f);
                    mainText.Print("Test", QFontAlignment.Centre);
                GL.PopMatrix();
            QFont.End();
            GL.PopAttrib();

            //Render each Square
            foreach (KeyValuePair<int, Cell> kvp in cells)
            {
                kvp.Value.Render();
            }

            SwapBuffers();
        }

        void drawPicking()
        {
            // Disable some caps (we want the flat / raw objects)
            GL.PushAttrib(AttribMask.EnableBit | AttribMask.ColorBufferBit);
            GL.Disable(EnableCap.Fog);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Dither);
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.LineStipple);
            GL.Disable(EnableCap.PolygonStipple);
            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.AlphaTest);

            // Clear the buffer
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Render the squares in picking mode (Using their Id to derive a color)
            foreach (KeyValuePair<int, Cell> kvp in cells)
            {
                kvp.Value.RenderPickingMode();
            }

            GL.PopAttrib();
        } 
    }
}

