using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public class G_Bug_Rules : IGameRules2
    {
        G_Field _field = new G_Field();

        public string Name
        {
            get { return "Human Player Gruppe A"; }
        }

        private int _appleCounter;

        private Canvas canvas = new Canvas();//Funktioniert ?


        public IGameField CurrentField => throw new NotImplementedException();
        //Wird nicht benötigt ?

        public bool MovesPossible //Wenn keine Kollision mit der Wand stattgefunden hat
        {
            get { return !CollisionWithWall(); }
        }
         

        public int CheckIfPLayerWon()
        {
            throw new NotImplementedException();
            //Framework Game Lost ????
        }

        public void ClearField()
        {
            _appleCounter = 0;

        }

        public void DoMove(IPlayMove move)
        {
            if (move is G_Move)
            {
                G_Move move2 = (G_Move)move;
                //_field.xBugVel = _field.xBugVel + move2.xBugPosValChange;
                if (move2.xBugPosValChange != 0)
                {
                    _field.xBugVel = move2.xBugPosValChange;
                }
                else if (move2.yBugPosValChange != 0)
                {
                    _field.yBugVel = move2.yBugPosValChange;
                }
            }
            //Neue Bug Position beschreiben Setter 
        }

        public void StartedGameCall()
        {
            
            //Bug Position Canvas middle (x, y)
            //Set apple count a=0

            _appleCounter = 0;

            //Über das Canvas Objekt in der Klassen Variable Sinnvoll oder direkt in der Methode erstellen ?
            //Soll ein neues Objekt von G_Move in der Methode erstellt werden oder soll das ganze über eine statische Klasse funktionieren ?


        }

        public void TickGameCall()
        {
            _field.xBugPos = _field.xBugPos + _field.xBugVel; //Geschwindigkeitserhöhung
            //Neue Position des Bugs berechnen 
            //Zähler implementieren 
            //Je nach Richtung 
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

            //Schauen ob sich Käfer und Wand treffen
            //Zugriff via G_Move -- Übergibt Bug Position

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

    //Field Klasse erstellen und Bug Position erstellen

    public class G_Field : IGameField
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
        public string Name => throw new NotImplementedException();

        public int PlayerNumber => throw new NotImplementedException();

        public bool CanBeRuledBy(IGameRules rules)
        {
            throw new NotImplementedException();
            //Verweis auf eigene Regeln
        }

        public IGamePlayer Clone()
        {
            throw new NotImplementedException();
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
            //Lassen wir weg
        }


    }

    public class G_Move : IPlayMove
    {
        
        public int  PlayerNumber => throw new NotImplementedException();//Wird nicht verwendet

        public int xBugPosValChange //Werteänderung je nach Tasten Druck wird in GetMove
        {
            get { return xBugPosValChange; }
            set { xBugPosValChange = value; }
        }
        public int yBugPosValChange
        {
            get { return yBugPosValChange; }
            set { yBugPosValChange = value; }
        }

        //public int xBug { get { return _xBug; } } Verwendung ?

        //public int yBug { get { return _yBug; } } Verwendung ?
    }

    public class G_Bug : IHumanGamePlayer
    {
        public string Name => throw new NotImplementedException();

        public int PlayerNumber => throw new NotImplementedException();
        //Wird nicht benötigt

        public bool CanBeRuledBy(IGameRules rules)
        {
            throw new NotImplementedException();
            //Eigene Regeln implementieren
        }

        public IGamePlayer Clone()
        {
            throw new NotImplementedException();
            //Objekt Klonen
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            throw new NotImplementedException();
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
                    //Je nach Taste ein neues G_Move Objekt erzeugen
                    return new G_Move() { xBugPosValChange = -1 };
                }
            }
        }

        public void SetPlayerNumber(int playerNumber)
        {
            throw new NotImplementedException();
            //Wird nicht benötigt
        }
    }

    public class G_Painter : IPaintGame
    {
        public string Name => throw new NotImplementedException();

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            throw new NotImplementedException();
        }
    }



}
