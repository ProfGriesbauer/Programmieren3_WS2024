using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{
    public interface IG_GameField_Bug : IGameField
    {
        double xBugPos { get; set;}

        double yBugPos { get; set; }

        double xApplePos { get; set; }

        double yApplePos { get; set; }

        double xBugVel { get; set; }
        
        double yBugVel { get; set; }

    }
}
