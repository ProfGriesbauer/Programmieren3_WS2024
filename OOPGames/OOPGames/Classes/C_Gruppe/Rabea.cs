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
    public class C_Painter : IPaintGame
    {
        public string Name { get { return "RJL_Painter"; } }

        public void PaintGameField(Canvas canvas, IGameField currentField)

        {
            if (currentField is C_Field)
            {
                C_Field myField = (C_Field)currentField;

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
    public class C_Field : IGameField
    {
        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is C_Painter;
        }

            int[,] Field = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

            public int this[int r, int c]
            {
                get
                {
                    if (r >= 0 && r < 3 && c >= 0 && c < 3)
                    {
                        return Field[r, c];
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
                        Field[r, c] = value;
                    }
                }
            }
        }
    public class C_Rules : IGameRules
    {
        C_Field Field = new C_Field();


        public string Name { get { return "RJL_Rules"; } }

        public IGameField CurrentField { get { return Field; } }

        public bool MovesPossible
        {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Field[i, j] == 0)
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
                if (Field[i, 0] > 0 && Field[i, 0] == Field[i, 1] && Field[i, 1] == Field[i, 2])
                {
                    return Field[i, 0];
                }
                else if (Field[0, i] > 0 && Field[0, i] == Field[1, i] && Field[1, i] == Field[2, i])
                {
                    return Field[0, i];
                }
            }

            if (Field[0, 0] > 0 && Field[0, 0] == Field[1, 1] && Field[1, 1] == Field[2, 2])
            {
                return Field[0, 0];
            }
            else if (Field[0, 2] > 0 && Field[0, 2] == Field[1, 1] && Field[1, 1] == Field[2, 0])
            {
                return Field[0, 2];
            }

            return -1;
        }

        public void ClearField()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Field[i, j] = 0;
                }
            }
        }


        public void DoMove(IPlayMove move)
        {
            if (move is C_Move)
            {
                C_Move myMove = (C_Move) move;

                if (myMove.Row >= 0 && myMove.Row < 3 && myMove.Column >= 0 && myMove.Column < 3)
                {
                    Field[myMove.Row, myMove.Column] = myMove.PlayerNumber;
                }
            } 
        }
    }
    public class C_Move : IRowMove, IColumnMove
    {
        int _PlayerNumber = 0;

        int _Row = 0;

        int _Column = 0;

        public C_Move(int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }

        public int Row { get { return _Row; } }

        public int Column { get { return _Column; } }

        public int PlayerNumber { get { return _PlayerNumber; } }
    }
    public class C_Human : IHumanGamePlayer
    {
        int _PlayerNumber = 0;

        public string Name { get { return "RJL_HumanPlayer"; } }

        public int PlayerNumber { get { return _PlayerNumber; } }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is C_Rules;
        }

        public IGamePlayer Clone()
        {
            C_Human C_TTT_HP = new C_Human();
            C_TTT_HP.SetPlayerNumber(this._PlayerNumber);
            return C_TTT_HP;
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (field is C_Field)
            {

                C_Field myfield = (C_Field)field;

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
                                return new C_Move(i, j, _PlayerNumber);
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
    public class C_Computer : IComputerGamePlayer
    {
        int _PlayerNumber = 0;

        public string Name { get { return "RJL_ComputerPkayer"; } }

        public int PlayerNumber { get { return _PlayerNumber; } }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is C_Rules;
        }

        public IGamePlayer Clone()
        {
            C_Computer C_TTT_CP = new C_Computer();
            C_TTT_CP.SetPlayerNumber(this._PlayerNumber);
            return C_TTT_CP;
        }

        public IPlayMove GetMove(IGameField field)
        {
            if (field is C_Field)
            {
                C_Field myfield = (C_Field)field;

                Random rand = new Random();
                int f = rand.Next(0, 8);
                for (int i = 0; i < 9; i++)
                {
                    int c = f % 3;
                    int r = ((f - c) / 3) % 3;
                    if (myfield[r, c] <= 0)
                    {
                        return new C_Move(r, c, _PlayerNumber);
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
