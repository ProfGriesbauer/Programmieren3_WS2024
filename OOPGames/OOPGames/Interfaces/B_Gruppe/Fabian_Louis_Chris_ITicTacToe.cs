using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public interface IB_Painter_TTT : IPaintGame
    {
        void PaintTTT(Canvas canvas, IB_Field_TTT playField);

    }

    public interface IB_Field_TTT : IGameField
    {
        //Values for the current Heigth and Width of the Canvas setted/updated by the PaintTTT function
        double Height { get; set; }
        double Width { get; set; }

        //Margin for the Playfield
        double MarginPercentage { get; }

     
        IB_Rules_TTT Rules_TTT { get; }

        // 3x3 TicTacToe Field
        //O: unuesed
        //1: Player 1
        //2: Player 2
        int this[int r, int c] { get; set; }
    }

    public interface IB_Rules_TTT : IGameRules
    {
        //Adds the given move to the current tictactoe field if possible
        void DoMoveTTT(IB_Move_TTT move);

        //Returns the number of a player who won using the current field
        //RETURN -1 IF NO PLAYER WON
        int CheckIfPLayerWon_TTT(IB_Field_TTT field);

        //Gets the current state of the game field; the class implementing
        //this interface should hold a game field corresponding to the rules
        //it implements
        IB_Field_TTT TTTField { get; }
    }

    public interface IB_Move_TTT : IPlayMove
    {
        int Column { get; }
        int Row { get; }

    }
    public interface IB_HumanPlayer_TTT : IHumanGamePlayer
    {
        IB_Move_TTT GetTTTMove(IB_Field_TTT field, IClickSelection selection);
    }

    public interface IB_ComputerPlayer_TTT : IComputerGamePlayer
    {
        IB_Move_TTT GetTTTMove(IB_Field_TTT field);
    }

    public interface IB_ComputerPlayerSchlau_TTT : IComputerGamePlayer
    {
        IB_Move_TTT GetTTTMove(IB_Field_TTT field);
    }

}
