using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
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
                    _field.xBugVel = _field.xBugVel+move2.xBugPosValChange;
                }
                else if (move2.yBugPosValChange != 0)
                {
                    _field.yBugVel = _field.yBugVel + move2.yBugPosValChange;
                }
            }
            
            //Wäre für dauerhafte Geschwindigkeitserhöhung solange die Taste gedrückt ist
            //_field.xBugVel = _field.xBugVel + move2.xBugPosValChange;
        }

        public void StartedGameCall()
        {

            _field.xBugPos = 300;
            _field.yBugPos = 300;
            
            _appleCounter = 0;
            _tickCounter = 0;

        }

        public void TickGameCall()
        {
            
            //if (_tickCounter ==30)
            //{
                _field.xBugPos = _field.xBugPos + 2;
                _field.yBugPos = _field.yBugPos + 2;

                //_tickCounter = 0;
                
            //}

            //_tickCounter++;
            
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

            int _xMiddlePoint = (int)canvas.ActualWidth / 2;
            int _yMiddlePoint = (int)canvas.ActualHeight / 2;

            int _xBoundryRight = _xMiddlePoint + (int)(canvas.ActualWidth / 2) - 10;//Wert noch nicht getestet
            int _xBoundryLeft = _xMiddlePoint - (int)(canvas.ActualWidth / 2) + 10;// Wert noch nicht getestet;

            int _yBoundryTop = _yMiddlePoint + (int)(canvas.ActualHeight / 2) - 10;//Wert noch nicht getestet;
            int _yBoundryBottom = _yMiddlePoint - (int)(canvas.ActualHeight / 2) + +10;//Wert noch nicht getestet

            if (_field.xBugPos < _xBoundryLeft)
            {
                return true;
            }

            if (_field.xBugPos > _xBoundryRight)
            {
                return true;
            }

            if (_field.yBugPos > _yBoundryTop)
            {
                return true;
            }

            if (_field.yBugPos < _yBoundryBottom)
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
            A_Human_Player clone = new A_Human_Player();
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
        
        public int  PlayerNumber => throw new NotImplementedException();

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
            
            if(selection is IKeySelection)
            {
                IKeySelection keySelection = (IKeySelection)selection;
                if (keySelection.Key == System.Windows.Input.Key.A)
                {
                    return new G_Move() { xBugPosValChange = -1 };
                }
                else if (keySelection.Key == System.Windows.Input.Key.D)
                {
                    return new G_Move() { xBugPosValChange = 1 };
                }
                else if (keySelection.Key == System.Windows.Input.Key.W)
                {
                    return new G_Move() { yBugPosValChange = -1 };
                }
                else if (keySelection.Key == System.Windows.Input.Key.S)
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
