using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace MemoryGame
{
    class Program
    {
        static char[,] board = new char[4, 4];
        static bool[,] revealed = new bool[4, 4];
        static Random random = new Random();

        static void Main(string[] args)
        {
            InitializeBoard();
            PlayGame();
        }

        static void InitializeBoard()
        {
            // Erstellen und Mischen der Kartenpaare
            char[] cards = { 'A', 'A', 'B', 'B', 'C', 'C', 'D', 'D', 'E', 'E', 'F', 'F', 'G', 'G', 'H', 'H' };
            Shuffle(cards);

            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    board[i, j] = cards[index++];
                    revealed[i, j] = false;
                }
            }
        }

        static void Shuffle(char[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        static void PlayGame()
        {
            int moves = 0;
            int pairsFound = 0;

            while (pairsFound < 8)
            {
                Console.Clear();
                PrintBoard();
                Console.WriteLine("Gib die Koordinaten der ersten Karte ein (z.B. 0 1):");
                var first = GetCoordinates();
                Console.WriteLine("Gib die Koordinaten der zweiten Karte ein (z.B. 2 3):");
                var second = GetCoordinates();

                if (board[first.Item1, first.Item2] == board[second.Item1, second.Item2] && (first != second))
                {
                    Console.WriteLine("Paar gefunden!");
                    revealed[first.Item1, first.Item2] = true;
                    revealed[second.Item1, second.Item2] = true;
                    pairsFound++;
                }
                else
                {
                    Console.WriteLine("Kein Paar. Versuche es noch einmal.");
                    Console.WriteLine($"Erste Karte: {board[first.Item1, first.Item2]}, Zweite Karte: {board[second.Item1, second.Item2]}");
                    System.Threading.Thread.Sleep(2000);
                }

                moves++;
            }

            Console.Clear();
            PrintBoard();
            Console.WriteLine($"Glückwunsch! Du hast alle Paare gefunden. Benötigte Züge: {moves}");
        }

        static (int, int) GetCoordinates()
        {
            int row, col;
            while (true)
            {
                var input = Console.ReadLine().Split();
                if (input.Length == 2 &&
                    int.TryParse(input[0], out row) && row >= 0 && row < 4 &&
                    int.TryParse(input[1], out col) && col >= 0 && col < 4)
                {
                    if (!revealed[row, col])
                        return (row, col);
                    else
                        Console.WriteLine("Diese Karte wurde bereits aufgedeckt. Bitte wähle eine andere Karte.");
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte erneut eingeben (z.B. 0 1):");
                }
            }
        }

        static void PrintBoard()
        {
            Console.WriteLine("   0 1 2 3");
            Console.WriteLine("  ---------");
            for (int i = 0; i < 4; i++)
            {
                Console.Write(i + " | ");
                for (int j = 0; j < 4; j++)
                {
                    if (revealed[i, j])
                        Console.Write(board[i, j] + " ");
                    else
                        Console.Write("* ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("  ---------");
        }
    }
}
