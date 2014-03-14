using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.OpenTk.SolarSystem.Utilities
{
    public class BoundingBox
    {
        private Coordinates _coordinates;
        private float _size;

        public float Top
        {
            get {
                return (float)(_coordinates.Y + _size);
            }        
        }

        public float Left
        {
            get {
                return (float)(_coordinates.X - _size);
            }        
        }

        public float Right
        {
            get {
                return (float)(_coordinates.X + _size);
            }        
        }

        public float Bottom
        {
            get {
                return (float)(_coordinates.Y - _size);
            }        
        }

        public float Front
        {
            get
            {
                return (float)(_coordinates.Z - _size);
            }
        }

        public float Back
        {
            get
            {
                return (float)(_coordinates.Y - _size);
            }
        }
        public BoundingBox(Coordinates coordinates, float size)
        {
            _coordinates = coordinates;
            _size = size;
        }
    }
}
