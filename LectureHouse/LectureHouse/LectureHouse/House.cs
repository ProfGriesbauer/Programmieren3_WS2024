using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LectureHouse
{
    public class House
    {
        float _StromV = 56; //in A

        public float GibMirDenStromVerbauchInmA (House haus)
        {
            return _StromV*1000;
        }
    }
}
