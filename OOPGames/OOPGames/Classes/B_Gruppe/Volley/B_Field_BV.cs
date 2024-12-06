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

        IB_Player_BV[] _player = new IB_Player_BV[2];

        IB_Rules_BV _rules;


        public B_Field_BV(IB_Rules_BV rules)
        {
            _ground = new B_Ground_BV();
            _net = new B_Net_BV();

            _ball = new B_Ball_BV();

            _player[0] = new B_HumanPlayer_BV();
            _player[0].SetPlayerNumber(1);
            _player[1] = new B_ComputerPlayer_BV();
            _player[1].SetPlayerNumber(2);

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

        public IB_Player_BV[] Player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
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

