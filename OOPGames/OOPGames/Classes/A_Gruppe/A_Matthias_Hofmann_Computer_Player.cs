
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace OOPGames
{
    public class A_Computer_Player : IComputerGamePlayer
    {
        int _playerNumber = 0;
        public string Name
        {
           get { return "Computer Player Gruppe A"; }
        }
        

        public int PlayerNumber
        {
            get { return 2; }
        }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is A_Rules;
        }

        public IGamePlayer Clone()
        {
            A_Computer_Player clone = new A_Computer_Player();
            clone.SetPlayerNumber(_playerNumber);
            return clone;
        }

        public IPlayMove GetMove(IGameField field)
        {
            Random random = new Random();
            int randomRow = random.Next(0, 3);
            int randomColumn = random.Next(0, 3);
            int maxw = 0;
            while (maxw < 100)
            {
                maxw++;
                if (field is IA_TicTacToeField)
                {
                    IA_TicTacToeField myField = ((IA_TicTacToeField)field);

                    if (myField[randomRow, randomColumn] <= 0)
                    {
                        return new A_Move(randomRow, randomColumn, _playerNumber);
                    }

                    else
                    {
                        randomRow = random.Next(0, 3);
                        randomColumn = random.Next(0, 3);
                    }
                }
                else
                    return null;
                
            }

            return null;
            
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _playerNumber = playerNumber;
        }
    }
}
