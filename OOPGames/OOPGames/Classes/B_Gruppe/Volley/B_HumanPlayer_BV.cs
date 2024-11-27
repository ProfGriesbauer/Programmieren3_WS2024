using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace OOPGames
{
    public class B_HumanPlayer_BV : B_Player_BV 
    {
        public override string Name
        {
            get
            {
                return "Gruppe B BV HumanPlayer";
            }
        }
        public override IGamePlayer Clone()
        {
            B_HumanPlayer_BV BV_Human = new B_HumanPlayer_BV();
            BV_Human.SetPlayerNumber(this.PlayerNumber);
            return BV_Human;
        }
        public void HandleInput(Key key)
        {
            // Handle movement based on key input
            switch (key)
            {
                case Key.Left:
                case Key.A:
                    Velo_x = -5; // Move left
                    break;
                case Key.Right:
                case Key.D:
                    Velo_x = 5; // Move right
                    break;
                case Key.Up:
                case Key.W:
                    Velo_y = -5; // Jump up
                    break;
                case Key.Down:
                case Key.S:
                    Velo_y = 5; // Move down
                    break;
                default:
                    Velo_x = 0;
                    Velo_y = 0;
                    break;
            }
        }

    }
}
