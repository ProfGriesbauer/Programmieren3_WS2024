using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{
    public class A_Rules : IGameRules
    {
        //Erzeugen eines Feld Objektes
        A_Field _field = new A_Field(3, 3);


        public string Name
        {
            get { return "Rules Gruppe A"; }
        }

        //Iterriert über jedes Feld im Array 
        //Steht eine 0 als Wert im Array ist das Feld noch frei und kann beschrieben werden
        public bool MovesPossible
        {
            get
            {
                for (int row = 0; row < _field.rows; row++)
                {
                    for (int column = 0; column < _field.columns; column++)
                    {
                        if (_field[row, column] == 0)
                            return true;
                    }

                }
                return false;
            }
        }


        public IGameField CurrentField    
        { 
            get { return _field; }
        }

        

        public int CheckIfPLayerWon()
        {
            
            for (int i = 0; i < 3; i++)
            {
                if (_field[i, 0] > 0 && _field[i, 0] == _field[i, 1] && _field[i, 1] == _field[i, 2])
                {
                    return _field[i, 0];
                }
                else if (_field[0, i] > 0 && _field[0, i] == _field[1, i] && _field[1, i] == _field[2, i])
                {
                    return _field[0, i];
                }
            }

            if (_field[0, 0] > 0 && _field[0, 0] == _field[1, 1] && _field[1, 1] == _field[2, 2])
            {
                return _field[0, 0];
            }
            else if (_field[0, 2] > 0 && _field[0, 2] == _field[1, 1] && _field[1, 1] == _field[2, 0])
            {
                return _field[0, 2];
            }

            return -1;

        }

        public void ClearField()
        {
            for (int row = 0; row < _field.rows; row++)
            {
                for (int column = 0; column < _field.columns; column++)
                {
                    _field[row, column] = 0;
                }
            }
        }

        public void DoMove(IPlayMove move)
        {
            if (move is A_Move)
            {
                //Cast durchführen um zu zeigen das move von A_Move ist, somit kann auf Row und Column zugegriffen werden
                A_Move mymove = (A_Move) move;
                //Überprüfen ob der eingegebe Wert zulässig ist if()
                _field[mymove.Row, mymove.Column] = mymove.PlayerNumber;
            }
        }
    }
}
