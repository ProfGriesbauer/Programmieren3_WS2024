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
        private int[] _Points = new int[2];
        
        public B_Rules_BV()
        {
            _Field = new B_Field_BV(this);
            _Points[0] = 0;
            _Points[1] = 0;
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

        public bool MovesPossible
        {
            get
            {
                if (CheckIfPLayerWon() < 0) {
                    return true;
                }
                return false;
            }
        }

        public int CheckIfPLayerWon()
        {
            for (int i = 0; i < 2; i++)
            {
                if (Points[i] >= 10)
                {
                    return i;
                }
            }
            return -1;
        }

        public void CheckIfPLayerScored()
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
            if (move is B_Move_BV)
            {
                
            }
        }

        public void StartedGameCall()
        {
            ScoredReset(new Random().Next(0, 2));
        }

        public void TickGameCall()
        {
            CheckIfPLayerScored();
        }

        public void ScoredReset(int scorer)
        {
            //Reset Ball
            if (scorer == 0)
            {
                Field_BV.Ball.Pos_x = Field_BV.Width / 4;
            }
            else if (scorer == 1)
            {
                Field_BV.Ball.Pos_x = (Field_BV.Width / 4)*3;
            }
            Field_BV.Ball.Pos_y = Field_BV.Height*0.7;
            Field_BV.Ball.Velo_x = 0;
            Field_BV.Ball.Velo_y = 0;


            //Reset Players
            Field_BV.Player1.Pos_x = Field_BV.Width / 4;
            Field_BV.Player1.Pos_y = 0;
            Field_BV.Player1.Velo_x = 0;
            Field_BV.Player1.Velo_y = 0;

            Field_BV.Player2.Pos_x = Field_BV.Width / 4;
            Field_BV.Player2.Pos_y = 0;
            Field_BV.Player2.Velo_x = 0;
            Field_BV.Player2.Velo_y = 0;

        }
    }
}
