﻿using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;

namespace OOPGames
{
    public class FlappyPainter : D_FB_Base_FlappyPainter
    {
        public override string Name => "FlappyBirdPainter";

        public override void PaintFlappyField(Canvas canvas, ID_FB_GameField currentField)
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

            if (field.gameover)
            {
                DrawGameOverPicture(canvas, field);
                DrawGameOver(canvas, field);
                DrawHighScore(canvas, field);
                DrawScore2(canvas, field);
            }
            else
            {
                DrawScore(canvas, field);
            }
        }

        public override void FlappyTickGameField(Canvas canvas, ID_FB_GameField currentField)
        {
            PaintFlappyField(canvas, currentField);
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
        public static double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private void DrawBird(Canvas canvas, FlappyField field)
        {
            // Erstellen des Bilds
            var birdImage = new Image
            {
                Width = field.Bird.Radius * 4,
                Height = field.Bird.Radius * 4.2,
                Source = new BitmapImage(new Uri("/Classes/D_Gruppe/Grafiken/bird.png", UriKind.Relative))
            };

            // Mittelpunkt für die Rotation setzen
            double centerX = birdImage.Width / 2;
            double centerY = birdImage.Height / 2;

            // Berechnung des Rotationswinkels basierend auf Geschwindigkeit (Richtung umkehren)
            double rotationAngle = Clamp(field.Bird.Velocity * 3, -45, 45); // Positiv = nach unten, Negativ = nach oben

            // Transformation anwenden
            var rotateTransform = new RotateTransform(rotationAngle, centerX, centerY);
            birdImage.RenderTransform = rotateTransform;

            // Positionierung des Bilds
            Canvas.SetTop(birdImage, field.Bird.Y - field.Bird.Radius - 14);
            Canvas.SetLeft(birdImage, field.Bird.X - field.Bird.Radius - 17);

            // Bild zur Zeichenfläche hinzufügen
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

                var calculatedHeight = field.Height - (tube.TopHeight + tube.GapSize);
                var bottomPipeImage = new Image
                {
                    Width = tube.Width,
                    Height = Math.Max(0, calculatedHeight), // Mindesthöhe von 0
                    Source = new BitmapImage(new Uri("/Classes/D_Gruppe/Grafiken/Pipe_unten.png", UriKind.Relative)),
                    Stretch = Stretch.Fill
                };
                // Sicherstellen, dass Canvas.SetTop nur bei positiver Höhe sinnvoll ist
                Canvas.SetTop(bottomPipeImage, Math.Max(tube.TopHeight + tube.GapSize, 0));
                Canvas.SetLeft(bottomPipeImage, tube.X);

                // Bild nur hinzufügen, wenn es sichtbar ist (Höhe > 0)
                if (bottomPipeImage.Height > 0)
                {
                    canvas.Children.Add(bottomPipeImage);
                }
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
                Text = "Game Over!",
                FontSize = 60,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black,
                Background = Brushes.Transparent,
                TextAlignment = TextAlignment.Center,
                FontFamily = new FontFamily("Comic Sans MS")
            };

            gameOverText.Measure(new Size(canvas.ActualWidth, canvas.ActualHeight));
            double textTop = (canvas.ActualHeight - gameOverText.DesiredSize.Height) / 2 - 50;
            double textLeft = (canvas.ActualWidth - gameOverText.DesiredSize.Width) / 2;

            Canvas.SetTop(gameOverText, textTop);
            Canvas.SetLeft(gameOverText, textLeft);
            canvas.Children.Add(gameOverText);
        }

        public void DrawGameOverPicture(Canvas canvas, FlappyField field)
        {
            // Berechne die Größe und Position des Bildes basierend auf der Canvas-Größe und dem gewünschten Abstand
            double margin = 70;
            double imageWidth = canvas.ActualWidth - 2 * margin;
            double imageHeight = canvas.ActualHeight - 2 * margin;

            var GameOverImage = new Image
            {
                Width = imageWidth,
                Height = imageHeight,
                Source = new BitmapImage(new Uri("/Classes/D_Gruppe/Grafiken/Highscore.png", UriKind.Relative)),
                Stretch = Stretch.Fill
            };

            Canvas.SetTop(GameOverImage, margin);
            Canvas.SetLeft(GameOverImage, margin);
            canvas.Children.Add(GameOverImage);
        }

        private void DrawHighScore(Canvas canvas, FlappyField field)
        {
            var highScoreText = new TextBlock
            {
                Text = $"Highscore : {field.highscore}",
                FontSize = 40,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black,
                Background = Brushes.Transparent,
                TextAlignment = TextAlignment.Center,
                FontFamily = new FontFamily("Comic Sans MS")
            };

            highScoreText.Measure(new Size(canvas.ActualWidth, canvas.ActualHeight));
            double textTop = (canvas.ActualHeight - highScoreText.DesiredSize.Height) / 2 + 30;
            double textLeft = (canvas.ActualWidth - highScoreText.DesiredSize.Width) / 2;

            Canvas.SetTop(highScoreText, textTop);
            Canvas.SetLeft(highScoreText, textLeft);
            canvas.Children.Add(highScoreText);
        }

        public void DrawScore2(Canvas canvas, FlappyField field)
        {
            var scoreText = new TextBlock
            {
                Text = $"Score: {field.score}",
                FontSize = 40,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black,
                Background = Brushes.Transparent,
                TextAlignment = TextAlignment.Center,
                FontFamily = new FontFamily("Comic Sans MS")
            };

            scoreText.Measure(new Size(canvas.ActualWidth, canvas.ActualHeight));
            double textTop = (canvas.ActualHeight - scoreText.DesiredSize.Height) / 2 + 80;
            double textLeft = (canvas.ActualWidth - scoreText.DesiredSize.Width) / 2;

            Canvas.SetTop(scoreText, textTop);
            Canvas.SetLeft(scoreText, textLeft);
            canvas.Children.Add(scoreText);
        }

    }

    public class FlappyField : D_FB_Base_FlappyFiedl
    {
        public override D_Bird Bird { get; set; } // Der Vogel als Objekt
        public override List<D_Tubes> Obstacles { get; set; }
        public override List<D_Boden> Boden { get; set; } // Liste der Bodenteile
        public override int Width { get; set; }
        public override int Height { get; set; }

        public int score = 0;

        public int highscore;

        public bool gameover = false;

        private string _dirpath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Flappybird");

        private string _highscorefile;

        public FlappyField(int width, int height)
        {
            Width = width;
            Height = height;
            Bird = new D_Bird(200, (height) / 2, 15, 3, 0); // Initialisierung des Vogels
            Obstacles = new List<D_Tubes>();
            Boden = null; // new List<D_Boden>()
            LoadHighscore();
        }

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
    }

    public class FlappyRules : D_FB_Base_FlappyRules
    {
        public override string Name => "FlappyBirdRules";

        public new FlappyField CurrentField { get; set; }

        public override ID_FB_GameField FlappyField { get { return CurrentField; } }

        private int gapSize = 140;

        private int tubeDistance = 250;

        private int speedOuG = 7; //wie schnell sich Hinderniss und Boden Bewegen

        private int PlayerWon = -1;

        private int birdjump = -22;

        public FlappyRules()
        {
            CurrentField = new FlappyField(800, 600);
        }

        public override bool MovesPossible // Solange der Spieler nicht verloren hat.
        {
            get
            {
                if (CheckifCollision(CurrentField))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public override void DoFlappyMove(ID_FB_Move move)
        {
            if (move is FlappyMove)
            {
                var bird = (CurrentField).Bird;
                bird.moveUp(birdjump); // Vogel springt nach oben
            }
        }

        public override void ClearField()
        {
            CurrentField = new FlappyField(800, 600);
            PlayerWon = -1;
        }

        public override int CheckIfPLayerWon() => PlayerWon;

        public override void StartedGameCall()
        {
            // Initialisierung vom Score
            var field = CurrentField;
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
            // Prüfe Kollision mit allen ID_GameObjekten (Hindernisse und Boden)
            foreach (var obj in (field.Obstacles ?? Enumerable.Empty<ID_GameObject>())
                         .Concat(field.Boden ?? Enumerable.Empty<ID_GameObject>()))
            {
                if (obj.CheckCollision(field.Bird))
                {
                    return true;
                }
            }


            return false;
        }

        public override void TickGameCall()
        {
            var field = CurrentField;

            if (!CheckifCollision(field))
            {
                // LevelUp -> schnellere Obstacles, kleinere Gapsize, größerer Abstand der Tubes
                levelup(field);

                // Aktualisiere die Position des Vogels
                field.Bird.UpdatePosition();

                // Bewege und aktualisiere alle ID_GameObjekte
                foreach (var obj in (field.Obstacles ?? Enumerable.Empty<ID_GameObject>())
                             .Concat(field.Boden ?? Enumerable.Empty<ID_GameObject>()))
                {
                    obj.MoveLeft(speedOuG);
                }

                // Entferne ID_GameObjekte, die aus dem Bildschirmbereich sind
                field.Obstacles?.RemoveAll(obj => obj.IsOutOfScreen());
                field.Boden?.RemoveAll(obj => obj.IsOutOfScreen());

                // Füge neue Hindernisse hinzu
                AddNewObstacles(field);

                // Füge neue Bodenstücke hinzu
                AddNewGround(field);

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
            }
        }

        private void AddNewObstacles(FlappyField field)
        {
            int MaxGapOffset = 325; // Maximaler Abstand zwischen den Gaps

            if (field.Obstacles.Count == 0 || field.Obstacles.Last().X < field.Width - tubeDistance)
            {
                Random rnd = new Random();

                int previousGapY = field.Obstacles.Count > 0
                    ? ((D_Tubes)field.Obstacles.Last()).TopHeight
                    : field.Height / 2; // Standardhöhe, falls keine Pfeiler vorhanden sind

                // Begrenze den neuen Gap innerhalb von MaxGapOffset
                int minGapY = Math.Max(50, previousGapY - MaxGapOffset); // Lücke nicht oberhalb 50
                int maxGapY = Math.Min(field.Height - 250, previousGapY + MaxGapOffset); // Lücke nicht unterhalb der unteren Grenze

                int gapY = rnd.Next(minGapY, maxGapY + 1);

                // Neues Hindernis erstellen
                field.Obstacles.Add(new D_Tubes(field.Width - 30, gapY, gapSize, 50, field.Height));
            }
        }

        private void AddNewGround(FlappyField field)
        {
            if (field.Boden.Count == 0 || field.Boden.Last().X + field.Boden.Last().Width < field.Width)
            {
                var lastBoden = field.Boden.LastOrDefault();
                int newX = lastBoden != null ? lastBoden.X + lastBoden.Width : 0;
                field.Boden.Add(new D_Boden(newX, field.Height - 75, field.Width / 50, 75));
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

        public override string StatusBar()
        {
            if (!CheckifCollision(CurrentField))
            {
                return "Jump with Key U!";
            }
            else
            {
                return "Game Over! Restart with Space";
            }
        }

        public void levelup(FlappyField field)
        {
            if(field.score <= 10 )
            {
                tubeDistance = 200;
                gapSize = 180;
                speedOuG = 5;
            }
            if (field.score <= 20 && field.score > 10)
            {
                tubeDistance = 220;
                gapSize = 170;
                speedOuG = 6;
            }
            if (field.score <= 30 && field.score > 20)
            {
                tubeDistance = 230;
                gapSize = 160;
                speedOuG = 7;
            }
            if (field.score <= 40 && field.score > 30)
            {
                tubeDistance = 240;
                gapSize = 140;
                speedOuG = 8;
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

    public class FlappyPlayer : D_FB_Base_HumanFlappyPlayer
    {
        public override string Name => "FlappyBirdPlayer";
        int _playerNumber = 0;

        public override int PlayerNumber => _playerNumber;

        public override void SetPlayerNumber(int playerNumber) => _playerNumber = playerNumber;

        public override ID_FB_Move GetMove(IMoveSelection selection, ID_FB_GameField field)
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

        public override IGamePlayer Clone()
        { 
            FlappyPlayer fbhumanplayer = new FlappyPlayer();
            fbhumanplayer.SetPlayerNumber(_playerNumber);
            return fbhumanplayer;
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
            if (Velocity < 22)
            {
                Velocity += Acceleration;
            }

            Y += Velocity;
        }
    }

    public class D_Tubes : ID_GameObject
    {
        public int X { get; private set; } // Die x-Position der Hindernisse
        public int Y => 0; // Y ist nicht relevant, wird als 0 gesetzt
        public int Width { get; private set; } // Breite der Pfeiler
        public int Height => TopHeight + GapSize; // Gesamthöhe


        public int TopHeight { get; private set; } // Höhe des oberen Pfeilers
        public int GapSize { get; private set; } // Größe der Lücke zwischen den Pfeilern


        // Konstruktor
        public D_Tubes(int x, int topHeight, int gapSize, int width, int screenHeight)
        {
            X = x;
            TopHeight = topHeight;
            GapSize = gapSize;
            Width = width;
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

    public class D_Boden : ID_GameObject
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
