using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    //TicTacToe specific paint game
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface OMM_BugGamePaint
    {
        //Paints the given game field on the given canvas
        //NOTE: Clearing the canvas, etc. has to be done within this function
        void PaintTicTacToeField(Canvas canvas, OMM_BugField currentField);
    }

    //TicTacToe specific game field 3x3
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface OMM_BugField : IGameField
    {
        //Indexer: returns 0 for a unused tictactoefield, 1 for player 1, 2 for player 2, etc.
        //indexed by the row r and column c
        int this[int r, int c] { get; set; }
    }

    //TicTacToe specific game rules
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface OMM_BugRules : IGameRules2
    {
        //Gets the current state of the tictactoe field; the class implementing
        //this interface should hold a game field corresponding to the rules
        //it implements
        OMM_BugField BugField { get; }

        //Adds the given move to the current tictactoe field if possible
        void DoBugMove(OMM_BugMove move);
    }

    //TicTacToeMove which is derived from row and column
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface OMM_BugMove : IRowMove, IColumnMove
    {

    }

    //TicTacToe specific human player
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface OMM_HumanBugPlayer : IHumanGamePlayer
    {
        //Returns a valid move if possible for the given selection and 
        //the given state of the tic tac toe field.
        //IF THE GIVEN SELECTION IS NO VALID MOVE, NULL HAS TO BE RETURNED.
        OMM_BugMove GetMove(IMoveSelection selection, OMM_BugField field);
    }

    //TicTacToe specific human player
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface OMM_ComputerBugPlayer : IComputerGamePlayer
    {
        //Returns a valid move and the given state of the tic tac toe field.
        OMM_BugMove GetMove(OMM_BugField field);
    }
}
