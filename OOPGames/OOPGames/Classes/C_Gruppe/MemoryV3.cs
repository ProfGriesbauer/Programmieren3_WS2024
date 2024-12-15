using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Timers;
// Memory 11:40
namespace OOPGames
{
    // Memory Game Field 
    public class MemoryGameField : IGameField
    {
        public string[,] Cards { get; set; }
        public bool[,] Revealed { get; set; }
        public int Rows { get; }
        public int Columns { get; }

        public MemoryGameField(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            InitializeField();
        }

        private void InitializeField()
        {
            Cards = new string[Rows, Columns];
            Revealed = new bool[Rows, Columns];
            var cardValues = new List<string>();
            int totalCards = Rows * Columns;

            for (int i = 0; i < totalCards / 2; i++)
            {
                cardValues.Add($"{i + 1}");
                cardValues.Add($"{i + 1}");
            }

            var random = new Random();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    int randomIndex = random.Next(cardValues.Count);
                    Cards[i, j] = cardValues[randomIndex];
                    cardValues.RemoveAt(randomIndex);
                }
            }
        }

        public bool IsRevealed(int row, int column)
        {
            Console.WriteLine($"Prüfe IsRevealed für Row={row}, Column={column}");
            ValidateCoordinates(row, column);
            return Revealed[row, column];
        }

        public void RevealCard(int row, int column)
        {
            ValidateCoordinates(row, column);
            Revealed[row, column] = true;
        }

        public string GetCard(int row, int column)
        {
            ValidateCoordinates(row, column);
            return Cards[row, column];
        }

        public void HideCards((int Row, int Col) first, (int Row, int Col) second)
        {
            Console.WriteLine($"Verdeckkarten: First=({first.Row}, {first.Col}), Second=({second.Row}, {second.Col})");
            ValidateCoordinates(first.Row, first.Col);
            ValidateCoordinates(second.Row, second.Col);
            Revealed[first.Row, first.Col] = false;
            Revealed[second.Row, second.Col] = false;
        }

        private void ValidateCoordinates(int row, int column)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException($"Row: {row}, Column: {column} ist außerhalb des Bereichs. Gültig sind Row: 0-{Rows - 1}, Column: 0-{Columns - 1}.");
            }
        }

        public bool CanBePaintedBy(IPaintGame painter) => true;
    }

    public class C_ClickSelection : IClickSelection
    {
        public int XClickPos { get; set; }
        public int YClickPos { get; set; }
        public int ChangedButton { get; set; }

        public MoveType MoveType => throw new NotImplementedException();
    }


    // Memory Move
    public class MemoryMove : IRowMove, IColumnMove
    {
        public int PlayerNumber { get; }
        public int Row { get; }
        public int Column { get; }

        public MemoryMove(int playerNumber, int row, int column)
        {
            PlayerNumber = playerNumber;
            Row = row;
            Column = column;
        }
    }

    // Memory Game Rules
    public class C_MemoryGameRules : IGameRules
    {
        private Timer _revealTimer;
        public string Name => "Memory";
        public IGameField CurrentField { get; set; }
        public bool MovesPossible => RemainingPairs > 0;

        private int RemainingPairs;
        private int Player1Score;
        private int Player2Score;
        private (int Row, int Column)? FirstCard;
        private (int Row, int Column)? _secondCard;
        private bool _isWaiting;

        public C_MemoryGameRules(int rows = 4, int columns = 4)
        {
            if ((rows * columns) % 2 != 0)
            {
                throw new ArgumentException("Memory game must have an even number of cards!");
            }

            CurrentField = new MemoryGameField(rows, columns);
            RemainingPairs = (rows * columns) / 2;
            _revealTimer = new Timer(1000); // 1 Sekunde Verzögerung
            _revealTimer.Elapsed += OnRevealTimerElapsed;
        }

        public void DoMove(IPlayMove move)
        {
            if (_isWaiting || !(move is MemoryMove memoryMove) || !(CurrentField is MemoryGameField field)) return;

            if (field.IsRevealed(memoryMove.Row, memoryMove.Column)) return;

            field.RevealCard(memoryMove.Row, memoryMove.Column);

            if (FirstCard == null)
            {
                FirstCard = (memoryMove.Row, memoryMove.Column);
                Console.WriteLine($"Erste Karte: Row={FirstCard.Value.Row}, Col={FirstCard.Value.Column}");
            }
            else
            {
                _secondCard = (memoryMove.Row, memoryMove.Column);
                Console.WriteLine($"Zweite Karte: Row={_secondCard.Value.Row}, Col={_secondCard.Value.Column}");
                _isWaiting = true;
                if (field.GetCard(FirstCard.Value.Row, FirstCard.Value.Column) ==
                field.GetCard(_secondCard.Value.Row, _secondCard.Value.Column))
                {
                    RemainingPairs--;
                    FirstCard = null;
                    _secondCard = null;
                    _isWaiting = false;
                }
                else
                {
                    _revealTimer.Start();
                }
            }
        }

        private void OnRevealTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _revealTimer.Stop();
            if (FirstCard.HasValue && _secondCard.HasValue && CurrentField is MemoryGameField field)
            {
                Console.WriteLine($"Verdeckkarten: {FirstCard.Value} und {_secondCard.Value}");
                field.HideCards(FirstCard.Value, _secondCard.Value);
            }
            else
            {
                Console.WriteLine("Fehler: Kartenkoordinaten sind ungültig.");
            }
            FirstCard = null;
            _secondCard = null;
            _isWaiting = false;
        }

        private void CheckPair(MemoryGameField field, (int Row, int Column) first, (int Row, int Column) second, int playerNumber)
        {
            if (field.GetCard(first.Row, first.Column) == field.GetCard(second.Row, second.Column))
            {
                RemainingPairs--;
                if (playerNumber == 1) Player1Score++;
                else Player2Score++;
            }
            else
            {
                field.HideCards(first, second);
            }
        }

        public void ClearField()
        {
            CurrentField = new MemoryGameField(4, 4);
            RemainingPairs = (4 * 4) / 2;
            FirstCard = null;
            _secondCard = null;
        }

        public int CheckIfPLayerWon()
        {
            return RemainingPairs == 0 ? (Player1Score > Player2Score ? 1 : 2) : -1;
        }
    }

    // Memory Game Painter
    public class C_MemoryGamePainter : IPaintGame
    {
        public string Name => "C_MemoryGamePainter";
        C_MemoryGameRules _rules = new C_MemoryGameRules();


        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (!(currentField is MemoryGameField memoryField)) return;
            canvas.Children.Clear();

            double cardWidth = canvas.ActualWidth / memoryField.Columns;
            double cardHeight = canvas.ActualHeight / memoryField.Rows;

            for (int row = 0; row < memoryField.Rows; row++)
            {
                for (int col = 0; col < memoryField.Columns; col++)
                {
                    var card = new Rectangle
                    {
                        Width = cardWidth - 5,
                        Height = cardHeight - 5,
                        Stroke = Brushes.Black,
                        Fill = memoryField.IsRevealed(row, col) ? Brushes.LightGray : Brushes.Silver
                    };

                    card.MouseLeftButtonDown += (sender, args) =>
                    {
                        var selection = new C_ClickSelection
                        {
                            XClickPos = (int)args.GetPosition(canvas).X,
                            YClickPos = (int)args.GetPosition(canvas).Y,
                            ChangedButton = 1
                        };

                        HumanPlayerMove(selection, canvas);
                        PaintGameField(canvas, _rules.CurrentField);

                        try
                        {
                            Console.WriteLine($"Klick auf Karte: Row={row}, Col={col}");
                            HumanPlayerMove(selection, canvas);
                            PaintGameField(canvas, _rules.CurrentField);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Fehler: {ex.Message}");
                        }
                    };

                    Canvas.SetLeft(card, col * cardWidth);
                    Canvas.SetTop(card, row * cardHeight);
                    canvas.Children.Add(card);

                    if (memoryField.IsRevealed(row, col))
                    {
                        var text = new TextBlock
                        {
                            Text = memoryField.GetCard(row, col),
                            Foreground = Brushes.Black,
                            FontSize = cardHeight / 3,
                            FontWeight = FontWeights.Bold,
                            TextAlignment = TextAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };

                        Canvas.SetLeft(text, col * cardWidth + cardWidth / 4);
                        Canvas.SetTop(text, row * cardHeight + cardHeight / 4);
                        canvas.Children.Add(text);
                    }
                }
            }
        }

        private void HumanPlayerMove(IClickSelection selection, Canvas canvas)
        {
            if (!(_rules.CurrentField is MemoryGameField field)) return;

            double cardWidth = canvas.ActualWidth / field.Columns;
            double cardHeight = canvas.ActualHeight / field.Rows;

            // Berechnung der Zeile und Spalte basierend auf Klickposition
            int row = (int)(selection.YClickPos / cardHeight);
            int col = (int)(selection.XClickPos / cardWidth);

            try
            {
                Console.WriteLine($"Klick: X={selection.XClickPos}, Y={selection.YClickPos}, Row={row}, Col={col}");
                var move = new MemoryMove(1, row, col);
                _rules.DoMove(move);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler in HumanPlayerMove: {ex.Message}");
            }
        }
    }

    // Human Player
    public class C_HumanMemoryPlayer : IHumanGamePlayer
    {
        public string Name => "RJL_HumanMemoryPlayer";
        public int PlayerNumber { get; set; }

        public void SetPlayerNumber(int playerNumber) => PlayerNumber = playerNumber;

        public bool CanBeRuledBy(IGameRules rules) => rules is C_MemoryGameRules;

        public IGamePlayer Clone()
        {
            var player = new C_HumanMemoryPlayer();
            player.SetPlayerNumber(PlayerNumber);
            return player;
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            // This function is not used in UI-based implementation.
            return null;
        }
    }
    public class C_ComputerMemoryPlayer : IComputerGamePlayer
    {
        public string Name => "RJL_ComputerMemoryPlayer";
        public int PlayerNumber { get; set; }
        private readonly Random _random = new Random();

        public void SetPlayerNumber(int playerNumber) => PlayerNumber = playerNumber;

        public bool CanBeRuledBy(IGameRules rules) => rules is C_MemoryGameRules;

        public IGamePlayer Clone()
        {
            var player = new C_ComputerMemoryPlayer();
            player.SetPlayerNumber(PlayerNumber);
            return player;
        }

        public IPlayMove GetMove(IGameField field)
        {
            var memoryField = field as MemoryGameField;
            if (memoryField == null) return null;

            int row, col;
            do
            {
                row = _random.Next(memoryField.Rows);
                col = _random.Next(memoryField.Columns);
            } while (memoryField.IsRevealed(row, col));

            return new MemoryMove(PlayerNumber, row, col);
        }
    }
}
