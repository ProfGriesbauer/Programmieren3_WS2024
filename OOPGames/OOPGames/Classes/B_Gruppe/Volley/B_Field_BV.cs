using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{
    internal class B_Field_BV : IB_Field_BV
    {
        private double _height;
        private double _width;
        private IB_Ball_BV _ball;
        private IB_Player_BV _player1;
        private IB_Player_BV _player2;
        private IB_Rules_BV _rules;

        public B_Field_BV(IB_Rules_BV rules)
            _height = 600;
            _width = 600;
            _ball = new B_Ball_BV();
            _player1 = new B_Player_BV();
            _player2 =new B_Player_BV();
          

        public B_Field_BV(IB_Rules_BV rules)
        {
            _rules = rules;
        }
        public double Height {
            get { 
                return _height;
            };
            set
            {
                _height = value;    
            } 
        }
        public double Width { get
            {
                return _width;
            }; 
            set
            {
                _width = value; 
            } 
        }

        public double MarginPercentage = 0,1;

        public IB_Ball_BV Ball
        {
            get
            {
                return (IB_Ball_BV)_ball;
            }
            set
            {
                _ball = value;
            }
        }
        public IB_Player_BV Player1 { get
            {
                return _player1;
            };
            set
            {
                _player1 = value;
            }
        }
        public IB_Player_BV Player2 { get
            {
                return _player2
            }
            set
            {
                _player2 = value;
            } 
        }

        public IB_Rules_BV Rules_BV
        {
            get
            {
                return _rules
            }
        }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is IB_Player_BV;
        }
    }
}
