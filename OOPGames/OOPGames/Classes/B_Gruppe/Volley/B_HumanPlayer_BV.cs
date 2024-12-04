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

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (selection is IKeySelection && field is IB_Field_BV)
            {
                return GetMoveBV((IB_Field_BV)field, (IKeySelection)selection);
            }
            else
            {
                return null;
            }
        }

        public IB_Move_BV GetMoveBV(IB_Field_BV field, IKeySelection key)
        {
            bool _MoveLeft = false;
            bool _MoveRight = false;
            bool _Jump = false;
            int pn = -1;


            switch (key.Key)
            {
                case Key.A:
                    pn = 1;
                    _MoveLeft = true; // Move left
                    break;
                case Key.D:
                    pn = 1;
                    _MoveRight = true; // Move right
                    break;
                case Key.W:
                    pn = 1;
                    _Jump = true; // Jump up
                    break;
            }

            switch (key.Key)
            {
                case Key.J:
                    pn = 2;
                    _MoveLeft = true; // Move left
                    break;
                case Key.L:
                    pn = 2;
                    _MoveRight = true; // Move right
                    break;
                case Key.I:
                    pn = 2;
                    _Jump = true; // Jump up
                    break;
            }


            return pn != -1 ? new B_Move_BV(pn, _MoveLeft, _MoveRight, _Jump) : null;
        }
    }
}
