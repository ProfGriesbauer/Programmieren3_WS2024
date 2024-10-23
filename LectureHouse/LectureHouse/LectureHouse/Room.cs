using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureHouse
{
    public interface IRoom
    {
        public void AlleLichterAn();

        public void AlleLichterAus();
    }
    public class Room : IRoom
    {
        IList<Light> _lights;

        public Room ()
        {
            _lights = new List<Light>();
            _lights.Add(new Light());
            _lights.Add(new Light());
            _lights.Add(new Light());
            _lights.Add(new Light());
        }
        public void AlleLichterAn()
        {
            foreach (Light l in _lights)
            {
                l.OnOff = true;
            }
            return;
        }

        public void AlleLichterAus()
        {
            foreach (Light l in _lights)
            {
                l.OnOff = true;
            }
            return;
        }
    }

    public class Light
    {
        bool _OnOff;

        public bool OnOff
        {
            get { return _OnOff; }
            set { _OnOff = value; }
        }
    }
}
