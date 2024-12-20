﻿using System;
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

        IB_Player_BV[] _player = new IB_Player_BV[2] { null, null };

        IB_Rules_BV _rules;


        // Paint Style of the Field
        //  0 = Test Style (Rectangle and Ellipse Objects)
        //  1 = AI Grafik Style
        //  2 = Hand Drawn Comic Style
        readonly int _fieldStyle = 2;


        public B_Field_BV(IB_Rules_BV rules)
        {
            _ground = new B_Ground_BV();
            _net = new B_Net_BV();

            _ball = new B_Ball_BV();

            _rules = rules;
        }
        public double Height { get; set; }
        public double Width { get; set; }

        public int FieldStyle
        {
            get
            {
                return _fieldStyle;
            }
        }
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

