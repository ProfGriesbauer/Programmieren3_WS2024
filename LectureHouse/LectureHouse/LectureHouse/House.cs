using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LectureHouse
{
    public interface IEGerateBesuchbar
    {
        public void willkommen(EGerateBesucher besucher);
    }

    public class EGerateBesucher
    {
        int _Zahler;
        public void BesucheMich(IEGerateBesuchbar besuchter)
        {
            if (besuchter is Light || besuchter is JalousieRoom)
            {
                _Zahler++;
                if (_Zahler > 20)
                {
                    throw new Exception("Zu viel");
                }
            }
        }

        public int AktuellerStand { get { return _Zahler; } } 
    }

    public interface IHouse
    {
        public float GibMirDenStromVerbauchInmA();

        public bool AlleLichterMainRoom { set; }

        public void RaumHinzufuegen(IRoom room);

        public IList<IRoom> AlleRäume { get; }
    }
    public class House : IHouse, IEGerateBesuchbar
    {
        float _StromV; //in A
        float _WasserV = 88;

        IRoom _mainRoom;

        IList<IRoom> _Rooms = new List<IRoom>();

        public static double PI = 3.14157535;

        public House ()
        {
            _StromV = 99;
            _mainRoom = new NormalerRoom();
        }

        public House (float stromV, float wasserV)
        {
            _StromV = stromV;
            _WasserV = wasserV;
            _mainRoom = new NormalerRoom();
        }

        public static float GibMirPIAlsFloat ()
        {
            return (float)PI;
        }

        public float GetStromVerbauchInmA()
        {
            return _StromV * 1000;
        }

        public void SetStromVerbrauchInmA (float stromV) // in mA
        {
            _StromV = stromV / 1000;
            return;
        }

        public void SetStromVerbrauchInmA() // in mA
        {
            _StromV = 100 / 1000;
            return;
        }

        public float StromVerbrauchInmA
        {
            get 
            {
                return GetStromVerbauchInmA();
            }

            set 
            { 
                _StromV = value / 1000; 
            }
        }

        public float GibMirSinnlosenGesamtVerbrauch ()
        {
            return _StromV + _WasserV;
        }

        public float SinnloserGesamtverbrauch
        {
            get 
            {
                return _StromV + _WasserV;
            }
        }

        public bool AlleLichterMainRoom 
        {             
            set
            {
                if (value)
                {
                    _mainRoom.AlleLichterAn();
                }
                else
                {
                    _mainRoom.AlleLichterAus();
                }
            }
        }

        public IList<IRoom> AlleRäume
        {
            get
            {
                return new List<IRoom>(_Rooms);
            }
        }

        public float GibMirDenStromVerbauchInuA(House haus)
        {
            return _StromV * 1000000;
        }

        public float GibMirDenWasserverbrauch()
        {
            return _WasserV;
        }

        public float GibMirDenStromVerbauchInmA()
        {
            return _StromV * 1000;
        }

        public void RaumHinzufuegen(IRoom room)
        {
            _Rooms.Add(room);
        }

        public void willkommen(EGerateBesucher besucher)
        {
            besucher.BesucheMich(this);
            _mainRoom.willkommen(besucher);
            foreach (IRoom r in _Rooms) 
            {
                r.willkommen(besucher);
            }
        }
    }
}
