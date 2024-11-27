using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class omSnakePaint : IX_PaintTicTacToe
    {
        public string Name { get { return "omSnakePaint"; } }

        public void PaintTicTacToeField(Canvas canvas, IX_TicTacToeField currentField)
        {
            PaintGameField(canvas, currentField);
        }

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (!(currentField is omSnakeField))
            {
                return;
            }

            omSnakeField myField = (omSnakeField)currentField;

            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(0, 0, 0);
            canvas.Background = new SolidColorBrush(bgColor);
            Color snakeColor = Color.FromRgb(0, 255, 0);
            Brush snakeBrush = new SolidColorBrush(snakeColor);
            Color foodColor = Color.FromRgb(255, 0, 0);
            Brush foodBrush = new SolidColorBrush(foodColor);

            // Draw snake
            foreach (var segment in myField.SnakeSegments)
            {
                Rectangle rect = new Rectangle()
                {
                    Width = 20,
                    Height = 20,
                    Fill = snakeBrush
                };
                Canvas.SetLeft(rect, segment.X * 20);
                Canvas.SetTop(rect, segment.Y * 20);
                canvas.Children.Add(rect);
            }

            // Draw food
            Rectangle food = new Rectangle()
            {
                Width = 20,
                Height = 20,
                Fill = foodBrush
            };
            Canvas.SetLeft(food, myField.Food.X * 20);
            Canvas.SetTop(food, myField.Food.Y * 20);
            canvas.Children.Add(food);
        }
    }

    public class omSnakeRules : OMM_BugRules
    {
        omSnakeField _Field = new omSnakeField();

        public OMM_BugField TicTacToeField { get { return _Field; } }

        public bool MovesPossible
        {
            get
            {
                return !_Field.GameOver;
            }
        }

        public string Name { get { return "omSnakeRules"; } }

        public int CheckIfPLayerWon()
        {
            return -1; // No winner in Snake
        }

        public void ClearField()
        {
            _Field = new omSnakeField();
        }

        public void DoTicTacToeMove(IX_TicTacToeMove move)
        {
            if (move is omSnakeMove snakeMove)
            {
                _Field.ChangeDirection(snakeMove.Direction);
            }
        }

        public IGameField CurrentField { get { return TicTacToeField; } }

        public OMM_BugField BugField => throw new NotImplementedException();

        public void DoMove(IPlayMove move)
        {
            if (move is IX_TicTacToeMove)
            {
                DoTicTacToeMove((IX_TicTacToeMove)move);
            }
        }

        public void DoBugMove(OMM_BugMove move)
        {
            //throw new NotImplementedException();
        }

        public void StartedGameCall()
        {
            //throw new NotImplementedException();
        }

        public void TickGameCall()
        {
            //throw new NotImplementedException();
        }
    }

    public class omSnakeField : OMM_BugField
    {
        public List<Point> SnakeSegments { get; private set; }
        public Point Food { get; private set; }
        public bool GameOver { get; private set; }
        public Direction CurrentDirection { get; private set; }
        private int _moveCounter;

        public omSnakeField()
        {
            Random rand = new Random();
            int startX = rand.Next(0, 20);
            int startY = rand.Next(0, 20);
            SnakeSegments = new List<Point> { new Point(startX, startY) };
            Food = new Point(rand.Next(0, 20), rand.Next(0, 20));
            GameOver = false;
            CurrentDirection = Direction.Right; // Initiale Bewegungsrichtung
            _moveCounter = 0;
        }

        public void MoveSnake()
        {
            if (GameOver) return;

            _moveCounter++;
            if (_moveCounter < 20) return;
            _moveCounter = 0;

            Point newHead = SnakeSegments.First();

            switch (CurrentDirection)
            {
                case Direction.Up:
                    newHead.Y -= 1;
                    break;
                case Direction.Down:
                    newHead.Y += 1;
                    break;
                case Direction.Left:
                    newHead.X -= 1;
                    break;
                case Direction.Right:
                    newHead.X += 1;
                    break;
            }

            SnakeSegments.Insert(0, newHead);

            if (newHead == Food)
            {
                // Generate new food
                Random rand = new Random();
                Food = new Point(rand.Next(0, 20), rand.Next(0, 20));
            }
            else
            {
                SnakeSegments.RemoveAt(SnakeSegments.Count - 1);
            }

            // Check for collisions
            if (newHead.X < 0 || newHead.X >= 20 || newHead.Y < 0 || newHead.Y >= 20 || SnakeSegments.Skip(1).Contains(newHead))
            {
                GameOver = true;
            }
        }

        public void ChangeDirection(Direction newDirection)
        {
            if ((CurrentDirection == Direction.Left && newDirection == Direction.Right) ||
                (CurrentDirection == Direction.Right && newDirection == Direction.Left) ||
                (CurrentDirection == Direction.Up && newDirection == Direction.Down) ||
                (CurrentDirection == Direction.Down && newDirection == Direction.Up))
            {
                // Ignore opposite direction changes
                return;
            }

            CurrentDirection = newDirection;
        }

        public int this[int r, int c]
        {
            get { return -1; }
            set { }
        }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is OMM_BugGamePaint;
        }
    }

    public class omSnakeHumanPlayer : IX_HumanTicTacToePlayer
    {
        int _PlayerNumber = 0;

        public string Name { get { return "omSnakeHumanPlayer"; } }

        public int PlayerNumber { get { return _PlayerNumber; } }

        public IGamePlayer Clone()
        {
            omSnakeHumanPlayer player = new omSnakeHumanPlayer();
            player.SetPlayerNumber(_PlayerNumber);
            return player;
        }

        public IX_TicTacToeMove GetMove(IMoveSelection selection, IX_TicTacToeField field)
        {
            if (selection is IKeySelection keySelection)
            {
                Direction direction = Direction.None;

                if (keySelection.Key == System.Windows.Input.Key.Left)
                {
                    direction = Direction.Left;
                }
                else if (keySelection.Key == System.Windows.Input.Key.Right)
                {
                    direction = Direction.Right;
                }

                if (direction != Direction.None)
                {
                    return new omSnakeMove(direction, _PlayerNumber);
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
            return rules is IX_TicTacToeRules;
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (field is IX_TicTacToeField)
            {
                return GetMove(selection, (IX_TicTacToeField)field);
            }
            else
            {
                return null;
            }
        }
    }

    public class omSnakeComputerPlayer : IX_ComputerTicTacToePlayer
    {
        int _PlayerNumber = 0;

        public string Name { get { return "omSnakeComputerPlayer"; } }

        public int PlayerNumber { get { return _PlayerNumber; } }

        public IGamePlayer Clone()
        {
            omSnakeComputerPlayer player = new omSnakeComputerPlayer();
            player.SetPlayerNumber(_PlayerNumber);
            return player;
        }

        public IX_TicTacToeMove GetMove(IX_TicTacToeField field)
        {
            // Implement simple AI for snake movement
            Random rand = new Random();
            Direction direction = (Direction)rand.Next(1, 5);
            return new omSnakeMove(direction, _PlayerNumber);
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is IX_TicTacToeRules;
        }

        public IPlayMove GetMove(IGameField field)
        {
            if (field is IX_TicTacToeField)
            {
                return GetMove((IX_TicTacToeField)field);
            }
            else
            {
                return null;
            }
        }
    }

    public class omSnakeMove : IX_TicTacToeMove
    {
        public Direction Direction { get; private set; }
        public int PlayerNumber { get; private set; }

        public omSnakeMove(Direction direction, int playerNumber)
        {
            Direction = direction;
            PlayerNumber = playerNumber;
        }

        public int Row { get { return -1; } }
        public int Column { get { return -1; } }
    }
    public class AnimatedSnakePaint : IPaintGame2
    {


        public string Name { get { return "AnimatedSnakePaint"; } }


        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            // Implementierung der Methode zum Zeichnen des Spielfelds
            if (!(currentField is omSnakeField))
            {
                return;
            }

            omSnakeField myField = (omSnakeField)currentField;

            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(0, 0, 0);
            canvas.Background = new SolidColorBrush(bgColor);
            Color snakeColor = Color.FromRgb(0, 255, 0);
            Brush snakeBrush = new SolidColorBrush(snakeColor);
            Color foodColor = Color.FromRgb(255, 0, 0);
            Brush foodBrush = new SolidColorBrush(foodColor);

            // Draw snake
            foreach (var segment in myField.SnakeSegments)
            {
                Rectangle rect = new Rectangle()
                {
                    Width = 20,
                    Height = 20,
                    Fill = snakeBrush
                };
                Canvas.SetLeft(rect, segment.X * 20);
                Canvas.SetTop(rect, segment.Y * 20);
                canvas.Children.Add(rect);
            }

            // Draw food
            Rectangle food = new Rectangle()
            {
                Width = 20,
                Height = 20,
                Fill = foodBrush
            };
            Canvas.SetLeft(food, myField.Food.X * 20);
            Canvas.SetTop(food, myField.Food.Y * 20);
            canvas.Children.Add(food);
        }

        public void TickPaintGameField(Canvas canvas, IGameField currentField)
        {
            // Implementierung der Methode zum Zeichnen des Spielfelds alle 40ms
            PaintGameField(canvas, currentField);
        }
    }

    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}
