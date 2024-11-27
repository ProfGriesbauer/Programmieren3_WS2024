using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using OOPGames;

namespace OOPGames
{
    public class B_Field_BV : IB_Field_BV
    {
        IB_Ground_BV _ground;
        IB_Net_BV _net;

        IB_Ball_BV _ball;

        IB_Player_BV _player1;
        IB_Player_BV _player2;

        IB_Rules_BV _rules;

        public B_Field_BV(IB_Rules_BV rules)
        {
            _ground = new B_Ground_BV();
            _net = new B_Net_BV();

            _ball = new B_Ball_BV();

            _player1 = new B_HumanPlayer_BV();
            _player2 = new B_HumanPlayer_BV();

            _rules = rules;
        }
        public double Height { get; set; }
        public double Width { get; set; }

        public IB_Ground_BV Ground
        {
            get
            {
                return _ground;
            }
            set
            {
                _ground = value;
            }
        }
        public IB_Net_BV Net
        {
            get
            {
                return _net;
            }
            set
            {
                _net = value;
            }
        }
        public IB_Ball_BV Ball
        {
            get
            {
                return _ball;
            }
            set
            {
                _ball = value;
            }
        }

        public IB_Player_BV Player1
        {
            get
            {
                return _player1;
            }
            set
            {
                _player1 = value;
            }
        }

        public IB_Player_BV Player2
        {
            get
            {
                return _player2;
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
                return _rules;
            }
        }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is IB_Painter_BV;
        }
    }
}

