using OOPGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        private int _step;

        private Canvas canvas = new Canvas();

        private int _tickCounter = 0;


        public IGameField CurrentField
        {
            get { return _field; }
        }


        public bool MovesPossible
        {
            //get { return !CollisionWithWall(); }
            get { return true; }
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

            _field.xBugPos = 250;
            _field.yBugPos = 250;

            _appleCounter = 0;
            _tickCounter = 0;

        }

        public void TickGameCall()
        {
            _step = 5;

            for (int i = 0; i <= _appleCounter; i++)
            {
                _step = _step + 1;
            }

            if (CollisionWithWall() == true)
            {
                _field.xBugVel = 0;
                _field.yBugVel = 0;
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



        }

        public bool CollisionWithApple(int xPosApple, int yPosApple)
        {
            //Schauen ob Apfel und Käfer sich treffen
            //Zugriff via G_Move -- Übergibt Bug Position

            if (xPosApple == _field.xBugPos && yPosApple == _field.yBugPos)//Bestimmter Bereich muss noch festgelegt werden
            {
                _appleCounter++;
                return true;
            }
            return false;
        }

        public bool CollisionWithWall()
        {

            /*int _xMiddlePoint = (int)canvas.ActualWidth / 2;
            int _yMiddlePoint = (int)canvas.ActualHeight / 2;

            int _xBoundryRight = _xMiddlePoint + (int)(canvas.ActualWidth / 2) - 10;//Wert noch nicht getestet
            int _xBoundryLeft = _xMiddlePoint - (int)(canvas.ActualWidth / 2) + 10;// Wert noch nicht getestet;

            int _yBoundryTop = _yMiddlePoint + (int)(canvas.ActualHeight / 2) - 10;//Wert noch nicht getestet;
            int _yBoundryBottom = _yMiddlePoint - (int)(canvas.ActualHeight / 2) + +10;//Wert noch nicht getestet*/



            if (_field.xBugPos < 20)
            {
                return true;
            }

            if (_field.xBugPos > (canvas.ActualWidth - 20) && canvas.ActualWidth != 0)
            {
                return true;
            }

            if (_field.xBugPos > (518 - 20 - 40))
            {
                return true;
            }

            if (_field.yBugPos > (canvas.ActualHeight - 20) && canvas.ActualHeight != 0)
            {
                return true;
            }

            if (_field.yBugPos > (527 - 20 - 20))
            {
                return true;
            }

            if (_field.yBugPos < 20)
            {
                return true;
            }

            return false;
        }
    }


    //Nur getter als static ausführen ?

    public class G_Field : IG_GameField_Bug
    {
        double _xBugPos;
        double _yBugPos;
        double _xApplePos;
        double _yApplePos;
        double _xBugVel;
        double _yBugVel;
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
    }

    public class G_Apple : IComputerGamePlayer
    {
        int _playerNumber = 0;
        public string Name
        {
            get { return "Apple Gruppe G"; }
        }

        public int PlayerNumber
        {
            get { return 2; }
        }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is G_Bug_Rules;
        }

        public IGamePlayer Clone()
        {
            G_Apple clone = new G_Apple();
            return clone;
        }

        public IPlayMove GetMove(IGameField field)
        {
            Random random = new Random();
            int xPosApple = random.Next(0, 9); // Liefert einen Wert zwischen 0 und 8
            Random random1 = new Random();
            int yPosApple = random1.Next(0, 9); // Liefert einen Wert zwischen 0 und 8

            // Generieren der Apfelposition - mind. +-2 Felder von der momentanen Bug-Position
            // Zugriff via G_Move -- Übergibt Bug-Position

            return new G_Move() { xApplePosValChange = xPosApple, yApplePosValChange = yPosApple };
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _playerNumber = playerNumber;
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

        public int xApplePosValChange //Werteänderung je nach Tasten Druck wird in GetMove
        {
            get { return xApplePosValChange; }
            set { xApplePosValChange = value; }
        }
        public int yApplePosValChange
        {
            get { return yApplePosValChange; }
            set { yApplePosValChange = value; }
        }

        //public int xBug { get { return _xBug; } } Verwendung ?

        //public int yBug { get { return _yBug; } } Verwendung ?
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
            //Über Key Selection die Richtung abspeichern
            //Arrow Key UP --> Dir = 0
            //Arrow Key Right --> Dir = 1
            //Arrow Key Down --> Dir = 2
            //Arrow Key Left --> Dir = 3

            if (selection is IKeySelection)
            {
                IKeySelection keySelection = (IKeySelection)selection;
                if (keySelection.Key == System.Windows.Input.Key.A)
                {
                    return new G_Move() { xBugPosValChange = -1 };
                }
                if (keySelection.Key == System.Windows.Input.Key.D)
                {
                    return new G_Move() { xBugPosValChange = 1 };
                }
                if (keySelection.Key == System.Windows.Input.Key.W)
                {
                    return new G_Move() { yBugPosValChange = -1 };
                }
                if (keySelection.Key == System.Windows.Input.Key.S)
                {
                    return new G_Move() { yBugPosValChange = 1 };
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



                 canvas.Children.Clear();

                 //Rahmen Zeichnen
                 Color _lineColor = Color.FromRgb(255, 0, 0);
                 Brush _lineStroke = new SolidColorBrush(_lineColor);

                 Line line1 = new Line() { X1 = 20, Y1 = 20, X2 = (canvas.ActualWidth - 20), Y2 = 20, Stroke = _lineStroke, StrokeThickness = 3.0 };
                 canvas.Children.Add(line1);
                 Line line2 = new Line() { X1 = 20, Y1 = 20, X2 = 20, Y2 = (canvas.ActualHeight - 20), Stroke = _lineStroke, StrokeThickness = 3.0 };
                 canvas.Children.Add(line2);
                 Line line3 = new Line() { X1 = 20, Y1 = (canvas.ActualHeight - 20), X2 = (canvas.ActualWidth - 20), Y2 = (canvas.ActualHeight - 20), Stroke = _lineStroke, StrokeThickness = 3.0 };
                 canvas.Children.Add(line3);
                 Line line4 = new Line() { X1 = (canvas.ActualWidth - 20), Y1 = (canvas.ActualHeight - 20), X2 = (canvas.ActualWidth - 20), Y2 = 20, Stroke = _lineStroke, StrokeThickness = 3.0 };
                 canvas.Children.Add(line4);

                 //Bug zeichnen
                 Rectangle bug = new Rectangle
                 {
                     Width = 40,
                     Height = 20,
                     Fill = Brushes.White,
                     Stroke = Brushes.Black,
                     StrokeThickness = 1
                 };

                 // Position der Zelle auf der Canvas setzen
                 Canvas.SetLeft(bug, myCurrentField.xBugPos);
                 Canvas.SetTop(bug, myCurrentField.yBugPos);

                 // Zelle zum Canvas hinzufügen
                 canvas.Children.Add(bug);

             }
         }

         public void TickPaintGameField(Canvas canvas, IGameField currentField)
         {
             PaintGameField(canvas, currentField);
         }
     }
 }

    public class omm_BugPaint : OMM_BugGamePaint
    {
        public string Name { get { return "OMM_Bug_Paint"; } }
        public void PaintBugField(Canvas canvas, OMM_BugField currentField)
        {
            PaintGameField(canvas, currentField);
        }


        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (!(currentField is OMM_BugField))
            {
                return;
            }

            OMM_BugField myField = (OMM_BugField)currentField;

            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(0, 0, 0);
            canvas.Background = new SolidColorBrush(bgColor);
            Color lineColor = Color.FromRgb(255, 128, 0);
            Brush lineStroke = new SolidColorBrush(lineColor);
            Color BugColor = Color.FromRgb(0, 255, 0);
            Brush BugStroke = new SolidColorBrush(BugColor);
            Color AppleColor = Color.FromRgb(0, 0, 255);
            Brush AppleStroke = new SolidColorBrush(AppleColor);
            double thickness = 2.0;
            double CanvasWidth = canvas.ActualWidth; //Coordinate Maximum --> Recieve from Canvas (iPaintGame)
            double CanvasHeight = canvas.ActualHeight; //Coordinate Maximum --> Recieve from Canvas (iPaintGame)
            double RasterXY = 13; //Raster defines Grid size Bsp. RasterXY = 14 --> 14 Columns
            double GridHeight = CanvasHeight - (2 * (CanvasHeight / RasterXY));
            double RasterWidth = (CanvasWidth / RasterXY);
            double RasterHeight = (GridHeight / RasterXY);
            double RasterMiddleX = RasterWidth/2;
            double RasterMiddleY = RasterHeight/2;

            for (double x = 0; x <= CanvasWidth; x += (CanvasWidth / RasterXY))
            {
                for (double y = 0; y <= GridHeight; y += (CanvasHeight / RasterXY))
                {
                    Line LinesVertical = new Line() { X1 = x, Y1 = 0, X2 = x, Y2 = (GridHeight - thickness), Stroke = lineStroke, StrokeThickness = thickness };
                    canvas.Children.Add(LinesVertical);
                    Line LinesHorizontal = new Line() { X1 = 0, Y1 = y, X2 = (CanvasWidth - thickness), Y2 = y, Stroke = lineStroke, StrokeThickness = thickness };
                    canvas.Children.Add(LinesHorizontal);
                }
            }
        }

        public void TickPaintGameField(Canvas canvas, IG_GameField_Bug currentField)
        {
            throw new NotImplementedException();
        }


        private void DrawBug(Canvas canvas, IG_GameField_Bug field)
        {
            // Erstellen des Bilds
            var bugImage = new Image
            {
                Width = field.Bug.Radius * 4,
                Height = field.Bug.Radius * 4.2,
                Source = new BitmapImage(new Uri("/Classes/G_Gruppe/Bilder/Bug.png", UriKind.Relative))
            };

            // Mittelpunkt von Bild
            double centerX = bugImage.Width / 2;
            double centerY = bugImage.Height / 2;

            // Positionierung des Bilds
            Canvas.SetTop(bugImage, field.Bug.Y - field.Bug.Radius - 14);
            Canvas.SetLeft(bugImage, field.Bug.X - field.Bug.Radius - 17);

            // Bild zur Zeichenfläche hinzufügen
            canvas.Children.Add(bugImage);

        }
    }



            //Apple Spawn Random in Center and Paint
            //Apple Cannot Spawn at Current BugPos or in a 3x3 Grid around the Bug
            //Assumes BugPos is measured from the center of the Grid

/*List<int> AppleSpawnExclusionZoneX = new List<int> { xBugPos - (CanvasWidth / RasterXY), xBugPos, xBugPos + (CanvasWidth / RasterXY) };
List<int> AppleSpawnExclusionZoneY = new List<int> { yBugPos - (CanvasHeight / RasterXY), yBugPos, yBugPos + (CanvasHeight / RasterXY) };

Random RasterRandomX = new Random();
int AppleSpawnGridX = GenerateRandomNumber(0, RasterXY, AppleSpawnExclusionZoneX, RasterRandomX); //Select Column

Random RasterRandomY = new Random();
int AppleSpawnGridY = GenerateRandomNumber(0, (RasterXY-2), AppleSpawnExclusionZoneY, RasterRandomY); //select a Row

Console.WriteLine("Apple Spawn Coordinates: " + AppleSpawnGridX + ", " + AppleSpawnGridY);


int GenerateRandomNumber(int lower, int upper, List<int> exclusions, Random rand)
{
    int number;
    do
    {
        number = rand.Next(lower, upper + 1);
    }
    while (exclusions.Contains(number));

    return number;
}

int AppleSpawnPosX = (AppleSpawnGridX * (CanvasWidth / RasterXY)) + ((CanvasWidth / RasterXY) / 2);
int AppleSpawnPosY = (AppleSpawnGridY * (GridHeight / RasterXY)) + ((GridHeight / RasterXY) / 2);

Ellipse Apple = new Ellipse() { Margin = new Thickness(AppleSpawnPosX, AppleSpawnPosY, 0, 0), Width = RasterWidth, Height = RasterHeight, Stroke = AppleStroke, StrokeThickness = 5.0 }; //Paint Apple
canvas.Children.Add(Apple);
*/




