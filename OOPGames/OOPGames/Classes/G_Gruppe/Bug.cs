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
    public class omm_BugPaint : OMM_BugGamePaint
    {
        public string Name { get { return "OMM_Bug_Paint"; } }
        public void PaintTicTacToeField(Canvas canvas, OMM_BugField currentField)
        {
            PaintGameField(canvas, currentField);
        }


        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (!(currentField is OMM_BugField))
            {
                return;
            }

            OMM_BugField myField = (OMM_BugField)currentField;

            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(0, 0, 0);
            canvas.Background = new SolidColorBrush(bgColor);
            Color lineColor = Color.FromRgb(255, 128, 0);
            Brush lineStroke = new SolidColorBrush(lineColor);
            Color BugColor = Color.FromRgb(0, 255, 0);
            Brush BugStroke = new SolidColorBrush(BugColor);
            Color AppelColor = Color.FromRgb(0, 0, 255);
            Brush AppleStroke = new SolidColorBrush(AppelColor);
            double thickness = 2.0;

            Line l0 = new Line() { X1 = 40, Y1 = 0, X2 = 40, Y2 = 400, Stroke = lineStroke, StrokeThickness = thickness  };
            canvas.Children.Add(l0);
            Line l1 = new Line() { X1 = 80, Y1 = 0, X2 = 80, Y2 = 400, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 120, Y1 = 0, X2 = 120, Y2 = 400, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 160, Y1 = 0, X2 = 160, Y2 = 400, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 200, Y1 = 0, X2 = 200, Y2 = 400, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 240, Y1 = 0, X2 = 240, Y2 = 400, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = 280, Y1 = 0, X2 = 280, Y2 = 400, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l6);
            Line l7 = new Line() { X1 = 320, Y1 = 0, X2 = 320, Y2 = 400, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l7);
            Line l8 = new Line() { X1 = 360, Y1 = 0, X2 = 360, Y2 = 400, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l8);
            Line l9 = new Line() { X1 = 400, Y1 = 0, X2 = 400, Y2 = 400, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l9);

            Line l10 = new Line() { X1 = 0, Y1 = 40, X2 = 400, Y2 = 40, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l10);
            Line l11 = new Line() { X1 = 0, Y1 = 80, X2 = 400, Y2 = 80, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l11);
            Line l12 = new Line() { X1 = 0, Y1 = 120, X2 = 400, Y2 = 120, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l12);
            Line l13 = new Line() { X1 = 0, Y1 = 160, X2 = 400, Y2 = 160, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l13);
            Line l14 = new Line() { X1 = 0, Y1 = 200, X2 = 400, Y2 = 200, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l14);
            Line l15 = new Line() { X1 = 0, Y1 = 240, X2 = 400, Y2 = 240, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l15);
            Line l16 = new Line() { X1 = 0, Y1 = 280, X2 = 400, Y2 = 280, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l16);
            Line l17 = new Line() { X1 = 0, Y1 = 320, X2 = 400, Y2 = 320, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l17);
            Line l18 = new Line() { X1 = 0, Y1 = 360, X2 = 400, Y2 = 360, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l18);
            Line l19 = new Line() { X1 = 0, Y1 = 400, X2 = 400, Y2 = 400, Stroke = lineStroke, StrokeThickness = thickness };
            canvas.Children.Add(l19);


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (myField[i, j] == 1)
                    {
                        Line X1 = new Line() { X1 = 20 + (j * 100), Y1 = 20 + (i * 100), X2 = 120 + (j * 100), Y2 = 120 + (i * 100), Stroke = BugStroke, StrokeThickness = 5.0 };
                        canvas.Children.Add(X1);
                        Line X2 = new Line() { X1 = 20 + (j * 100), Y1 = 120 + (i * 100), X2 = 120 + (j * 100), Y2 = 20 + (i * 100), Stroke = BugStroke, StrokeThickness = 5.0 };
                        canvas.Children.Add(X2);
                    }
                    else if (myField[i, j] == 2)
                    {
                        Ellipse OE = new Ellipse() { Margin = new Thickness(20 + (j * 100), 20 + (i * 100), 0, 0), Width = 100, Height = 100, Stroke = AppleStroke, StrokeThickness = 5.0 };
                        canvas.Children.Add(OE);
                    }
                }
            }
        }
    }
    /*
    public class oX_TicTacToeRules : OMM_BugRules
    {
        omm_BugField _Field = new omm_BugField();

        public omm_BugField TicTacToeField { get { return _Field; } }

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

        public string Name { get { return "OliverMarcusTicTacToeRules"; } }

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

        public void DoTicTacToeMove(omm_BugMove move)
        {
            if (move.Row >= 0 && move.Row < 3 && move.Column >= 0 && move.Column < 3)
            {
                _Field[move.Row, move.Column] = move.PlayerNumber;
            }
        }

        public IGameField CurrentField { get { return TicTacToeField; } }

        public void DoMove(IPlayMove move)
        {
            if (move is omm_BugMove)
            {
                DoTicTacToeMove((omm_BugMove)move);
            }
        }
    }

    public class oX_TicTacToeField : omm_BugField
    {
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

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is omm_BugPaint;
        }
    }

    public class oX_TicTacToeMove : omm_BugMove
    {
        int _Row = 0;
        int _Column = 0;
        int _PlayerNumber = 0;

        public oX_TicTacToeMove(int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }

        public int Row { get { return _Row; } }

        public int Column { get { return _Column; } }

        public int PlayerNumber { get { return _PlayerNumber; } }
    }

    public class oX_TicTacToeHumanPlayer : omm_BugHumanPlayer
    {
        int _PlayerNumber = 0;

        public string Name { get { return "OliverMarcusHumanTicTacToePlayer"; } }

        public int PlayerNumber { get { return _PlayerNumber; } }



        public IGamePlayer Clone()
        {
            X_TicTacToeHumanPlayer ttthp = new X_TicTacToeHumanPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public omm_BugMove GetMove(IMoveSelection selection, omm_BugField field)
        {
            if (selection is IClickSelection)
            {
                IClickSelection sel = (IClickSelection)selection;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (sel.XClickPos > 20 + (j * 100) && sel.XClickPos < 120 + (j * 100) &&
                            sel.YClickPos > 20 + (i * 100) && sel.YClickPos < 120 + (i * 100) &&
                            field[i, j] <= 0)
                        {
                            return new X_TicTacToeMove(i, j, _PlayerNumber);
                        }
                    }
                }
            }

            return null;
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is omm_BugRules;
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (field is omm_BugField)
            {
                return GetMove(selection, (omm_BugField)field);
            }
            else
            {
                return null;
            }
        }
    }

    public class oX_TicTacToeComputerPlayer : omm_BugComputerPlayer
    {
        int _PlayerNumber = 0;

        public string Name { get { return "OliverMarcusComputerTicTacToePlayer"; } }

        public int PlayerNumber { get { return _PlayerNumber; } }

        public IGamePlayer Clone()
        {
            X_TicTacToeComputerPlayer ttthp = new X_TicTacToeComputerPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public omm_BugMove GetMove(omm_BugField field)
        {
            Random rand = new Random();
            int f = rand.Next(0, 8);
            for (int i = 0; i < 9; i++)
            {
                int c = f % 3;
                int r = ((f - c) / 3) % 3;
                if (field[r, c] <= 0)
                {
                    return new X_TicTacToeMove(r, c, _PlayerNumber);
                }
                else
                {
                    f++;
                }
            }

            return null;
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is omm_BugRules;
        }

        public IPlayMove GetMove(IGameField field)
        {
            if (field is omm_BugField)
            {
                return GetMove((omm_BugField)field);
            }
            else
            {
                return null;
            }
        }
    }
    */
}
