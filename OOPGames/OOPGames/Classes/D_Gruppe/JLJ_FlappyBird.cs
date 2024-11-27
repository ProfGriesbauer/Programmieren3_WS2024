using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class FlappyPainter : ID_FB_Painter
    {
        public string Name => "FlappyBirdPainter";

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {

            var field = (FlappyField)currentField;

            //Spielfeld der Canvas anpassen
            field.AdoptCanvas(canvas);

            canvas.Children.Clear();

            // Vogel zeichnen
            var bird = new Ellipse
            {
                Width = field.Bird.Radius * 2,
                Height = field.Bird.Radius * 2,
                Fill = Brushes.Yellow
            };
            Canvas.SetTop(bird, field.Bird.Y - field.Bird.Radius);
            Canvas.SetLeft(bird, field.Bird.X - field.Bird.Radius);
            canvas.Children.Add(bird);

            // Hindernisse zeichnen
            foreach (var tube in field.Obstacles)
            {
                // Oberer Pfeiler
                var topPillar = new Rectangle
                {
                    Width = tube.Width,
                    Height = tube.TopHeight,
                    Fill = Brushes.Green
                };
                Canvas.SetTop(topPillar, 0);
                Canvas.SetLeft(topPillar, tube.X);
                canvas.Children.Add(topPillar);

                // Unterer Pfeiler
                var bottomPillar = new Rectangle
                {
                    Width = tube.Width,
                    Height = field.Height - (tube.TopHeight + tube.GapSize),
                    Fill = Brushes.Green
                };
                Canvas.SetTop(bottomPillar, tube.TopHeight + tube.GapSize);
                Canvas.SetLeft(bottomPillar, tube.X);
                canvas.Children.Add(bottomPillar);
            }

            //Score zeichnen 
            var scoreText = new TextBlock
            {
                Text = $"Score: {field.score}",
                FontSize = 30,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black,
                Background = Brushes.Transparent,
                TextAlignment = TextAlignment.Center,
                Width = 200, // Breite des Textblocks
                Height = 40 // Höhe des Textblocks
            };

            // Positioniere das TextBlock oben mittig auf der Canvas
            Canvas.SetTop(scoreText, 10); // 10 Pixel vom oberen Rand
            Canvas.SetLeft(scoreText, (canvas.ActualWidth - scoreText.Width) / 2); // Zentriert
            canvas.Children.Add(scoreText);

            //Score zeichnen 
            var gameoverText = new TextBlock
            {
                Text = $"Game Over!",
                FontSize = 60,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black,
                Background = Brushes.Red,
                TextAlignment = TextAlignment.Center,
                Width = 400, // Breite des Textblocks
                Height = 100 // Höhe des Textblocks
            };

            //Game Over Pop up
            if (field.gameover)
            {
                Canvas.SetTop(gameoverText, (canvas.ActualHeight - gameoverText.Height) / 2); // 10 Pixel vom oberen Rand
                Canvas.SetLeft(gameoverText, (canvas.ActualWidth - gameoverText.Width) / 2); // Zentriert
                canvas.Children.Add(gameoverText);
            }
        }

        public void TickPaintGameField(Canvas canvas, IGameField currentField)
        {
            PaintGameField(canvas, currentField);
        }
    }

    public class FlappyField : ID_FB_GameField
    {
        public D_Bird Bird { get; private set; } // Der Vogel als Objekt
        public List<D_Tubes> Obstacles { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int score = 0;

        public bool gameover = false;

        public void AdoptCanvas (Canvas canvas)
        {
            Width = (int)canvas.ActualWidth;
            Height = (int)canvas.ActualHeight;
        }

        public FlappyField(int width, int height)
        {
            Width = width;
            Height = height;
            Bird = new D_Bird(50, height / 2, 15, 1, 0); // Initialisierung des Vogels
            Obstacles = new List<D_Tubes>();
        }

        public bool CanBePaintedBy(IPaintGame painter) => painter is ID_FB_Painter;
    }

    public class FlappyRules : ID_FB_Rules
    {
        public string Name => "FlappyBirdRules";
        public IGameField CurrentField { get; private set; }
        public bool MovesPossible // Solange der Spieler nicht verloren hat.
        {
            get
            {
                if (CheckifCollision((FlappyField)CurrentField))
                {
                    return false;
                }

                return true;
            }
        }
        private int PlayerWon = -1;

        public FlappyRules()
        {
            CurrentField = new FlappyField(800, 600);
        }

        public void DoMove(IPlayMove move)
        {
            if (move is FlappyMove)
            {
                var bird = ((FlappyField)CurrentField).Bird;
                bird.moveUp(-13); // Vogel springt nach oben
            }
        }

        public void ClearField()
        {
            CurrentField = new FlappyField(800, 600);
            PlayerWon = -1;
        }

        public int CheckIfPLayerWon() => PlayerWon;

        public void StartedGameCall()
        {
            // Initialisierung vom Score
            var field = (FlappyField)CurrentField;
            field.score = 0;
            field.gameover = false;
        }

        public bool CheckifCollision(FlappyField field)
        {
            if ((field.Bird.Y - field.Bird.Radius) < 0 || (field.Bird.Y + field.Bird.Radius) > field.Height)
            {
                return true;
            }
            foreach (var tube in field.Obstacles)
            {
                if (tube.CheckCollision(field.Bird))
                {
                    return true;
                }
            }
            return false;
        }

        public void TickGameCall()
        {
            var field = (FlappyField)CurrentField;

            if (!CheckifCollision(field))
            {
                // Aktualisiere die Position des Vogels
                field.Bird.UpdatePosition();

                // Bewege die Hindernisse
                foreach (var tube in field.Obstacles)
                {
                    tube.MoveLeft(5); // Bewege jedes Hindernis nach links
                }
                // Entferne Hindernisse, die aus dem Bildschirmbereich sind
                field.Obstacles.RemoveAll(tube => tube.IsOutOfScreen());

                // Füge neue Hindernisse hinzu
                if (field.Obstacles.Count == 0 || field.Obstacles.Last().X < field.Width - 250)
                {
                    Random rnd = new Random();
                    int gapY = rnd.Next(100, field.Height - 200);
                    field.Obstacles.Add(new D_Tubes(field.Width - 30, gapY, 150, 40, field.Height));
                }

                //Score updaten
                updateScore(field);
            }
            else
            {
                // Ende des Spiels Highscore speichern 
                field.gameover = true;
                // Keine Moves mehr 
                // Neustart Knopf
            }
        }

        public void updateScore(FlappyField field)
        {
            foreach (var tube in field.Obstacles)
            {
                if (((tube.X + tube.Width) < field.Bird.X) && (tube.X + tube.Width) > ((field.Bird.X) - 5))
                {
                    field.score++;
                }
            }
        }

        public string StatusBar()
        {
            if (!CheckifCollision((FlappyField)CurrentField))
            {
                return "Jump with Key U!";
            }
            else
            {
                return "Game Over!";
            }
        }
    }

    public class FlappyMove : ID_FB_Move
    {
        public int PlayerNumber { get; private set; }
        public FlappyMove(int playerNumber)
        {
            PlayerNumber = playerNumber;
        }
    }

    public class FlappyPlayer : ID_FB_HumanPlayer
    {
        public string Name => "FlappyBirdPlayer";
        int _playerNumber = 0;

        public int PlayerNumber => _playerNumber;

        public void SetPlayerNumber(int playerNumber) => _playerNumber = playerNumber;

        public bool CanBeRuledBy(IGameRules rules) => rules is FlappyRules;

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (selection is IKeySelection keySelection)
            {
                if (keySelection.Key == Key.U)
                {
                    return new FlappyMove(_playerNumber);
                }
            }  
            return null;
        }

        IGamePlayer IGamePlayer.Clone()
        {
            FlappyPlayer fbhumanplayer = new FlappyPlayer();
            fbhumanplayer.SetPlayerNumber(_playerNumber);
            return fbhumanplayer;
        }
    }


    public class D_Tubes
    {
        public int X { get; private set; } // Die x-Position der Hindernisse
        public int TopHeight { get; private set; } // Höhe des oberen Pfeilers
        public int GapSize { get; private set; } // Größe der Lücke zwischen den Pfeilern
        public int Width { get; private set; } // Breite der Pfeiler

        private int ScreenHeight; // Die Höhe des Spielfeldes

        // Konstruktor
        public D_Tubes(int x, int topHeight, int gapSize, int width, int screenHeight)
        {
            X = x;
            TopHeight = topHeight;
            GapSize = gapSize;
            Width = width;
            ScreenHeight = screenHeight;
        }

        // Bewegt die Pfeiler nach links
        public void MoveLeft(int speed)
        {
            X -= speed;
        }

        // Prüft, ob der Vogel mit den Pfeilern kollidiert
        public bool CheckCollision(D_Bird bird)
        {
            // Überprüfung für den oberen Pfeiler
            bool collidesWithTop = bird.X + bird.Radius > X &&
                                   bird.X - bird.Radius < X + Width &&
                                   bird.Y - bird.Radius < TopHeight;

            // Überprüfung für den unteren Pfeiler
            bool collidesWithBottom = bird.X + bird.Radius > X &&
                                      bird.X - bird.Radius < X + Width &&
                                      bird.Y + bird.Radius > TopHeight + GapSize;

            return collidesWithTop || collidesWithBottom;
        }

        // Prüft, ob die Pfeiler aus dem Bildschirmbereich heraus sind
        public bool IsOutOfScreen()
        {
            return X + Width < 0; // Hindernis ist aus dem Bildschirmbereich
        }
    }


    public class D_Bird
    {
        public int X { get; private set; } // Die x-Position des Vogels
        public int Y { get; private set; } // Die y-Position des Vogels
        public int Radius{ get; private set; } // Der Radius des Vogels für die Hitbox
        public int Acceleration { get; private set; } // Die Beschleunigunf des Vogels nach unten 
        public int Velocity { get; private set; } // Die Geschwindigkeit mit der sich der Vogel in Y richtung bewegt


        // Konstruktor
        public D_Bird(int x, int y, int radius, int acceleration, int velocity)
        {
            X = x;
            Y = y;
            Radius = radius;
            Acceleration = acceleration;
            Velocity = velocity;
        }

        // Aktion wenn Taste gedrückt wird
        public void moveUp(int speed)
        {
            Velocity = speed;
        }

        // Nach jedem Tick wird die Posotion des Vogels erneuert und die Beschleunigung auf die Geschwindigkeit addiert
        public void UpdatePosition()
        {
            Velocity += Acceleration;
            Y += Velocity;
        }
    }
}
