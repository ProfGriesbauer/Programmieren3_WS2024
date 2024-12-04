using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{
    public class B_Move_BV : IB_Move_BV
    {
        int _PlayerNumber = 0;
        bool _MoveLeft = false;
        bool _MoveRight = false;
        bool _Jump = false;

        public B_Move_BV(int playerNumber, bool moveLeft, bool moveRight, bool jump)
        {
            _PlayerNumber = playerNumber;
            _MoveLeft = moveLeft;
            _MoveRight = moveRight;
            _Jump = jump;
        }
        public int PlayerNumber
        {
            get
            {
                return _PlayerNumber;
            }
        }

        public bool MoveLeft
        {
            get
            {
                return _MoveLeft;
            }
        }
        public bool MoveRight
        {
            get
            {
                return _MoveRight;
            }
        }
        public bool Jump
        {
            get
            {
                return _Jump;
            }
        }
    }
}
