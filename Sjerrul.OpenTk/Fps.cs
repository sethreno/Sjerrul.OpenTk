using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.OpenTk
{
    public static class Fps
    {
        static double _time = 0.0, _frames = 0.0;
        static int _fps = 0;

        public static int GetFps(double time)
        {
            _time += time;
            if (_time < 1.0)
            {
                _frames++;
                return _fps;
            }
            else
            {
                _fps = (int)_frames;
                _time = 0.0;
                _frames = 0.0;
                return _fps;
            }
        }
    }
}
