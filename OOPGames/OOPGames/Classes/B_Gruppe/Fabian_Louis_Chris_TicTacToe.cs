using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace OOPGames
{
    public class B_Paint_TTT : IB_Painter_TTT
    {
        double _CanvasHeigth;
        double _CanvasWidth;
        private readonly double _MarginPercentage = 0.1;
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
            
            playField.Height = canvas.ActualHeight;
            playField.Width = canvas.ActualWidth;

            double _maxPossibleSquare = Math.Min(playField.Height, playField.Width);
            double _Margin = _maxPossibleSquare * playField.MarginPercentage;
            double _maxSquare = _maxPossibleSquare - 2*_Margin;
            double _sq = _maxSquare / 3;
            double _SymbolSizeX = 0.65*_sq;
            double _SymbolSizeO = 0.75 * _sq;

            canvas.Children.Clear();
            Color bgColor = Color.FromArgb(255, 5, 5, 5);
            canvas.Background = new SolidColorBrush(bgColor);
            Color lineColor = Color.FromArgb(255, 255, 223, 93);
            Brush lineStroke = new SolidColorBrush(lineColor);
            Color XColor = Color.FromArgb(255, 2, 134, 255);
            Brush XStroke = new SolidColorBrush(XColor);
            Color OColor = Color.FromArgb(255, 255, 2, 134);
            Brush OStroke = new SolidColorBrush(OColor);
            Color WinColor = Color.FromArgb(255, 0, 204, 102);
            Brush WinStroke = new SolidColorBrush(WinColor);

            Line l1 = new Line() { X1 = _Margin + _sq, Y1 = _Margin, X2 = _Margin + _sq, Y2 = _Margin + _maxSquare, Stroke = lineStroke, StrokeThickness = _sq / 40 };
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = _Margin + 2 * _sq, Y1 = _Margin, X2 = _Margin + 2 * _sq, Y2 = _Margin + _maxSquare, Stroke = lineStroke, StrokeThickness = _sq / 40 };
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = _Margin, Y1 = _Margin + _sq, X2 = _Margin + _maxSquare, Y2 = _Margin + _sq, Stroke = lineStroke, StrokeThickness = _sq / 40 };
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = _Margin, Y1 = _Margin + 2 * _sq, X2 = _Margin + _maxSquare, Y2 = _Margin + 2 * _sq, Stroke = lineStroke, StrokeThickness = _sq / 40 };
            canvas.Children.Add(l4);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (playField[i, j] == 1)
                    {
                        Line X1 = new Line()
                        { 
                            X1 = _Margin + (_sq - _SymbolSizeX) / 2 + (j * _sq), 
                            Y1 = _Margin + (_sq - _SymbolSizeX) / 2 + (i * _sq), 
                            X2 = _Margin + (_sq - _SymbolSizeX) / 2 + (j * _sq) + _SymbolSizeX, 
                            Y2 = _Margin + (_sq - _SymbolSizeX) / 2 + (i * _sq) + _SymbolSizeX, 
                            Stroke = XStroke, 
                            StrokeThickness = _sq/18 
                        };
                        canvas.Children.Add(X1);

                        Line X2 = new Line() 
                        { 
                            X1 = _Margin + (_sq - _SymbolSizeX) / 2 + (j * _sq) + _SymbolSizeX, 
                            Y1 = _Margin + (_sq - _SymbolSizeX) / 2 + (i * _sq), 
                            X2 = _Margin + (_sq - _SymbolSizeX) / 2 + (j * _sq), 
                            Y2 = _Margin + (_sq - _SymbolSizeX) / 2 + (i * _sq) + _SymbolSizeX, 
                            Stroke = XStroke, 
                            StrokeThickness = _sq/18 
                        };
                        canvas.Children.Add(X2);
                    }
                    else if (playField[i, j] == 2)
                    {
                        Ellipse OE = new Ellipse() 
                        { 
                            Margin = new Thickness(_Margin + (_sq-_SymbolSizeO)/2 + (j * _sq), _Margin + (_sq - _SymbolSizeO) / 2 + (i * _sq), 0, 0),
                            Width = _SymbolSizeO, 
                            Height = _SymbolSizeO, 
                            Stroke = OStroke, 
                            StrokeThickness = _sq/20
                        };
                        canvas.Children.Add(OE);
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                //check rows
                if (playField[i, 0] != 0)
                {
                    if (playField[i, 0] == playField[i, 1] && playField[i, 0] == playField[i, 2])
                    {
                        double _Width = 1.1 * _maxSquare;
                        double _Height = 1.3 * _sq;

                        Ellipse EWinRow = new Ellipse()
                        {
                            Width = _Width,
                            Height = _Height,
                            Stroke = WinStroke,
                            StrokeThickness = _sq / 30,
                            RenderTransform = new TranslateTransform(_Margin-(_Width-_maxSquare)/2, _Margin - (_Height - _sq) / 2 + i*_sq)
                        };
                        canvas.Children.Add(EWinRow);
                    }
                }
                //check colums
                if (playField[0, i] != 0)
                {
                    if (playField[0, i] == playField[1, i] && playField[0, i] == playField[2, i])
                    {
                        double _Width = 1.3 * _sq;
                        double _Height = 1.1 * _maxSquare;

                        Ellipse EWinColum = new Ellipse()
                        {
                            Width = _Width,
                            Height = _Height,
                            Stroke = WinStroke,
                            StrokeThickness = _sq / 30,
                            RenderTransform = new TranslateTransform(_Margin - (_Width - _sq) / 2 + i * _sq, _Margin - (_Height - _maxSquare) / 2)
                        };
                       
                        canvas.Children.Add(EWinColum);
                    }
                }
            }

            //check diagonal 1
            if (playField[0, 0] != 0 && playField[0, 0] == playField[1, 1] && playField[0, 0] == playField[2, 2])
            {
                double _Width = 1.4 * _maxSquare;
                double _Height = 1.3 * _sq;

                Ellipse EWinDiagonal1 = new Ellipse()
                {
                    Width = _Width,
                    Height = _Height,
                    Stroke = WinStroke,
                    StrokeThickness = _sq / 30,
                };

                // Create a TransformGroup to combine transformations
                TransformGroup transformGroup = new TransformGroup();
                transformGroup.Children.Add(new TranslateTransform(_Margin - (_Width - _maxSquare) / 2, _Margin - (_Height - _sq) / 2 + _sq));
                transformGroup.Children.Add(new RotateTransform(45, _maxPossibleSquare / 2, _maxPossibleSquare / 2));

                // Assign the combined transformation to the Ellipse
                EWinDiagonal1.RenderTransform = transformGroup;

                // Add the Ellipse to the canvas
                canvas.Children.Add(EWinDiagonal1);

            }

            //check diagonal 2
            if (playField[0, 2] != 0 && playField[0, 2] == playField[1, 1] && playField[0, 2] == playField[2, 0])
            {
                double _Width = 1.4 * _maxSquare;
                double _Height = 1.2 * _sq;

                Ellipse EWinDiagonal2 = new Ellipse()
                {
                    Width = _Width,
                    Height = _Height,
                    Stroke = WinStroke,
                    StrokeThickness = _sq / 30,
                };

                // Create a TransformGroup to combine transformations
                TransformGroup transformGroup = new TransformGroup();
                transformGroup.Children.Add(new TranslateTransform(_Margin - (_Width - _maxSquare) / 2, _Margin - (_Height - _sq) / 2 + _sq));
                transformGroup.Children.Add(new RotateTransform(-45, _maxPossibleSquare / 2, _maxPossibleSquare / 2));

                // Assign the combined transformation to the Ellipse
                EWinDiagonal2.RenderTransform = transformGroup;

                // Add the Ellipse to the canvas
                canvas.Children.Add(EWinDiagonal2);
            }

        }
    }
    

    public class B_Field_TTT : IB_Field_TTT
    {
        double _CanvasHeigth;
        double _CanvasWidth;
        private readonly double _MarginPercentage = 0.1;

        int[,] _Field = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        IB_Rules_TTT _Rules;
        public B_Field_TTT (IB_Rules_TTT rules)
        {
            _Rules = rules; 
        }

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

        public double Height
        {
            get
            {
                return _CanvasHeigth;
            }
            set
            {
                _CanvasHeigth = value;
            }
        }

        public double Width
        {
            get
            {
                return _CanvasWidth;
            }
            set
            {
                _CanvasWidth = value;
            }
        }

        public double MarginPercentage
        {
            get
            {
                return _MarginPercentage;
            }
        }

        public IB_Rules_TTT Rules_TTT
        {
            get { return _Rules; }
        }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is IB_Painter_TTT;
        }
    }
    public class B_Rules_TTT : IB_Rules_TTT
    {
        B_Field_TTT _Field;

        public B_Rules_TTT()
        {
            _Field = new B_Field_TTT(this);
        }
        public IB_Field_TTT TTTField
        {
            get
            {
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
            for (int i = 0; i < 3; i++)
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
                if (_Field[0, i] != 0)
                {
                    if (_Field[0, i] == _Field[1, i] && _Field[0, i] == _Field[2, i])
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
            _Field = new B_Field_TTT(this);
        }

        public void DoMove(IPlayMove move)
        {
            if (move is IB_Move_TTT)
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

        public B_Move_TTT(int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }
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

    public abstract class B_BasePlayer : IGamePlayer
    {
        int _PlayerNumber = 0;
        public abstract string Name
        {
            get;
        }

        public int PlayerNumber
        {
            get
            {
                return _PlayerNumber;
            }
        }
        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is IB_Rules_TTT;
        }

        public abstract IGamePlayer Clone();

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }

    public class B_ComputerPlayer_TTT : B_BasePlayer, IB_ComputerPlayer_TTT
    {
        public override string Name
        {
            get
            {
                return "Gruppe B TicTacToe ComputerPlayer";
            }
        }

        public override IGamePlayer Clone()
        {
            B_ComputerPlayer_TTT TTT_Computer = new B_ComputerPlayer_TTT();
            TTT_Computer.SetPlayerNumber(this.PlayerNumber);
            return TTT_Computer;
        }

        public IB_Move_TTT GetTTTMove(IB_Field_TTT field)
        {


            Random rand = new Random();
            int f = rand.Next(0, 8);
            for (int i = 0; i < 9; i++)
            {
                int c = f % 3;
                int r = ((f - c) / 3) % 3;
                if (field[r, c] <= 0)
                {
                    return new B_Move_TTT(r, c, this.PlayerNumber);
                }
                else
                {
                    f++;
                }
            }

            return null;
        }

        public IPlayMove GetMove(IGameField field)
        {
            if (field is IB_Field_TTT)
            {
                return GetTTTMove((IB_Field_TTT)field);
            }
            else
            {
                return null;
            }
        }

    }


    //Erstellt eine Klasse B_ComputerPlayerSchlau_TTT aus der Abstrakten Klasse B_BasePlayer
    //und der Interface Klasse IB_ComputerPlayerSchlau_TTT
    public class B_ComputerPlayerSchlau_TTT : B_BasePlayer, IB_ComputerPlayer_TTT
    {

        //Überschreibt den Angezeigten Name der Klasse im Auswahlbereich
        public override string Name
        {
            get
            {
                return "Gruppe B TicTacToe ComputerPlayerSchlau";
            }
        }

        //Erstellt ein neues Objekt für einen ComputerPlayer
        public override IGamePlayer Clone()
        {
            B_ComputerPlayerSchlau_TTT TTT_ComputerSchlau = new B_ComputerPlayerSchlau_TTT();
            TTT_ComputerSchlau.SetPlayerNumber(this.PlayerNumber);
            return TTT_ComputerSchlau;
        }


        public IPlayMove GetMove(IGameField field)
        {
            if (field is IB_Field_TTT)
            {
                return GetTTTMove((IB_Field_TTT)field);
            }
            else
            {
                return null;
            }
        }

        private int Minimax(int[,] board, int depth, bool isMaximizing)
        {
            int winner = CheckWinner(board);
            if (winner != 0)
            {
                return winner == this.PlayerNumber ? 1 : -1;
            }

            if (IsBoardFull(board)) return 0;

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int r = 0; r < 3; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        if (board[r, c] == 0)
                        {
                            board[r, c] = this.PlayerNumber;
                            int score = Minimax(board, depth + 1, false);
                            board[r, c] = 0;
                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                int opponent = this.PlayerNumber == 1 ? 2 : 1;
                for (int r = 0; r < 3; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        if (board[r, c] == 0)
                        {
                            board[r, c] = opponent;
                            int score = Minimax(board, depth + 1, true);
                            board[r, c] = 0;
                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
        }

        public IB_Move_TTT GetTTTMove(IB_Field_TTT field)
        {
            int[,] board = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = field[i, j];
                }
            }

            int bestScore = int.MinValue;
            (int, int) bestMove = (-1, -1);

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (board[r, c] == 0)
                    {
                        board[r, c] = this.PlayerNumber;
                        int score = Minimax(board, 0, false);
                        board[r, c] = 0;

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = (r, c);
                        }
                    }
                }
            }

            return new B_Move_TTT(bestMove.Item1, bestMove.Item2, this.PlayerNumber);

            //return null;
        }
        private bool IsBoardFull(int[,] board)
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (board[r, c] == 0) return false;
                }
            }
            return true;
        }

        // Prüft, ob ein Spieler gewonnen hat
        private int CheckWinner(int[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                // Überprüfe Zeilen
                if (board[i, 0] != 0 && board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2])
                    return board[i, 0];

                // Überprüfe Spalten
                if (board[0, i] != 0 && board[0, i] == board[1, i] && board[0, i] == board[2, i])
                    return board[0, i];
            }

            // Überprüfe Diagonalen
            if (board[0, 0] != 0 && board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2])
                return board[0, 0];

            if (board[0, 2] != 0 && board[0, 2] == board[1, 1] && board[0, 2] == board[2, 0])
                return board[0, 2];

            return 0; // Kein Gewinner
        }
    }

    public class B_HumanPlayer_TTT : B_BasePlayer, IB_HumanPlayer_TTT
    {
        public override string Name
        {
            get
            {
                return "Gruppe B TicTacToe HumanPlayer";
            }
        }

        public override IGamePlayer Clone()
        {
            B_HumanPlayer_TTT TTT_Human = new B_HumanPlayer_TTT();
            TTT_Human.SetPlayerNumber(this.PlayerNumber);
            return TTT_Human;
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (selection is IClickSelection && field is IB_Field_TTT)
            {
                return GetTTTMove((IB_Field_TTT)field, (IClickSelection)selection);
            }
            else
            {
                return null;
            }
        }

        public IB_Move_TTT GetTTTMove(IB_Field_TTT field, IClickSelection selection)

        {
            double _maxPossibleSquare = Math.Min(field.Height, field.Width);
            double _Margin = _maxPossibleSquare * field.MarginPercentage;
            double _maxSquare = _maxPossibleSquare - 2 * _Margin;
            double _sq = _maxSquare / 3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (selection.XClickPos > _Margin + (j * _sq) && selection.XClickPos < _sq + _Margin + (j * _sq) &&
                        selection.YClickPos > _Margin + (i * _sq) && selection.YClickPos < _sq + _Margin + (i * _sq) &&
                        field[i, j] <= 0)
                    {
                        return new B_Move_TTT(i, j, this.PlayerNumber);
                    }
                }
            }
            return null;
        }

    }

}
