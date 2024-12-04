using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OOPGames
{
    public class B_Rules_BV : IB_Rules_BV
    {
        bool _firstStart = true;
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
                if (CheckIfPLayerWon() < 0)
                {
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
            int _On_Ground = Field_BV.Ball.B_On_Ground(Field_BV);
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
                DoMoveBV((IB_Move_BV)move);
            }
        }

        public void DoMoveBV(IB_Move_BV move)
        {

            if (move.MoveLeft && !move.MoveRight)
            {
                Field_BV.Player[move.PlayerNumber - 1].Velo_x = -15;
            }
            else if (!move.MoveLeft && move.MoveRight)
            {
                Field_BV.Player[move.PlayerNumber - 1].Velo_x = 15;
            }
            else
            {
                Field_BV.Player[move.PlayerNumber - 1].Velo_x = 0;
            }

            //If Jump Move
            if (move.Jump)
            {
                //Check if Player is on Ground or slightly above
                if (Field_BV.Player[move.PlayerNumber - 1].Pos_y + Field_BV.Player[move.PlayerNumber - 1].Playersize / 2 >= Field_BV.Height - Field_BV.Ground.Height - 10)
                {
                    Field_BV.Player[move.PlayerNumber - 1].Velo_y = -60;
                }
            }

            //reset move Object
            move.ResetMove();
        }

        public void StartedGameCall()
        {
            _firstStart = true;
            Points[0] = 0;
            Points[1] = 0;

        }

        public void TickGameCall()
        {
            //Sets the Ball and Playerpositions at first Start
            if (_firstStart)
            {
                ScoredReset(new Random().Next(0, 2));
                _firstStart = false;
            }

            //Checks if Ball is on Ground an resets Game if so
            CheckIfPLayerScored();

            //Moves Ball and Players
            Field_BV.Ball.B_Move_Ball(Field_BV);
            Field_BV.Player[0].B_Move_Player(Field_BV);
            Field_BV.Player[1].B_Move_Player(Field_BV);

            Field_BV.Player[0].Velo_x = 0;
            Field_BV.Player[1].Velo_x = 0;

        }

        public void ScoredReset(int scorer)
        {
            //Reset Ball
            Field_BV.Ball.GravityOn = false;
            if (scorer == 0)
            {
                Field_BV.Ball.Pos_x = Field_BV.Width / 4;
            }
            else if (scorer == 1)
            {
                Field_BV.Ball.Pos_x = (Field_BV.Width / 4) * 3;
            }
            Field_BV.Ball.Pos_y = Field_BV.Height * 0.55;
            Field_BV.Ball.Velo_x = 0;
            Field_BV.Ball.Velo_y = 0;


            //Reset Players
            Field_BV.Player[0].Pos_x = Field_BV.Width / 4;
            Field_BV.Player[0].Pos_y = Field_BV.Height - Field_BV.Ground.Height;
            Field_BV.Player[0].Velo_x = 0;
            Field_BV.Player[0].Velo_y = 0;

            Field_BV.Player[1].Pos_x = Field_BV.Width / 4 * 3;
            Field_BV.Player[1].Pos_y = Field_BV.Height - Field_BV.Ground.Height;
            Field_BV.Player[1].Velo_x = 0;
            Field_BV.Player[1].Velo_y = 0;

        }
    }
}
