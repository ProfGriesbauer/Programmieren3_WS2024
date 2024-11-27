
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            int randomRow = random.Next(1, 3);
            int randomColumn = random.Next(1, 3);

            while (true)
            {
                if (field is IA_TicTacToeField)
                {
                    IA_TicTacToeField myField = ((IA_TicTacToeField)field);

                    if (myField[randomRow, randomColumn] <= 0)
                    {
                        break;
                    }

                    else
                    {
                        randomRow = random.Next(1, 3);
                        randomColumn = random.Next(1, 3);
                    }
                }
                else
                break;
                
            }

            return new A_Move(randomRow, randomColumn, _playerNumber);
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _playerNumber = playerNumber;
        }
    }
}
