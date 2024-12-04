
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public class A_Human_Player : IHumanGamePlayer
    {
       
        int _playerNumber = 0;
        public string Name
        {
            get { return "Human Player Gruppe A"; }
        }

        public int PlayerNumber
        {
            get { return 1; }
        }


        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is A_Rules;
        }

        
        public IGamePlayer Clone()
        {
            A_Human_Player clone = new A_Human_Player();
            clone.SetPlayerNumber(_playerNumber);
            return clone;

        }


        
        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            
            if (selection is IClickSelection &&
                field is IA_TicTacToeField)
            {
                IA_TicTacToeField myfield = (IA_TicTacToeField)field;
                IClickSelection sel = (IClickSelection)selection;
                for (int row = 0; row <= myfield.rows-1; row++)
                {
                    for (int column = 0; column <= myfield.columns-1; column++)
                    {
                        if (sel.XClickPos < (column+1) * myfield.cellWidth && sel.YClickPos < (row+1) * myfield.cellHeight&&
                           myfield[row, column] <= 0)
                        {
                            return new A_Move (row, column, _playerNumber);
                        }
                    }
                }
            }
            return null;
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _playerNumber = playerNumber;
        }
    }
}
