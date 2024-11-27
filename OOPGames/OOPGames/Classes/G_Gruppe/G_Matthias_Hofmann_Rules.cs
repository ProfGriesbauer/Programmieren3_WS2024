using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace OOPGames
{
    
        //Neues Interface für fertige Klasse erstellen
        public class G_Rules : IGameRules2
        {
            //Erzeugen eines Feld Objektes
            //G_Bug_Field _field = new G_Bug_Field(,);

            private int _touchedApples = 0;


            //In eigenes Objekt aussondern ?
            //Die Mittelpunkte müssen gleich die Start Punkte sein
            private double _xMiddlePoint;
            private double _yMiddlePoint;
            private double _xBoundryRight;
            private double _xBoundryLeft;
            private double _yBoundryTop;
            private double _yBoundryBottom;

            public string Name
            {
                get { return "Rules Gruppe G Bug Game"; }
            }

            public IGameField CurrentField
            {
                get { return null; }
            }

            public bool MovesPossible
            {
            get
            {
                /*if (CheckIfWallIsTouched() == false)
                    return true;
                else
                    return false;
                */
                return false;

            }
            }

            public bool CheckIfWallIsTouched(Canvas canvas,int xPositionBug, int yPositionBug) //Aktuelle Position des Käfers auf den Spielfeld
            {
                _xMiddlePoint = canvas.ActualWidth / 2;
                _yMiddlePoint = canvas.ActualHeight / 2;

                _xBoundryRight = _xMiddlePoint + (canvas.ActualWidth / 2) - 20;//Wert noch nicht getestet
                _xBoundryLeft = _xMiddlePoint - (canvas.ActualWidth / 2) + 20;// Wert noch nicht getestet;

                _yBoundryTop = _yMiddlePoint + (canvas.ActualHeight / 2) - 20;//Wert noch nicht getestet;
                _yBoundryBottom = _yMiddlePoint - (canvas.ActualHeight / 2) + +20;//Wert noch nicht getestet

                if (xPositionBug < _xBoundryLeft)
                {
                    return true;
                }

                if (xPositionBug > _xBoundryRight)
                {
                    return true;
                }

                if (yPositionBug > _yBoundryTop)
                {
                    return true;
                }

                if (yPositionBug < _yBoundryBottom)
                {
                    return true;
                }

                return false;

            //Über If abfragen checken ob der die Position des Käfers gleich oder größer der Grenzen ist 
            //Mitthilfe der Grenzen Arrays mit Reihen und Spalten erzeugen 
            //Die Maximale Spalte, Reihe ist die Grenze
            //If abfrage ob die Reihe,Spalte des Käfer Arrays (1Wert) größer oder Gleich ist 
            //der Anzahl der Reihen im GrenzenArray 

        }

            public int CheckIfAppleIsTouched(int xPositionApple, int yPositionApple, int xPositionBug, int yPositionBug)
            {
                if (xPositionApple == xPositionBug && yPositionApple == yPositionBug)
            {
                    _touchedApples++;
                }
                return _touchedApples;
            }

            public int CheckIfPLayerWon()
            {
                throw new NotImplementedException();
                //Check if Player Lost => Framework durchsuchen
            }

            public void ClearField()
            {
                //Anzahl der Spalten und Anzahl der Reihen werden vom Field Objekt vorgegeben
                //In jedes Feld wird eine 0 geschreiben um den Käfer vom Feld zu löschen 
                //Oder ihn Alternativ wieder auf einen Startpunkt zu setzen 
                /*for (int row = 0; row < _field.rows; row++)
                {
                    for (int column = 0; column < _field.columns; column++)
                    {
                        _field[row, column] = 0;
                    }
                }*/
            }

            public void DoMove(IPlayMove move)
            {
                if (move is G_Bug_Move)
                {
                    //Cast durchführen um zu zeigen das move von A_Move ist, somit kann auf Row und Column zugegriffen werden
                    G_Bug_Move mymove = (G_Bug_Move)move;
                    //Überprüfen ob der eingegebe Wert zulässig ist if()
                    //_field[mymove.Row, mymove.Column] = mymove.PlayerNumber;
                }
            }

            public void StartedGameCall()
            {
                //Called whenever a game is startet
                //Implement this function to i.e. ask for game sizes, etc.
                throw new NotImplementedException();
            }

            public void TickGameCall()
            {
                //Called along with the painting tick every 40ms
                //Implement this function to do "realtime" games.
                throw new NotImplementedException();
            }
     
    }
 }


   


 

