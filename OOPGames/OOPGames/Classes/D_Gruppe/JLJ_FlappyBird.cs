using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Windows.Media;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Windows.Media.Effects;
using static System.Net.WebRequestMethods;
using Microsoft.Win32;

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

            canvas.Children.Clear(); // Canvas leeren 

            // Aufrufe der individuellen Zeichnungs-Methoden
            DrawBackground(canvas, field);
            DrawBird(canvas, field);
            DrawObstacles(canvas, field);
            DrawGround(canvas, field);
            DrawScore(canvas, field);

            if (field.gameover)
            {
                DrawGameOver(canvas, field);
                DrawHighScore(canvas, field);
            }
        }

        private void DrawBackground(Canvas canvas, FlappyField field)
        {
            var backgroundImage = new Image
            {
                Width = canvas.ActualWidth,
                Height = canvas.ActualHeight,
                Source = new BitmapImage(new Uri("/Classes/D_Gruppe/Grafiken/background.png", UriKind.Relative)),
                Stretch = Stretch.Fill,
                Opacity = 0.6
            };

            Canvas.SetTop(backgroundImage, 0);
            Canvas.SetLeft(backgroundImage, 0);
            canvas.Children.Add(backgroundImage);
        }

        private void DrawBird(Canvas canvas, FlappyField field)
        {
            var birdImage = new Image
            {
                Width = field.Bird.Radius * 4,
                Height = field.Bird.Radius * 4,
                Source = new BitmapImage(new Uri("/Classes/D_Gruppe/Grafiken/bird.png", UriKind.Relative))
            };
            Canvas.SetTop(birdImage, field.Bird.Y - field.Bird.Radius - 15);
            Canvas.SetLeft(birdImage, field.Bird.X - field.Bird.Radius - 17);
            canvas.Children.Add(birdImage);
            //// Vogel zeichnen
            //var bird = new Ellipse
            //{
            //    Width = field.Bird.Radius * 2,
            //    Height = field.Bird.Radius * 2,
            //    Fill = Brushes.Yellow
            //};
            //Canvas.SetTop(bird, field.Bird.Y - field.Bird.Radius);
            //Canvas.SetLeft(bird, field.Bird.X - field.Bird.Radius);
            //canvas.Children.Add(bird);
        }

        private void DrawObstacles(Canvas canvas, FlappyField field)
        {
            foreach (var tube in field.Obstacles)
            {
                var topPipeImage = new Image
                {
                    Width = tube.Width,
                    Height = tube.TopHeight,
                    Source = new BitmapImage(new Uri("/Classes/D_Gruppe/Grafiken/Pipe_oben.png", UriKind.Relative)),
                    Stretch = Stretch.Fill
                };
                Canvas.SetTop(topPipeImage, 0);
                Canvas.SetLeft(topPipeImage, tube.X);
                canvas.Children.Add(topPipeImage);

                //// Oberer Pfeiler
                //var topPillar = new Rectangle
                //{
                //    Width = tube.Width,
                //    Height = tube.TopHeight,
                //    Fill = Brushes.Green
                //};
                //Canvas.SetTop(topPillar, 0);
                //Canvas.SetLeft(topPillar, tube.X);
                //canvas.Children.Add(topPillar);

                var bottomPipeImage = new Image
                {
                    Width = tube.Width,
                    Height = field.Height - (tube.TopHeight + tube.GapSize),
                    Source = new BitmapImage(new Uri("/Classes/D_Gruppe/Grafiken/Pipe_unten.png", UriKind.Relative)),
                    Stretch = Stretch.Fill
                };
                Canvas.SetTop(bottomPipeImage, tube.TopHeight + tube.GapSize);
                Canvas.SetLeft(bottomPipeImage, tube.X);
                canvas.Children.Add(bottomPipeImage);

                //// Unterer Pfeiler
                //var bottomPillar = new Rectangle
                //{
                //    Width = tube.Width,
                //    Height = field.Height - (tube.TopHeight + tube.GapSize),
                //    Fill = Brushes.Green
                //};
                //Canvas.SetTop(bottomPillar, tube.TopHeight + tube.GapSize);
                //Canvas.SetLeft(bottomPillar, tube.X);
                //canvas.Children.Add(bottomPillar);
            }
        }

        private void DrawGround(Canvas canvas, FlappyField field)
        {
            foreach (var boden in field.Boden)
            {
                var groundImage = new Image
                {
                    Width = boden.Width + 4,
                    Height = boden.Height + 2,
                    Source = new BitmapImage(new Uri("/Classes/D_Gruppe/Grafiken/ground.png", UriKind.Relative)),
                    Stretch = Stretch.Fill
                };

                Canvas.SetTop(groundImage, boden.Y);
                Canvas.SetLeft(groundImage, boden.X);
                canvas.Children.Add(groundImage);
            }
        }

        private void DrawScore(Canvas canvas, FlappyField field)
        {
            var scoreText = new TextBlock
            {
                Text = $"{field.score}",
                FontSize = 70,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                Background = Brushes.Transparent,
                TextAlignment = TextAlignment.Center,
                Width = 100,
                Height = 300,
                FontFamily = new FontFamily("Comic Sans MS"),
                Effect = new DropShadowEffect
                {
                    Color = Colors.Black,
                    BlurRadius = 7,
                    ShadowDepth = 0,
                    Opacity = 1
                }
            };

            Canvas.SetTop(scoreText, 10);
            Canvas.SetLeft(scoreText, (canvas.ActualWidth - scoreText.Width) / 2);
            canvas.Children.Add(scoreText);
        }

        private void DrawGameOver(Canvas canvas, FlappyField field)
        {
            var gameOverText = new TextBlock
            {
                Text = $"Game Over!",
                FontSize = 60,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black,
                Background = Brushes.Red,
                TextAlignment = TextAlignment.Center,
                Width = 400,
                Height = 90,
                FontFamily = new FontFamily("Comic Sans MS")
            };

            Canvas.SetTop(gameOverText, (canvas.ActualHeight - gameOverText.Height) / 2);
            Canvas.SetLeft(gameOverText, (canvas.ActualWidth - gameOverText.Width) / 2);
            canvas.Children.Add(gameOverText);
        }

        private void DrawHighScore(Canvas canvas, FlappyField field)
        {
            var highScoreText = new TextBlock
            {
                Text = $"Highscore : {field.highscore}",
                FontSize = 40,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black,
                Background = Brushes.Red,
                TextAlignment = TextAlignment.Center,
                Width = 400,
                Height = 90,
                FontFamily = new FontFamily("Comic Sans MS")
            };

            Canvas.SetTop(highScoreText, (canvas.ActualHeight - highScoreText.Height) / 2 + 90);
            Canvas.SetLeft(highScoreText, (canvas.ActualWidth - highScoreText.Width) / 2);
            canvas.Children.Add(highScoreText);
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
        public List<D_Boden> Boden { get; set; } // Liste der Bodenteile
        public int Width { get; private set; }
        public int Height { get; private set; }

        public FlappyField(int width, int height)
        {
            Width = width;
            Height = height;
            Bird = new D_Bird(200, (height) / 2, 15, 1, 0); // Initialisierung des Vogels
            Obstacles = new List<D_Tubes>();
            Boden = null;// new List<D_Boden>()
            LoadHighscore();
        }

        public int score = 0;

        public int highscore;

        public bool gameover = false;
        private string _dirpath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Flappybird");
        private string _highscorefile ;

        public void LoadHighscore()
        {
            _highscorefile = System.IO.Path.Combine(_dirpath, "highscore.txt");

            if (!System.IO.Directory.Exists(_dirpath))
            {
               System.IO.Directory.CreateDirectory(_dirpath);
            }
           if (System.IO.File.Exists(_highscorefile))
            {
               int.TryParse(System.IO.File.ReadAllText(_highscorefile),out highscore);
            }
           else
            {
                System.IO.File.WriteAllText(_highscorefile, highscore.ToString());
                
            }
        }

        public void SaveHighscore(int sHighscore)
        {
            _highscorefile = System.IO.Path.Combine(_dirpath, "highscore.txt");
            if (System.IO.File.Exists(_highscorefile))
            {
                System.IO.File.WriteAllText(_highscorefile, sHighscore.ToString());
            }
            
        }

        public void AdoptCanvas (Canvas canvas)
        {
            Width = (int)canvas.ActualWidth;
            Height = (int)canvas.ActualHeight;

            if (Boden == null)// Initialisiere den Boden mit 50 Segmenten
            {
                Boden = new List<D_Boden>();
                for (int i = 0; i < 50; i++)
                {
                    Boden.Add(new D_Boden(i * (Width / 50), Height - 75, Width / 50, 75));
                    //Problem das die Höhe und breite nicht an die Canvas angepasst ist daopt canvas wird erst später aufgerufen
                }
            }
        }

     

        public bool CanBePaintedBy(IPaintGame painter) => painter is ID_FB_Painter;
    }

    public class FlappyRules : ID_FB_Rules
    {
        public string Name => "FlappyBirdRules";
        public IGameField CurrentField { get; private set; }

        private int speedOuG = 5; // Standard 5 wie schnell sich Hinderniss und Boden Bewegen

        private int PlayerWon = -1;

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
            field.LoadHighscore();

        }

        public bool CheckifCollision(FlappyField field)
        {
            // Vogel kollidiert mit Ober- oder Untergrenze
            if ((field.Bird.Y - field.Bird.Radius) < 0 || 
                (field.Bird.Y + field.Bird.Radius) > field.Height)
            {
                return true;
            }
            // Vogel kollidiert mit Hindernissen
            foreach (var tube in field.Obstacles)
            {
                if (tube.CheckCollision(field.Bird))
                {
                    return true;
                }
            }
            //checkt ob er mit einem Boden objekt kollidiert
            if(field.Boden != null)
            {
                foreach (var boden in field.Boden)
                {
                    if (boden.CheckCollision(field.Bird))
                    {
                        return true;
                    }
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
                    tube.MoveLeft(speedOuG); // Bewege jedes Hindernis nach links
                }
                // Entferne Hindernisse, die aus dem Bildschirmbereich sind
                field.Obstacles.RemoveAll(tube => tube.IsOutOfScreen());

                // Füge neue Hindernisse hinzu
                if (field.Obstacles.Count == 0 || field.Obstacles.Last().X < field.Width - 250)
                {
                    Random rnd = new Random();
                    int gapY = rnd.Next(50, field.Height - 250);
                    field.Obstacles.Add(new D_Tubes(field.Width - 30, gapY, 150, 40, field.Height));
                }

                // Bewege die Bodenstücke
                foreach (var boden in field.Boden)
                {
                    boden.MoveLeft(speedOuG);
                }

                // Entferne Bodenstücke, die aus dem Bildschirmbereich sind
                field.Boden.RemoveAll(boden => boden.IsOutOfScreen());

                // Füge neue Bodenstücke hinzu, um den Bildschirm zu füllen
                if (field.Boden.Count == 0 || field.Boden.Last().X + field.Boden.Last().Width < field.Width)
                {
                    var lastBoden = field.Boden.LastOrDefault();
                    int newX = lastBoden != null ? lastBoden.X + lastBoden.Width : 0;
                    field.Boden.Add(new D_Boden(newX, field.Height - 75, field.Width / 50, 75));
                }

                //Score updaten
                updateScore(field);
            }
            else
            {
                // Ende des Spiels Highscore speichern 
                field.gameover = true;
                if (field.score > field.highscore)
                {
                    field.highscore = field.score;
                    field.SaveHighscore(field.highscore);
                }
                // Keine Moves mehr 
                // Neustart Knopf
            }
        }

        public void updateScore(FlappyField field)
        {
            foreach (var tube in field.Obstacles)
            {
                if (((tube.X + tube.Width) < field.Bird.X) && (tube.X + tube.Width) > ((field.Bird.X) - speedOuG))
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
                return "Game Over! Restart with Space";
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
    public class D_Boden
    {
        public int X { get; set; } // x-Position des Bodens
        public int Y { get; set; } // y-Position des Bodens
        public int Width { get; set; } // Breite des Bodens
        public int Height { get; set; } // Höhe des Bodens

        public D_Boden(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        // Bewegt den Boden nach links
        public void MoveLeft(int speed)
        {
            X -= speed;
        }

        // Prüft, ob der Boden aus dem Bildschirmbereich heraus ist
        public bool IsOutOfScreen()
        {
            return X + Width < 0;
        }

        // Prüft, ob der Vogel mit dem Boden kollidiert
        public bool CheckCollision(D_Bird bird)
        {
            return bird.Y + bird.Radius > Y;
        }
    }
}
