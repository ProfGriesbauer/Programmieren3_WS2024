using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public interface IB_Painter_BV : IPaintGame2
    {
        void PaintBlobbyVolley(Canvas canvas, IB_Field_BV playField);
    }

    public interface IB_Field_BV : IGameField
    {
        //Values for the current Heigth and Width of the Canvas setted/updated by the PaintBlobbyVolley function
        double Height { get; set; }
        double Width { get; set; }

        //Margin for the Playfield
        double MarginPercentage { get; }

        //objects for the player and ball states
        IB_Ball_BV Ball { get; set; }
        IB_Player_BV Player1 { get; set; }
        IB_Player_BV Player2 { get; set; }


        IB_Rules_BV Rules_BV { get; }
    }

    public interface IB_Rules_BV : IGameRules2
    {
        //Points Player 1 Points[0]
        //Points Player 2 Points[1]
        int[] Points { get; set; }

        //Returns the number of a player who scored the current Ball
        //RETURN -1 IF NO PLAYER SCORED (Ball is still in the air)
        void CheckIfPLayerScored();

        //Resets the Game Field after a player scored
        void ScoredReset(int scorer);

        //Gets the current state of the game field; the class implementing
        //this interface should hold a game field corresponding to the rules it implements
        IB_Field_BV Field_BV { get; }
    }

    public interface IB_Move_BV : IPlayMove
    {

    }

    public interface IB_Player_BV : IGamePlayer
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

    public interface IB_HumanPlayer_BV : IB_Player_BV
    {

    }

    public interface IB_ComputerPlayer_BV : IB_Player_BV
    {

    }

    public interface IB_Ball_BV
    {
        //Values for the current Position
        double Pos_x { get; set; }
        double Pos_y { get; set; }
        double Velo_x { get; set; }
        double Velo_y { get; set; }

        double Ballsize { get; set; }

        //Paints the Player on the PlayField
        Canvas B_Paint_Ball(Canvas canvas);

        //Check if the Ball is on the ground and returns the playernumber who scored (Left = 0; Right = 1)
        //returns -1 if the ball is still in the air
        int B_On_Ground();

        //Move
        void B_Move_Ball();
    }
}
