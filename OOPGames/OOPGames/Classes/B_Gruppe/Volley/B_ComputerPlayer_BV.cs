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
                return "Gruppe B BV ComputerPlayer";
            }
        }
        public override IGamePlayer Clone()
        {
            B_ComputerPlayer_BV BV_Computer = new B_ComputerPlayer_BV();
            BV_Computer.SetPlayerNumber(this.PlayerNumber);
            return BV_Computer;
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
            // Simple AI: Move towards the ball horizontally
            if (field.Ball.Pos_x > Pos_x)
            {
                _MoveLeft = true; // Move left
            }
            else if (field.Ball.Pos_x < Pos_x)
            {

                _MoveRight = true; // Move right
            }

            if (field.Ball.Pos_y < field.Height * 0.8)
            {
                _Jump = true;
            }
            return new B_Move_BV(this.PlayerNumber, _MoveLeft, _MoveRight, _Jump);
        }
    }
}
