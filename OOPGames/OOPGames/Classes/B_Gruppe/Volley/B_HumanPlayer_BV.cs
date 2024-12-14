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
                return "Blobby Human Player";
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
        public IPlayMove GetKeyTickMove(IDictKeySelection selection, IGameField field)
        {
            if (field is IB_Field_BV)
            {
                return GetKeyTickMoveBV((IB_Field_BV)field, selection);
            }
            else
            {
                return null;
            }
        }
        public IB_Move_BV GetMoveBV(IB_Field_BV field, IKeySelection key) { return null; }

        int _PlayerTurn = -1;
        public IB_Move_BV GetKeyTickMoveBV(IB_Field_BV field, IDictKeySelection keySelection)
        {
            bool _MoveLeft = false;
            bool _MoveRight = false;
            bool _Jump = false;
            int pn = -1;


            if (field.Player[this.PlayerNumber - 1] == null)
            {
                field.Player[this.PlayerNumber - 1] = this;
            }

            if (field.Player[0] is IHumanGamePlayer2 && field.Player[1] is IHumanGamePlayer2)
            {
                _PlayerTurn = _PlayerTurn == 1 ? 2 : 1;
            }

            if (_PlayerTurn == 1 || _PlayerTurn == -1)
            {
                if (keySelection.PressedKeys.TryGetValue(Key.W, out bool isWPressed) && isWPressed)
                {
                    pn = 1;
                    _Jump = true; // Jump up
                }
                if (keySelection.PressedKeys.TryGetValue(Key.A, out bool isAPressed) && isAPressed)
                {
                    pn = 1;
                    _MoveLeft = true; // Move left
                }
                if (keySelection.PressedKeys.TryGetValue(Key.D, out bool isDPressed) && isDPressed)
                {
                    pn = 1;
                    _MoveRight = true; // Move right
                }
            }

            if (_PlayerTurn == 2 || _PlayerTurn == -1)
            {
                if (keySelection.PressedKeys.TryGetValue(Key.J, out bool isJPressed) && isJPressed)
                {
                    pn = 2;
                    _MoveLeft = true; // Move left
                }
                if (keySelection.PressedKeys.TryGetValue(Key.L, out bool isLPressed) && isLPressed)
                {
                    pn = 2;
                    _MoveRight = true; // Move right
                }
                if (keySelection.PressedKeys.TryGetValue(Key.I, out bool isIPressed) && isIPressed)
                {
                    pn = 2;
                    _Jump = true; // Jump up
                }
            }
            return pn != -1 ? new B_Move_BV(pn, _MoveLeft, _MoveRight, _Jump) : null;
        }

    }
}
