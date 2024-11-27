using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public class B_ComputerPlayer_BV : B_Player_BV
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

        public void UpdatePositionToTrackBall(IB_Ball_BV ball)
        {
            // Simple AI: Move towards the ball horizontally
            if (ball.Pos_x > Pos_x + 10)
            {
                Velo_x = 5; // Move right
            }
            else if (ball.Pos_x < Pos_x - 10)
            {
                Velo_x = -5; // Move left
            }
            else
            {
                Velo_x = 0; // Stay still if close enough to the ball
            }
        }
    }
}
