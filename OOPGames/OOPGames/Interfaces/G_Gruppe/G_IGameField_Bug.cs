using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{
    public interface IG_GameField_Bug : IGameField
    {
        double xBugPos { get; set; }

        double yBugPos { get; set; }

        double xApplePos { get; set; }

        double yApplePos { get; set; }

        double xBugVel { get; set; }

        double yBugVel { get; set; }


        D_Bug Bug { get; set; } // Der Vogel als Objekt


    }

    public class D_Bug
    {

        public int X { get; private set; } // Die x-Position des Vogels
        public int Y { get; private set; } // Die y-Position des Vogels
        public int Radius { get; private set; } // Der Radius des Vogels für die Hitbox
        public int Acceleration { get; private set; } // Die Beschleunigunf des Vogels nach unten 
        public int Velocity { get; private set; } // Die Geschwindigkeit mit der sich der Vogel in Y richtung bewegt


        // Konstruktor
        public D_Bug(int x, int y, int radius, int acceleration, int velocity)
        {
            X = x;
            Y = y;
            Radius = radius;
            Acceleration = acceleration;
            Velocity = velocity;
        }

    }

}
