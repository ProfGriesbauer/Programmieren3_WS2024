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
            double _Height = canvas.ActualHeight;
            double _Width = canvas.ActualWidth;
            double _maxSquare = Math.Max(_Height, _Width);

            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color lineColor = Color.FromRgb(255, 0, 0);
            Brush lineStroke = new SolidColorBrush(lineColor);
            Color XColor = Color.FromRgb(0, 255, 0);
            Brush XStroke = new SolidColorBrush(XColor);
            Color OColor = Color.FromRgb(0, 0, 255);
            Brush OStroke = new SolidColorBrush(OColor);

            Line l1 = new Line() { X1 = 120, Y1 = 20, X2 = 120, Y2 = _maxSquare-20, Stroke = lineStroke, StrokeThickness = 3.0 };
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
            _Field = new B_Field_TTT();
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
    public class B_ComputerPlayerSchlau_TTT : B_BasePlayer, IB_ComputerPlayerSchlau_TTT
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

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (selection.XClickPos > 20 + (j * 100) && selection.XClickPos < 120 + (j * 100) &&
                        selection.YClickPos > 20 + (i * 100) && selection.YClickPos < 120 + (i * 100) &&
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
