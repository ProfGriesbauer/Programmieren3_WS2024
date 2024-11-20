using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public interface ID_FB_Painter : IPaintGame2
    {

    }

    public interface ID_FB_GameField : IGameField
    {

    }

    public interface ID_FB_Rules : IGameRules2
    { 
    
    }

    public interface ID_FB_Move : IPlayMove
    {

    }

    public interface ID_FB_HumanPlayer : IHumanGamePlayer
    {

    }
}
