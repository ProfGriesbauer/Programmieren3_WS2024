using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LectureHouse
{
    public interface IRoom : IEGerateBesuchbar
    {
        public void AlleLichterAn();

        public void AlleLichterAus();
    }

    public interface IRoomJalousie : IRoom
    {
        public void JalousieZu();

        public void JalousieAuf();
    }

    public class NormalerRoom : Room
    {
        bool _IstBad = false;

        public bool IstBad
        {
            get { return _IstBad; }
            set { _IstBad = value; }
        }

        public override void AllesAusUndRunter()
        {
            AlleLichterAus();
        }
    }

    public class JalousieRoom : Room, IRoomJalousie 
    {
        bool _JalousieOpen;
        public JalousieRoom()
        {
            _JalousieOpen = true;
            AlleLichterAus();
        }

        public void JalousieZu()
        {
            _JalousieOpen = false;
        }

        public void JalousieAuf()
        {
            _JalousieOpen = true;
        }

        public override void AllesAusUndRunter()
        {
            AlleLichterAus();
            JalousieZu();
        }
    }

    public abstract class Room : IRoom
    {
        protected IList<Light> _lights;

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

        public abstract void AllesAusUndRunter();

        public void willkommen(EGerateBesucher besucher)
        {
            besucher.BesucheMich(this);
            foreach (Light l in _lights)
            {
                l.willkommen(besucher);
            }
        }
    }

    public class Light : IEGerateBesuchbar
    {
        bool _OnOff;

        public bool OnOff
        {
            get { return _OnOff; }
            set { _OnOff = value; }
        }

        public void willkommen(EGerateBesucher besucher)
        {
            besucher.BesucheMich(this);
        }
    }
}
