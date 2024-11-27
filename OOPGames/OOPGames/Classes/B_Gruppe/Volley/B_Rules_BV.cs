using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{
    public class B_Rules_BV : IB_Rules_BV
    {
        public string Name => "Blobby Volley Rules";

        private B_Field_BV _Field;
        private int[] _Points;

        public B_Rules_BV()
        {
            _Field = new B_Field_BV(this);
            //_Points[0] = 0;
            //_Points[1] = 0;
        }
        public IB_Field_BV Field_BV
        {
            get
            {
                return _Field;
            }
        }
        public IGameField CurrentField
        {
            get
            {
                return Field_BV;
            }
        }


        public int[] Points
        {
            get
            {
                return _Points;
            }

            set 
            {
                _Points = value;
            }

        }



        public bool MovesPossible => throw new NotImplementedException();

        public int CheckIfPLayerWon()
        {
            for (int i = 0; i < 2; i++)
            {
                if (Points[i] >= 10)
                {
                    return i;
                }
            }
            return 0;
        }

        public void CheckIfPLayerScored(IB_Field_BV Field_BV)
        {
            int _On_Ground = Field_BV.Ball.B_On_Ground();
            if (_On_Ground >= 0)
            {
                Points[_On_Ground]++;
                ScoredReset(_On_Ground);
            }

        }

        public void ClearField()
        {
            _Field = new B_Field_BV(this);
        }

        public void DoMove(IPlayMove move)
        {
            throw new NotImplementedException();
        }

        public void StartedGameCall()
        {
            throw new NotImplementedException();
        }

        public void TickGameCall()
        {
            throw new NotImplementedException();
        }

        public void ScoredReset(int scorer)
        {
            throw new NotImplementedException();
        }
    }
}
