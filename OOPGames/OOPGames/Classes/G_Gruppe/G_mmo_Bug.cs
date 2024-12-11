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
            _field.xApplePos = 0;
            _field.yApplePos = 0;

            _appleCounter = 0;
            _tickCounter = 0;

        }

        public void TickGameCall()
        {
            _step = 5;

            if (_field.xApplePos == 0 && _field.yApplePos == 0)
            {
                GenerateApple();
            }

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

            CollisionWithApple();



        }

        public void GenerateApple()
        {
            Random random = new Random();
            int randomXPos = random.Next(40, (int)_field.canvasWidth);
            int randomYPos = random.Next(40, (int)_field.canvasHeight);
            _field.xApplePos = randomXPos;
            _field.yApplePos = randomYPos;
        }

        public bool CollisionWithApple()
        {

            if ((_field.xApplePos+20) == _field.xBugPos && (_field.yApplePos+20) == _field.yBugPos)//Bestimmter Bereich muss noch festgelegt werden
            {
                _appleCounter++;
                _field.xApplePos = 0;
                _field.yApplePos = 0;
                return true;
            }
            return false;
        }

        public bool CollisionWithWall()
        {


            if (_field.xBugPos < 65)
            {
                return true;
            }

            if (_field.xBugPos > (_field.canvasWidth -25) && _field.canvasWidth != 0)
            {
                return true;
            }


            if (_field.yBugPos > (_field.canvasHeight-25) && _field.canvasHeight != 0)
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

        double _canvasWidth;
        double _canvasHeight;
        double _rasterXY;
        double _gridHeight;
        double _rasterWidth;
        double _rasterHeight;
        double _rasterMiddleX;
        double _rasterMiddleY;

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is G_Painter;//omm_BugPaint;//G_Painter;
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

        public double rasterXY 
        { 
            get { return _rasterXY; } 
            set { _rasterXY = value; } 
        }
        public double gridHeight 
        { 
            get { return _gridHeight; } 
            set { _gridHeight = value; } 
        }
        public double rasterWidth 
        { 
            get { return _rasterWidth; } 
            set { _rasterWidth = value; } 
        }
        public double rasterHeight 
        { 
            get { return _rasterHeight; } 
            set {  _rasterHeight = value; } 
        }
        public double rasterMiddleX 
        { 
            get { return _rasterMiddleX; }
            set { _rasterMiddleX = value; }
        }
        public double rasterMiddleY 
        { 
            get { return _rasterMiddleY; } 
            set { _rasterMiddleY = value; }
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
            //Generieren der Apfel position - mind. +-2 felder von der momentanen bug Positon
            //Zugriff via G_Move -- Übergibt Bug Position

            if (field is IG_GameField_Bug)
            {
                IG_GameField_Bug myField = (IG_GameField_Bug)field;

                Random random = new Random();
                int randomXPos = random.Next(40, (int)myField.canvasWidth);
                int randomYPos = random.Next(40, (int)myField.canvasHeight);
                myField.xApplePos = randomXPos;
                myField.yApplePos = randomYPos;

                return null;
            }
            return null;
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
                 myCurrentField.canvasHeight = canvas.ActualHeight;
                 myCurrentField.canvasWidth = canvas.ActualWidth;


                 canvas.Children.Clear();

                 //Rahmen Zeichnen
                 Color _lineColor = Color.FromRgb(255, 0, 0);
                 Brush _lineStroke = new SolidColorBrush(_lineColor);

                 Line line1 = new Line() { X1 = 20, Y1 = 20, X2 = (myCurrentField.canvasWidth - 20), Y2 = 20, Stroke = _lineStroke, StrokeThickness = 3.0 };
                 canvas.Children.Add(line1);
                 Line line2 = new Line() { X1 = 20, Y1 = 20, X2 = 20, Y2 = (myCurrentField.canvasHeight - 20), Stroke = _lineStroke, StrokeThickness = 3.0 };
                 canvas.Children.Add(line2);
                 Line line3 = new Line() { X1 = 20, Y1 = (myCurrentField.canvasHeight - 20), X2 = (myCurrentField.canvasWidth - 20), Y2 = (myCurrentField.canvasHeight - 20), Stroke = _lineStroke, StrokeThickness = 3.0 };
                 canvas.Children.Add(line3);
                 Line line4 = new Line() { X1 = (myCurrentField.canvasWidth - 20), Y1 = (myCurrentField.canvasHeight - 20), X2 = (myCurrentField.canvasWidth - 20), Y2 = 20, Stroke = _lineStroke, StrokeThickness = 3.0 };
                 canvas.Children.Add(line4);

                /*//Bug zeichnen
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
               canvas.Children.Add(bug);*/


                DrawBug(canvas, myCurrentField);



                //Apfel zeichnen
                if (myCurrentField.xApplePos != 0 && myCurrentField.yApplePos !=0)
                {
                    DrawApple(canvas, myCurrentField);
                }

             }
         }

        private void DrawBug(Canvas canvas, IG_GameField_Bug field)
        {
            // Erstellen des Bilds
            var bugImage = new Image
            {
                Width = 40,
                Height = 40,
                Source = new BitmapImage(new Uri("/Classes/G_Gruppe/folder_bilder/Bug.png", UriKind.Relative))
            };

            // Mittelpunkt von Bild
            double centerX = bugImage.Width / 2;
            double centerY = bugImage.Height / 2;

            // Positionierung des Bilds
            Canvas.SetTop(bugImage, field.yBugPos - 40);
            Canvas.SetLeft(bugImage, field.xBugPos - 40);

            // Bild zur Zeichenfläche hinzufügen
            canvas.Children.Add(bugImage);
        }

        private void DrawApple(Canvas canvas, IG_GameField_Bug field)
        {
            // Erstellen des Bilds
            var bugImage = new Image
            {
                Width = 40,
                Height = 40,
                Source = new BitmapImage(new Uri("/Classes/G_Gruppe/folder_bilder/Apple.png", UriKind.Relative))
            };

            // Mittelpunkt von Bild
            double centerX = bugImage.Width / 2;
            double centerY = bugImage.Height / 2;

            // Positionierung des Bilds
            Canvas.SetTop(bugImage, field.yApplePos - 40);
            Canvas.SetLeft(bugImage, field.xApplePos - 40);

            // Bild zur Zeichenfläche hinzufügen
            canvas.Children.Add(bugImage);

        }

        public void TickPaintGameField(Canvas canvas, IGameField currentField)
         {
             PaintGameField(canvas, currentField);
         }
     }
 }

    public class omm_BugPaint : IPaintGame2
{
        public string Name { get { return "OMM_Bug_Paint"; } }
        public void PaintBugField(Canvas canvas, IG_GameField_Bug currentField)
        {
            PaintGameField(canvas, currentField);
        }


        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (!(currentField is IG_GameField_Bug))
            {
                return;
            }

            IG_GameField_Bug myField = (IG_GameField_Bug)currentField;

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
            //double CanvasWidth = canvas.ActualWidth; //Coordinate Maximum --> Recieve from Canvas (iPaintGame)
            //double CanvasHeight = canvas.ActualHeight; //Coordinate Maximum --> Recieve from Canvas (iPaintGame)
            //double RasterXY = 13; //Raster defines Grid size Bsp. RasterXY = 14 --> 14 Columns
            //double GridHeight = CanvasHeight - (2 * (CanvasHeight / RasterXY));
            //double RasterWidth = (CanvasWidth / RasterXY);
            //double RasterHeight = (GridHeight / RasterXY);
            //double RasterMiddleX = RasterWidth/2;
            //double RasterMiddleY = RasterHeight/2;
            myField.canvasWidth = canvas.ActualWidth;
            myField.canvasHeight = canvas.ActualHeight;
            myField.rasterXY = 0;
            myField.gridHeight = myField.canvasHeight - (2* (myField.canvasHeight / myField.rasterXY));
            myField.rasterWidth = myField.canvasWidth / myField.rasterXY;
            myField.rasterHeight =myField.gridHeight / myField.rasterXY;
            myField.rasterMiddleX = myField.rasterWidth / 2;
            myField.rasterMiddleY = myField.rasterHeight / 2;

            for (double x = 0; x <= myField.canvasWidth; x += (myField.canvasWidth / myField.rasterXY))
            {
                for (double y = 0; y <= myField.gridHeight; y += (myField.canvasHeight / myField.rasterXY))
                {
                    Line LinesVertical = new Line() { X1 = x, Y1 = 0, X2 = x, Y2 = (myField.gridHeight - thickness), Stroke = lineStroke, StrokeThickness = thickness };
                    canvas.Children.Add(LinesVertical);
                    Line LinesHorizontal = new Line() { X1 = 0, Y1 = y, X2 = (myField.canvasWidth - thickness), Y2 = y, Stroke = lineStroke, StrokeThickness = thickness };
                    canvas.Children.Add(LinesHorizontal);
                }
            }
        }

        public void TickPaintGameField(Canvas canvas, IGameField currentField)
        {
            throw new NotImplementedException();
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



 