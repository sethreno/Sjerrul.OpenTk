using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.OpenTk.SolarSystem
{
    public class Coordinates
    {
        public double X { get; set; }
        public double Y { get; set; }


        public Coordinates()
        {


        }
        public Coordinates(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
