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
            throw new NotImplementedException();
            //Framework Game Lost ????
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
                }
                else if (move2.yBugPosValChange != 0)
                {
                    _field.yBugVel = move2.yBugPosValChange;
                }
            }
            
            //Wäre für dauerhafte Geschwindigkeitserhöhung solange die Taste gedrückt ist
            //_field.xBugVel = _field.xBugVel + move2.xBugPosValChange;
        }

        public void StartedGameCall()
        {

            _field.xBugPos = canvas.ActualWidth / 2;
            _field.yBugPos = canvas.ActualHeight / 2;
            
            _appleCounter = 0;
            _tickCounter = 0;

        }

        public void TickGameCall()
        {
            
            if (_tickCounter ==30)
            {
                _field.xBugPos = _field.xBugPos + _field.xBugVel;
                _field.yBugPos = _field.yBugPos + _field.yBugVel;

                _tickCounter = 0;
                
            }

            _tickCounter++;
            
        }

        public bool CollisionWithApple(int xPosApple, int yPosApple)
        {

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
        public bool CanBePaintedBy(IPaintGame painter)
        {
            throw new NotImplementedException();
        }

        public double xBugPos
        {
            get { return xBugPos; }
            set { xBugPos = value; }
        }
        public double yBugPos
        {
            get { return yBugPos; }
            set { yBugPos = value; }
        }

        public double xApplePos
        {
            get { return xApplePos; }
            set { xApplePos = value; }
        }
        public double yApplePos
        {
            get { return yApplePos; }
            set { yApplePos = value; }
        }

        public double xBugVel
        {
            get { return xBugVel; }
            set { xBugVel = value; }
        }

        public double yBugVel
        {
            get { return yBugVel; }
            set { yBugVel = value; }
        }
    }

    public class G_Apple : IComputerGamePlayer
    {
        public string Name
        {
            get { return "Apple Gruppe G"; }
        }

        public int PlayerNumber => throw new NotImplementedException();

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
            throw new NotImplementedException();
            //Generieren der Apfel position - mind. +-2 felder von der momentanen bug Positon
            //Zugriff via G_Move -- Übergibt Bug Position
        }

        public void SetPlayerNumber(int playerNumber)
        {
            throw new NotImplementedException();
        }


    }

    public class G_Move : IPlayMove
    {
        
        public int  PlayerNumber => throw new NotImplementedException();

        public double xBugPosValChange //Werteänderung je nach Tasten Druck wird in GetMove umgesetzt
        {
            get { return xBugPosValChange; }
            set { xBugPosValChange = value; }
        }
        public double yBugPosValChange
        {
            get { return yBugPosValChange; }
            set { yBugPosValChange = value; }
        }

    }

    public class G_Bug : IHumanGamePlayer
    {
        public string Name
        {
            get { return "Bug Player Gruppe G"; }
        }

        public int PlayerNumber => throw new NotImplementedException();
        

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is G_Bug_Rules;
        }

        public IGamePlayer Clone()
        {
            A_Human_Player clone = new A_Human_Player();
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
                if (keySelection.Key == System.Windows.Input.Key.Left)
                {
                    return new G_Move() { xBugPosValChange = -1 };
                }
                else if (keySelection.Key == System.Windows.Input.Key.Right)
                {
                    return new G_Move() { xBugPosValChange = 1 };
                }
                else if (keySelection.Key == System.Windows.Input.Key.Up)
                {
                    return new G_Move() { yBugPosValChange = -1 };
                }
                else if (keySelection.Key == System.Windows.Input.Key.Down)
                {
                    return new G_Move() { yBugPosValChange = 1 };
                }
            }
            return null;
        }

        public void SetPlayerNumber(int playerNumber)
        {
            throw new NotImplementedException();
        }
    }

    public class G_Painter : IPaintGame
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
    }



}
