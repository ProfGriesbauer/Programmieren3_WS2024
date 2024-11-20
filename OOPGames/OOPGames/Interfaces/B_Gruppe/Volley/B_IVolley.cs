using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public interface IB_Painter_Volley : IPaintGame
    {
        void PaintVolley(Canvas canvas, IB_Field_Volley playField);
    }

    public interface IB_Field_Volley : IGameField
    {
        //Values for the current Heigth and Width of the Canvas setted/updated by the PaintTTT function
        double Height { get; set; }
        double Width { get; set; }

        //Margin for the Playfield
        double MarginPercentage { get; }

        IB_Rules_Volley Rules_Volley { get; }
    }

    public interface IB_Rules_Volley : IGameRules
    {
        //Returns the number of a player who won using the current field
        //RETURN -1 IF NO PLAYER WON
        int CheckIfPLayerWon_Volley(IB_Field_Volley field);

        //Gets the current state of the game field; the class implementing
        //this interface should hold a game field corresponding to the rules
        //it implements
        IB_Field_Volley VolleyField { get; }
    }

    public interface IB_Player_Volley : IGamePlayer
    {
        //Values for the current Position
        double Pos_x { get; set; }
        double Pos_y { get; set; }
        double Velo_x { get; set; }
        double Velo_y { get; set; }

        double Playersize { get; set; }

        //Paints the Player on the PlayField
        Canvas B_Paint_Player(Canvas canvas);

        //
        void B_Move_Player();
    }

    public interface IB_Ball_Volley
    {
        //Values for the current Position
        double Pos_x { get; set; }
        double Pos_y { get; set; }
        double Velo_x { get; set; }
        double Velo_y { get; set; }

        double Ballsize { get; set; }

        //Paints the Player on the PlayField
        Canvas B_Paint_Ball(Canvas canvas);

        //
        void B_Move_Ball();
    }
}
