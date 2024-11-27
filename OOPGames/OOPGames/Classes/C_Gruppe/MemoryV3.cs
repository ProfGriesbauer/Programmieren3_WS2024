
namespace OOPGames.Classes.C_Gruppe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace OOPGames.Classes.C_Gruppe
    {
        using System;
        using System.Collections.Generic;
        using System.Windows.Controls;
        using System.Windows.Media;
        using System.Windows.Shapes;

        namespace OOPGames
        {
            // Memory-Feld
            public class MemoryGameField : IGameField
            {

                public string[,] Cards { get; private set; }
                public bool[,] Revealed { get; private set; }
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

                    // Kartenwerte generieren (Paare)
                    for (int i = 0; i < totalCards / 2; i++)
                    {
                        cardValues.Add($"Card{i + 1}");
                        cardValues.Add($"Card{i + 1}");
                    }

                    // Karten mischen
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

                public bool CanBePaintedBy(IPaintGame painter) => true;
            }

            // Memory-Spielzug
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

            // Memory-Spielregeln
            public class C_MemoryGameRules : IGameRules
            {
                public string Name => "Memory";
                public IGameField CurrentField { get; private set; }
                public bool MovesPossible => RemainingPairs > 0;

                private int RemainingPairs;
                private int ScorePlayer1;
                private int ScorePlayer2;

                public C_MemoryGameRules(int rows = 4, int columns = 4)
                {
                    if ((rows * columns) % 2 != 0) throw new ArgumentException("Das Spielfeld muss eine gerade Anzahl an Feldern haben!");
                    CurrentField = new MemoryGameField(rows, columns);
                    RemainingPairs = (rows * columns) / 2;
                }

                public void DoMove(IPlayMove move)
                {
                    if (move is MemoryMove memoryMove)
                    {
                        var field = CurrentField as MemoryGameField;

                        if (field == null || memoryMove.Row >= field.Rows || memoryMove.Column >= field.Columns) return;
                        if (field.Revealed[memoryMove.Row, memoryMove.Column]) return;

                        // Karte aufdecken
                        field.Revealed[memoryMove.Row, memoryMove.Column] = true;

                        // Prüfung auf Paare und Spielstatus
                        CheckForPairs(memoryMove, field);
                    }
                }

                private void CheckForPairs(MemoryMove move, MemoryGameField field)
                {
                    var revealedCards = new List<(int Row, int Column)>();

                    // Alle aufgedeckten Karten finden
                    for (int i = 0; i < field.Rows; i++)
                    {
                        for (int j = 0; j < field.Columns; j++)
                        {
                            if (field.Revealed[i, j] && !revealedCards.Contains((i, j)))
                            {
                                revealedCards.Add((i, j));
                            }
                        }
                    }

                    if (revealedCards.Count == 2)
                    {
                        var firstCard = revealedCards[0];
                        var secondCard = revealedCards[1];

                        if (field.Cards[firstCard.Row, firstCard.Column] == field.Cards[secondCard.Row, secondCard.Column])
                        {
                            RemainingPairs--;
                            if (move.PlayerNumber == 1) ScorePlayer1++;
                            else ScorePlayer2++;
                        }
                        else
                        {
                            field.Revealed[firstCard.Row, firstCard.Column] = false;
                            field.Revealed[secondCard.Row, secondCard.Column] = false;
                        }
                    }
                }

                public void ClearField() => CurrentField = new MemoryGameField(4, 4);

                public int CheckIfPLayerWon()
                {
                    return RemainingPairs == 0 ? 1 : -1;
                }
            }

            // Memory-Spiel-Painter: Implementierung von IPaintGame
            public class C_MemoryGamePainter : IPaintGame
            {
                public string Name { get { return "RJL_MemoryGamePainter"; {} } }

                public void PaintGameField(Canvas canvas, IGameField currentField)
                {
                    if(currentField is MemoryGameField)
                    {
                        MemoryGameField memoryGameField = (MemoryGameField)currentField;

                        var field = currentField as MemoryGameField;
                        if (field == null) return;

                        canvas.Children.Clear();
                        double cardWidth = canvas.Width / field.Columns;
                        double cardHeight = canvas.Height / field.Rows;
                        Line l1 = new Line() { X1 = 120, Y1 = 20, X2 = 120, Y2 = 320 };
                        canvas.Children.Add(l1);
                     for (int row = 0; row < field.Rows; row++)
                        {
                            for (int col = 0; col < field.Columns; col++)
                            {
                                var card = new Rectangle
                             {
                                    Width = cardWidth,
                                     Height = cardHeight,
                                 Stroke = Brushes.Black,
                                    Fill = field.Revealed[row, col] ? Brushes.LightGray : Brushes.Silver
                                };

                                Canvas.SetLeft(card, col * cardWidth);
                             Canvas.SetTop(card, row * cardHeight);
                             canvas.Children.Add(card);

                                // Karte anzeigen, wenn sie aufgedeckt ist
                                if (field.Revealed[row, col])
                                {
                                    var text = new TextBlock
                                    {
                                        Text = field.Cards[row, col],
                                        Foreground = Brushes.Black,
                                        VerticalAlignment = System.Windows.VerticalAlignment.Center,
                                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center
                                    };
                                    Canvas.SetLeft(text, col * cardWidth + cardWidth / 4);
                                    Canvas.SetTop(text, row * cardHeight + cardHeight / 4);
                                    canvas.Children.Add(text);
                                }
                            }
                        }
                    }
                }
            }

            // Menschlicher Spieler
            public class C_HumanMemoryPlayer : IHumanGamePlayer
            {
                public string Name { get { return "RJL_MemoryHumanPlayer"; { } } }



                public int PlayerNumber { get; private set; }


                public void SetPlayerNumber(int playerNumber)
                {
                    PlayerNumber = playerNumber;
                }

                public bool CanBeRuledBy(IGameRules rules)
                {
                    return rules is C_MemoryGameRules;
                }

                public IGamePlayer Clone()
                {
                    C_HumanMemoryPlayer C_M_HP = new C_HumanMemoryPlayer();
                    C_M_HP.SetPlayerNumber(this.PlayerNumber);
                    return C_M_HP;
                }

                public IPlayMove GetMove(IMoveSelection selection, IGameField field)
                {
                    var memoryField = field as MemoryGameField;
                    if (memoryField == null)
                        return null;

                    Console.WriteLine($"{Name}, bitte geben Sie die Zeile und Spalte ein (z. B. 0 1 für Zeile 0, Spalte 1):");
                    try
                    {
                        var input = Console.ReadLine()?.Split();
                        if (input?.Length == 2)
                        {
                            int row = int.Parse(input[0]);
                            int col = int.Parse(input[1]);

                            if (row >= 0 && row < memoryField.Rows && col >= 0 && col < memoryField.Columns && !memoryField.Revealed[row, col])
                            {
                                return new MemoryMove(PlayerNumber, row, col);
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Ungültige Eingabe! Bitte erneut versuchen.");
                    }

                    return null;
                }
            }

            // Computer-Spieler
            public class C_ComputerMemoryPlayer : IComputerGamePlayer
            {
                public string Name { get { return "RJL_ComputerMemoryPlayer"; { } } }
                public int PlayerNumber { get; private set; }
                private Random _random;

                public void SetPlayerNumber(int playerNumber)
                {
                    PlayerNumber = playerNumber;
                }

                public bool CanBeRuledBy(IGameRules rules)
                {
                    return rules is C_MemoryGameRules;
                }

                public IGamePlayer Clone()
                {
                    C_ComputerMemoryPlayer C_M_CP = new C_ComputerMemoryPlayer();
                    C_M_CP.SetPlayerNumber(this.PlayerNumber);
                    return C_M_CP;
                }

                public IPlayMove GetMove(IGameField field)
                {
                    var memoryField = field as MemoryGameField;
                    if (memoryField == null)
                        return null;

                    int row, col;
                    do
                    {
                        row = _random.Next(memoryField.Rows);
                        col = _random.Next(memoryField.Columns);
                    } while (memoryField.Revealed[row, col]);

                    return new MemoryMove(PlayerNumber, row, col);
                }
            }

            

        }

    }





}
