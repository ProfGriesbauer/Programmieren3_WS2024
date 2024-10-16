using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LectureHouse
{
    public interface IHouse
    {
        public float GibMirDenStromVerbauchInmA();
    }
    public class House : IHouse
    {
        float _StromV = 56; //in A
        float _WasserV = 78;

        public float GetStromVerbauchInmA()
        {
            return _StromV * 1000;
        }

        public void SetStromVerbrauchInmA (float stromV) // in mA
        {
            _StromV = stromV / 1000;
            return;
        }

        public float StromVerbrauchInmA
        {
            get 
            {
                return _StromV * 1000;
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
    }
}
