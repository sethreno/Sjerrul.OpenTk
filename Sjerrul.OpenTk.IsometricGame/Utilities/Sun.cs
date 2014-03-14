using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.OpenTk.SolarSystem.Utilities
{
    public class Sun
    {
        public Vector4 Position { get; set; }
        public Vector4 Diffuse { get; set; }
        public Vector4 Ambient { get; set; }
        public Vector4 Specular { get; set; }

        public Sun()
        {
            this.Position = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
            this.Ambient = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            this.Specular = new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
            this.Diffuse = new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
}
