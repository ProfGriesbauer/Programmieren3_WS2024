using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace OOPGames
{
    public class B_HumanPlayer_BV : B_Player_BV, IB_HumanPlayer_BV
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

        public IB_Move_BV GetMove(IB_Field_BV field, IKeySelection key)
        {
            bool _MoveLeft = false;
            bool _MoveRight = false;
            bool _Jump = false;
           
            switch (key.Key)
            {
                case Key.A:
                case Key.J:
                    _MoveLeft = true; // Move left
                    break;
                case Key.D:
                case Key.L:
                    _MoveRight = true; // Move right
                    break;
                case Key.W:
                case Key.I:
                    _Jump = true; // Jump up
                    break;
            }
            return new B_Move_BV (this.PlayerNumber, _MoveLeft, _MoveRight, _Jump );
        }
    }
}
