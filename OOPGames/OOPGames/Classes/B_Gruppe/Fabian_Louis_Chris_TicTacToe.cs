using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class B_Paint_TTT : IB_Painter_TTT
    {
        public string Name
        {
            get
            { 
            return "Gruppe B TicTacToe";
            }
        }

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (currentField is IB_Field_TTT)
            {
                PaintTTT(canvas, (IB_Field_TTT)currentField);
            }
        }

        public void PaintTTT(Canvas canvas, IB_Field_TTT playField)
        {
            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color lineColor = Color.FromRgb(255, 0, 0);
            Brush lineStroke = new SolidColorBrush(lineColor);
            Color XColor = Color.FromRgb(0, 255, 0);
            Brush XStroke = new SolidColorBrush(XColor);
            Color OColor = Color.FromRgb(0, 0, 255);
            Brush OStroke = new SolidColorBrush(OColor);

            Line l1 = new Line() { X1 = 120, Y1 = 20, X2 = 120, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 220, Y1 = 20, X2 = 220, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 20, Y1 = 120, X2 = 320, Y2 = 120, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 20, Y1 = 220, X2 = 320, Y2 = 220, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l4);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (playField[i, j] == 1)
                    {
                        Line X1 = new Line() { X1 = 20 + (j * 100), Y1 = 20 + (i * 100), X2 = 120 + (j * 100), Y2 = 120 + (i * 100), Stroke = XStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(X1);
                        Line X2 = new Line() { X1 = 20 + (j * 100), Y1 = 120 + (i * 100), X2 = 120 + (j * 100), Y2 = 20 + (i * 100), Stroke = XStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(X2);
                    }
                    else if (playField[i, j] == 2)
                    {
                        Ellipse OE = new Ellipse() { Margin = new Thickness(20 + (j * 100), 20 + (i * 100), 0, 0), Width = 100, Height = 100, Stroke = OStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(OE);
                    }
                }
            }
        }
    }

    public class B_Field_TTT : IB_Field_TTT
    {
        int[,] _Field = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        public int this[int r, int c]
        {
            get
            { 
                return _Field[r, c];
            }
            set
            {
                _Field[r, c] = value;
            }
        }

    
        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is IB_Painter_TTT;
        }
    }
    public class B_Rules_TTT : IB_Rules_TTT
    {
        B_Field_TTT _Field = new B_Field_TTT();
        public IB_Field_TTT TTTField
        {
            get {
                return _Field;
            }
    }

        public string Name 
        {
            get
            {
                return "Gruppe B TicTacToe Regeln";
            }
        }
        public IGameField CurrentField { get { return TTTField; } }

        public bool MovesPossible
        {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (_Field[i, j] == 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

        }

        public int CheckIfPLayerWon()
        {
            for (int i = 0; i<3; i++)
            {
                //check rows
                if (_Field[i, 0] != 0)
                    {
                    if (_Field[i, 0] == _Field[i, 1] && _Field[i, 0] == _Field[i, 2])
                    {
                        return _Field[i, 0];
                    }
                }
                //check colums
                if (_Field  [0, i] != 0)
                {
                    if (_Field[0, i] ==  _Field[1, i] && _Field[0, i] == _Field[2, i])
                    {
                        return _Field[0, i];
                    }
                }
            }

            //check diagonal 1
            if (_Field[0, 0] != 0 && _Field[0, 0] == _Field[1, 1] && _Field[0, 0] == _Field[2, 2])
            {
                return _Field[0, 0];
            }

            //check diagonal 2
            if (_Field[0, 2] != 0 && _Field[0, 2] == _Field[1, 1] && _Field[0, 2] == _Field[2, 0])
            {
                return _Field[0, 2];
            }

            return -1;

        }

        public void ClearField()
        {
            _Field = new B_Field_TTT();
        }

        public void DoMove(IPlayMove move)
        {
            if(move is IB_Move_TTT)
            {
                DoMoveTTT((IB_Move_TTT)move);
            }
        }

        public void DoMoveTTT(IB_Move_TTT move)
        {
            if (move.Row >= 0 && move.Row < 3 && move.Column >= 0 && move.Column < 3)
            {
                _Field[move.Row, move.Column] = move.PlayerNumber;
            }
        }
    }

    public class B_Move_TTT : IB_Move_TTT
    {
        int _Row = 0;
        int _Column = 0;
        int _PlayerNumber = 0;

        public int PlayerNumber
        {
            get
            {
                return _PlayerNumber;
            }
        }

        public int Column
        {
            get
            {
                return _Column;
            }
        }
        public int Row
             {
            get
            {
                return _Row;
            }
}
    }
}
