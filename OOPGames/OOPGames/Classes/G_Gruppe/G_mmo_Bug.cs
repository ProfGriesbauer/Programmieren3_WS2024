using OOPGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace OOPGames
{
    public class G_Bug_Rules : IGameRules2
    {
        G_Field _field = new G_Field();

        public string Name
        {
            get { return "Bug Rules Gruppe G"; }
        }

        private int _appleCounter;

        private double _step;

        private int _tickCounter = 0;

        public IGameField CurrentField
        {
            get { return _field; }
        }

        public bool MovesPossible
        {
            get { return !CollisionWithWall(); }
        }

        public int CheckIfPLayerWon()
        {
            return 0;
        }

        public void ClearField()
        {
            _appleCounter = 0;
            _tickCounter = 0;
        }

        public void DoMove(IPlayMove move)
        {
            if (move is G_Move)
            {
                G_Move move2 = (G_Move)move;

                if (move2.xBugPosValChange != 0)
                {
                    _field.xBugVel = move2.xBugPosValChange;
                    _field.yBugVel = 0;
                }
                else if (move2.yBugPosValChange != 0)
                {
                    _field.yBugVel = move2.yBugPosValChange;
                    _field.xBugVel = 0;
                }
            }
            //Wäre für dauerhafte Geschwindigkeitserhöhung solange die Taste gedrückt ist
            //_field.xBugVel = _field.xBugVel + move2.xBugPosValChange;
        }

        public void StartedGameCall()
        {
            _field.xBugPos = _field.canvasWidth / 2;
            _field.yBugPos = _field.canvasHeight / 2;
            _field.xApplePos = 0;
            _field.yApplePos = 0;
            _field.xBugVel = 0;
            _field.yBugVel = 0;
            _field.bugDirection = 0;
            _field.appleCounter = 0;
            _field.gameLost = false;

        }

        public void TickGameCall()
        {
            _step = (_field.canvasWidth / 400);

            if (_field.xApplePos == 0 && _field.yApplePos == 0)
            {
                GenerateApple();
            }

            for (int i = 0; i <= _field.appleCounter; i++)
            {
                _step = _step + (_field.canvasWidth / 600);
            }

            if (CollisionWithWall() == true)
            {
                _field.xBugVel = 0;
                _field.yBugVel = 0;
                _field.gameLost = true;
            }

            if (_field.xBugVel == 1)
            {
                _field.xBugPos = (_field.xBugPos + _step);
            }
            else if (_field.xBugVel == -1)
            {
                _field.xBugPos = (_field.xBugPos - _step);
            }
            else if (_field.yBugVel == 1)
            {
                _field.yBugPos = (_field.yBugPos + _step);
            }
            else if (_field.yBugVel == -1)
            {
                _field.yBugPos = (_field.yBugPos - _step);
            }

            CollisionWithApple();
        }

        public void GenerateApple()
        {
            Random random = new Random();
            int randomXPos = random.Next(80, (int)_field.canvasWidth - 80);
            int randomYPos = random.Next(80, (int)_field.canvasHeight - 80);
            _field.xApplePos = randomXPos;
            _field.yApplePos = randomYPos;
        }

        public bool CollisionWithApple()
        {

            if (_field.xBugPos >= _field.xApplePos - 23 && _field.xBugPos <= _field.xApplePos + 23)
            {
                if (_field.yBugPos >= _field.yApplePos - 23 && _field.yBugPos <= _field.yApplePos + 23)
                {
                    _field.appleCounter++;
                    _field.xApplePos = 0;
                    _field.yApplePos = 0;
                    return true;
                }

            }
            return false;
        }

        public bool CollisionWithWall()
        {
            if (_field.xBugPos < 65)
            {
                return true;
            }

            if (_field.xBugPos > (_field.canvasWidth - 25) && _field.canvasWidth != 0)
            {
                return true;
            }


            if (_field.yBugPos > (_field.canvasHeight - 25) && _field.canvasHeight != 0)
            {
                return true;
            }


            if (_field.yBugPos < 65)
            {
                return true;
            }

            return false;
        }
    }



    public class G_Field : IG_GameField_Bug
    {
        double _xBugPos;
        double _yBugPos;
        double _xApplePos;
        double _yApplePos;
        double _xBugVel;
        double _yBugVel;

        int _appleCounter;

        double _canvasWidth;
        double _canvasHeight;

        int _bugDirection;

        bool _gameLost;

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is G_Painter;
        }

        public double xBugPos
        {
            get { return _xBugPos; }
            set { _xBugPos = value; }
        }
        public double yBugPos
        {
            get { return _yBugPos; }
            set { _yBugPos = value; }
        }

        public double xApplePos
        {
            get { return _xApplePos; }
            set { _xApplePos = value; }
        }
        public double yApplePos
        {
            get { return _yApplePos; }
            set { _yApplePos = value; }
        }

        public double xBugVel
        {
            get { return _xBugVel; }
            set { _xBugVel = value; }
        }

        public double yBugVel
        {
            get { return _yBugVel; }
            set { _yBugVel = value; }
        }

        public double canvasWidth
        {
            get { return _canvasWidth; }
            set { _canvasWidth = value; }
        }
        public double canvasHeight
        {
            get { return _canvasHeight; }
            set { _canvasHeight = value; }
        }

        public int bugDirection
        {
            get { return _bugDirection; }
            set { _bugDirection = value; }
        }

        public int appleCounter
        {
            get { return _appleCounter; }
            set { _appleCounter = value; }
        }

        public bool gameLost
        {
            get { return _gameLost; }
            set { _gameLost = value; }
        }
    }



    public class G_Move : IPlayMove
    {

        public int PlayerNumber => throw new NotImplementedException();

        double _xBugPosValChange;
        double _yBugPosValChange;

        public double xBugPosValChange //Werteänderung je nach Tasten Druck wird in GetMove umgesetzt
        {
            get { return _xBugPosValChange; }
            set { _xBugPosValChange = value; }
        }
        public double yBugPosValChange
        {
            get { return _yBugPosValChange; }
            set { _yBugPosValChange = value; }
        }

    }

    public class G_Bug : IHumanGamePlayer
    {
        int _playerNumber = 0;
        public string Name
        {
            get { return "Bug Player Gruppe G"; }
        }

        public int PlayerNumber
        {
            get { return 1; }
        }



        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is G_Bug_Rules;
        }

        public IGamePlayer Clone()
        {
            G_Bug clone = new G_Bug();
            return clone;
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {

            if (selection is IKeySelection)
            {
                IKeySelection keySelection = (IKeySelection)selection;


                if (field is IG_GameField_Bug)
                {
                    IG_GameField_Bug myfield = (IG_GameField_Bug)field;


                    if (keySelection.Key == System.Windows.Input.Key.A)
                    {
                        myfield.bugDirection = 1;
                        return new G_Move() { xBugPosValChange = -1 };
                    }
                    if (keySelection.Key == System.Windows.Input.Key.D)
                    {
                        myfield.bugDirection = 2;
                        return new G_Move() { xBugPosValChange = 1 };
                    }
                    if (keySelection.Key == System.Windows.Input.Key.W)
                    {
                        myfield.bugDirection = 3;
                        return new G_Move() { yBugPosValChange = -1 };
                    }
                    if (keySelection.Key == System.Windows.Input.Key.S)
                    {
                        myfield.bugDirection = 4;
                        return new G_Move() { yBugPosValChange = 1 };
                    }
                }
            }
            return null;
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _playerNumber = playerNumber;
        }
    }

    public class G_Painter : IPaintGame2
    {
        public string Name
        {
            get { return "Bug Painter Gruppe G"; }
        }

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {

            if (currentField is IG_GameField_Bug)
            {
                IG_GameField_Bug myCurrentField = (IG_GameField_Bug)currentField;
                myCurrentField.canvasHeight = canvas.ActualHeight;
                myCurrentField.canvasWidth = canvas.ActualWidth;


                canvas.Children.Clear();

                //Hintergrund Zeichnen
                var LawnImage = new Image
                {
                    Width = myCurrentField.canvasWidth,
                    Height = myCurrentField.canvasHeight,
                    Source = new BitmapImage(new Uri("/Classes/G_Gruppe/folder_bilder/Lawn.png", UriKind.Relative))
                };

                Canvas.SetTop(LawnImage, 0);
                Canvas.SetLeft(LawnImage, 0);

                canvas.Children.Add(LawnImage);

                //Rahmen Zeichnen
                Color _lineColor = Color.FromRgb(255, 0, 0);
                Brush _lineStroke = new SolidColorBrush(_lineColor);

                Line line1 = new Line() { X1 = 20, Y1 = 30, X2 = (myCurrentField.canvasWidth - 20), Y2 = 30, Stroke = _lineStroke, StrokeThickness = 3.0 };
                canvas.Children.Add(line1);
                Line line2 = new Line() { X1 = 20, Y1 = 30, X2 = 20, Y2 = (myCurrentField.canvasHeight - 30), Stroke = _lineStroke, StrokeThickness = 3.0 };
                canvas.Children.Add(line2);
                Line line3 = new Line() { X1 = 20, Y1 = (myCurrentField.canvasHeight - 30), X2 = (myCurrentField.canvasWidth - 20), Y2 = (myCurrentField.canvasHeight - 30), Stroke = _lineStroke, StrokeThickness = 3.0 };
                canvas.Children.Add(line3);
                Line line4 = new Line() { X1 = (myCurrentField.canvasWidth - 20), Y1 = (myCurrentField.canvasHeight - 30), X2 = (myCurrentField.canvasWidth - 20), Y2 = 30, Stroke = _lineStroke, StrokeThickness = 3.0 };
                canvas.Children.Add(line4);


                DrawBug(canvas, myCurrentField, myCurrentField.bugDirection);


                if (myCurrentField.xApplePos != 0 && myCurrentField.yApplePos != 0)
                {
                    DrawApple(canvas, myCurrentField);
                }


                // Score Fenster
                Rectangle rectangleScore = new Rectangle
                {
                    Width = 80,
                    Height = 30,
                    Fill = Brushes.LightBlue,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };

                Canvas.SetLeft(rectangleScore, (myCurrentField.canvasWidth / 2 - 40));
                Canvas.SetTop(rectangleScore, (myCurrentField.canvasHeight - 20));

                TextBlock textBlockScore = new TextBlock
                {
                    Text = myCurrentField.appleCounter.ToString(),
                    FontSize = 24,
                    Foreground = Brushes.Black,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Canvas.SetLeft(textBlockScore, (myCurrentField.canvasWidth / 2 - 40) + 40 / 2 - 12); // Anpassen der Position, damit der Text mittig ist
                Canvas.SetTop(textBlockScore, (myCurrentField.canvasHeight - 20) + 20 / 2 - 12);

                canvas.Children.Add(rectangleScore);
                canvas.Children.Add(textBlockScore);


                //Anzeige Spiel verloren
                if (myCurrentField.gameLost == true)
                {

                    TextBlock textBlockGameLost = new TextBlock
                    {
                        Text = "You've Lost !",
                        FontSize = 34,
                        Foreground = Brushes.Black,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };

                    Canvas.SetLeft(textBlockGameLost, (myCurrentField.canvasWidth / 2 - 85));
                    Canvas.SetTop(textBlockGameLost, (myCurrentField.canvasHeight / 2));

                    canvas.Children.Add(textBlockGameLost);
                }

            }
        }

        private void DrawBug(Canvas canvas, IG_GameField_Bug field, int bugDirection)
        {
            if (bugDirection == 0)
            {
                var bugImage = new Image
                {
                    Width = 40,
                    Height = 40,
                    Source = new BitmapImage(new Uri("/Classes/G_Gruppe/folder_bilder/Bug.png", UriKind.Relative))
                };

                Canvas.SetTop(bugImage, field.yBugPos - 40);
                Canvas.SetLeft(bugImage, field.xBugPos - 40);

                canvas.Children.Add(bugImage);
            }
            else if (bugDirection == 1)
            {
                var bugImage = new Image
                {
                    Width = 40,
                    Height = 40,
                    Source = new BitmapImage(new Uri("/Classes/G_Gruppe/folder_bilder/BugLeft.png", UriKind.Relative))
                };

                Canvas.SetTop(bugImage, field.yBugPos - 40);
                Canvas.SetLeft(bugImage, field.xBugPos - 40);

                canvas.Children.Add(bugImage);
            }
            else if (bugDirection == 2)
            {
                var bugImage = new Image
                {
                    Width = 40,
                    Height = 40,
                    Source = new BitmapImage(new Uri("/Classes/G_Gruppe/folder_bilder/BugRight.png", UriKind.Relative))
                };

                Canvas.SetTop(bugImage, field.yBugPos - 40);
                Canvas.SetLeft(bugImage, field.xBugPos - 40);

                canvas.Children.Add(bugImage);
            }
            else if (bugDirection == 3)
            {
                var bugImage = new Image
                {
                    Width = 40,
                    Height = 40,
                    Source = new BitmapImage(new Uri("/Classes/G_Gruppe/folder_bilder/Bug.png", UriKind.Relative))
                };

                Canvas.SetTop(bugImage, field.yBugPos - 40);
                Canvas.SetLeft(bugImage, field.xBugPos - 40);

                canvas.Children.Add(bugImage);
            }
            else if (bugDirection == 4)
            {
                var bugImage = new Image
                {
                    Width = 40,
                    Height = 40,
                    Source = new BitmapImage(new Uri("/Classes/G_Gruppe/folder_bilder/BugDown.png", UriKind.Relative))
                };

                Canvas.SetTop(bugImage, field.yBugPos - 40);
                Canvas.SetLeft(bugImage, field.xBugPos - 40);

                canvas.Children.Add(bugImage);
            }

        }

        private void DrawApple(Canvas canvas, IG_GameField_Bug field)
        {
            // Erstellen des Bilds
            var AppleImage = new Image
            {
                Width = 40,
                Height = 40,
                Source = new BitmapImage(new Uri("/Classes/G_Gruppe/folder_bilder/Apple.png", UriKind.Relative))
            };

            // Mittelpunkt von Bild
            double centerX = AppleImage.Width / 2;
            double centerY = AppleImage.Height / 2;

            // Positionierung des Bilds
            Canvas.SetTop(AppleImage, field.yApplePos - 40);
            Canvas.SetLeft(AppleImage, field.xApplePos - 40);

            // Bild zur Zeichenfläche hinzufügen
            canvas.Children.Add(AppleImage);

        }

        public void TickPaintGameField(Canvas canvas, IGameField currentField)
        {
            PaintGameField(canvas, currentField);
        }
    }
}
