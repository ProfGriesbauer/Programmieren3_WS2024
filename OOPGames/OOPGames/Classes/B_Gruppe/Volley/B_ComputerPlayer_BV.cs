using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public class B_ComputerPlayer_BV : B_Player_BV, IB_ComputerPlayer_BV
    {
        public override string Name
        {
            get
            {
                return "Blobby Computer Player";
            }
        }

        public override IGamePlayer Clone()
        {
            B_ComputerPlayer_BV computerPlayer = new B_ComputerPlayer_BV();
            computerPlayer.SetPlayerNumber(this.PlayerNumber);
            return computerPlayer;
        }

        public IPlayMove GetMove(IGameField field)
        {
            if (field is IB_Field_BV)
            {
                return GetMoveBV((IB_Field_BV)field);
            }
            else
            {
                return null;
            }
        }

        public IB_Move_BV GetMoveBV(IB_Field_BV field)
        {
            bool _MoveLeft = false;
            bool _MoveRight = false;
            bool _Jump = false;

            // Ziel: Folge dem Ball
            if (field.Ball.Pos_x < Pos_x - 10) // Ball links vom Spieler
            {
                _MoveLeft = true;
            }
            else if (field.Ball.Pos_x >= Pos_x) // Ball rechts vom Spieler
            {
                _MoveRight = true;
            }

            // Logik für das Springen:
            // Springe nur, wenn der Ball über dem Spieler ist und er in Reichweite ist
            if (Math.Abs(field.Ball.Pos_x - Pos_x) < 50 && field.Ball.Pos_y < Pos_y)
            {
                _Jump = true;
            }

            return new B_Move_BV(this.PlayerNumber, _MoveLeft, _MoveRight, _Jump);
        }
    }
}
