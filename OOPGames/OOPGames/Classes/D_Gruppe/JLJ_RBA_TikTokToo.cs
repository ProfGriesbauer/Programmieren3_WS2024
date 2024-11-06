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
    public class D_PainterTikTokToo : IPaintGame
    {
        public string Name { get { return "JLJs_TTTPainter"; } }

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            // Test ob Gamefield 
            if (currentField is D_FieldTikTokToo)
            {
                D_FieldTikTokToo myField = (D_FieldTikTokToo)currentField;

                // Paint funktionen 
                canvas.Children.Clear();
                Color bgColor = Color.FromRgb(255, 255, 255);
                canvas.Background = new SolidColorBrush(bgColor);
                Color lineColor = Color.FromRgb(0, 0, 0);
                Brush lineStroke = new SolidColorBrush(lineColor);
                Color XColor = Color.FromRgb(145, 44, 238);
                Brush XStroke = new SolidColorBrush(XColor);
                Color OColor = Color.FromRgb(22, 211, 245);
                Brush OStroke = new SolidColorBrush(OColor);

                Line l1 = new Line() { X1 = 120, Y1 = 20, X2 = 120, Y2 = 320, Stroke = lineStroke, StrokeThickness = 5.0 };
                canvas.Children.Add(l1);
                Line l2 = new Line() { X1 = 220, Y1 = 20, X2 = 220, Y2 = 320, Stroke = lineStroke, StrokeThickness = 5.0 };
                canvas.Children.Add(l2);
                Line l3 = new Line() { X1 = 20, Y1 = 120, X2 = 320, Y2 = 120, Stroke = lineStroke, StrokeThickness = 5.0 };
                canvas.Children.Add(l3);
                Line l4 = new Line() { X1 = 20, Y1 = 220, X2 = 320, Y2 = 220, Stroke = lineStroke, StrokeThickness = 5.0 };
                canvas.Children.Add(l4);

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (myField[i, j] == 1)
                        {
                            Line X1 = new Line() { X1 = 20 + (j * 100), Y1 = 20 + (i * 100), X2 = 120 + (j * 100), Y2 = 120 + (i * 100), Stroke = XStroke, StrokeThickness = 3.0 };
                            canvas.Children.Add(X1);
                            Line X2 = new Line() { X1 = 20 + (j * 100), Y1 = 120 + (i * 100), X2 = 120 + (j * 100), Y2 = 20 + (i * 100), Stroke = XStroke, StrokeThickness = 3.0 };
                            canvas.Children.Add(X2);
                        }
                        else if (myField[i, j] == 2)
                        {
                            Ellipse OE = new Ellipse() { Margin = new Thickness(20 + (j * 100), 20 + (i * 100), 0, 0), Width = 100, Height = 100, Stroke = OStroke, StrokeThickness = 3.0 };
                            canvas.Children.Add(OE);
                        }
                    }
                }
            }
        }
    }

    public class D_RulesTikTokToo : IGameRules
    {
        D_FieldTikTokToo _Field = new D_FieldTikTokToo();


        public string Name { get { return "JLJs_TTTRules"; } }

        public IGameField CurrentField { get { return _Field; } }

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
            for (int i = 0; i < 3; i++)
            {
                if (_Field[i, 0] > 0 && _Field[i, 0] == _Field[i, 1] && _Field[i, 1] == _Field[i, 2])
                {
                    return _Field[i, 0];
                }
                else if (_Field[0, i] > 0 && _Field[0, i] == _Field[1, i] && _Field[1, i] == _Field[2, i])
                {
                    return _Field[0, i];
                }
            }

            if (_Field[0, 0] > 0 && _Field[0, 0] == _Field[1, 1] && _Field[1, 1] == _Field[2, 2])
            {
                return _Field[0, 0];
            }
            else if (_Field[0, 2] > 0 && _Field[0, 2] == _Field[1, 1] && _Field[1, 1] == _Field[2, 0])
            {
                return _Field[0, 2];
            }

            return -1;
        }

        public void ClearField()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _Field[i, j] = 0;
                }
            }
        }


        public void DoMove(IPlayMove move)
        {
            if (move is D_MoveTikTokToo)
            {
                D_MoveTikTokToo myMove = (D_MoveTikTokToo) move;

                if (myMove.Row >= 0 && myMove.Row < 3 && myMove.Column >= 0 && myMove.Column < 3)
                {
                    _Field[myMove.Row, myMove.Column] = myMove.PlayerNumber;
                }
            } 
        }
    }

    public class D_FieldTikTokToo : IGameField
    {
        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is D_PainterTikTokToo;
        }

        int[,] _Field = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        public int this[int r, int c]
        {
            get
            {
                if (r >= 0 && r < 3 && c >= 0 && c < 3)
                {
                    return _Field[r, c];
                }
                else
                {
                    return -1;
                }
            }

            set
            {
                if (r >= 0 && r < 3 && c >= 0 && c < 3)
                {
                    _Field[r, c] = value;
                }
            }
        }
    }

    public class D_MoveTikTokToo : IRowMove, IColumnMove
    {   
        int _PlayerNumber = 0;

        int _Row = 0;

        int _Column = 0;

        public D_MoveTikTokToo(int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }

        public int Row { get { return _Row; } }

        public int Column { get { return _Column; } }

        public int PlayerNumber { get { return _PlayerNumber; } }
    }

    public class D_HumanPTikTokToo : IHumanGamePlayer
    {
        int _PlayerNumber = 0;

        public string Name { get { return "JLJs_TTTHumanPlayer"; } }

        public int PlayerNumber { get { return _PlayerNumber; } }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is D_RulesTikTokToo;
        }

        public IGamePlayer Clone()
        {
            D_HumanPTikTokToo D_TTT_HP = new D_HumanPTikTokToo();
            D_TTT_HP.SetPlayerNumber(this._PlayerNumber);
            return D_TTT_HP;
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (field is D_FieldTikTokToo)
            {

                D_FieldTikTokToo myfield = (D_FieldTikTokToo)field; 

                if (selection is IClickSelection)
                {
                    IClickSelection sel = (IClickSelection)selection;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (sel.XClickPos > 20 + (j * 100) && sel.XClickPos < 120 + (j * 100) &&
                                sel.YClickPos > 20 + (i * 100) && sel.YClickPos < 120 + (i * 100) &&
                                myfield[i, j] <= 0)
                            {
                                return new D_MoveTikTokToo(i, j, _PlayerNumber);
                            }
                        }
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }

    public class D_ComputerPTikTokToo : IComputerGamePlayer
    {
        int _PlayerNumber = 0;

        public string Name { get { return "JLJs_TTTComputerPkayer"; } }

        public int PlayerNumber { get { return _PlayerNumber; } }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is D_RulesTikTokToo;
        }

        public IGamePlayer Clone()
        {
            D_ComputerPTikTokToo D_TTT_CP = new D_ComputerPTikTokToo();
            D_TTT_CP.SetPlayerNumber(this._PlayerNumber);
            return D_TTT_CP;
        }

        public IPlayMove GetMove(IGameField field)
        {
            if (field is D_FieldTikTokToo)
            {
                D_FieldTikTokToo myfield = (D_FieldTikTokToo)field;

                Random rand = new Random();
                int f = rand.Next(0, 8);
                for (int i = 0; i < 9; i++)
                {
                    int c = f % 3;
                    int r = ((f - c) / 3) % 3;
                    if (myfield[r, c] <= 0)
                    {
                        return new D_MoveTikTokToo(r, c, _PlayerNumber);
                    }
                    else
                    {
                        f++;
                    }
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }
}
