using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public interface ID_PaintTTT : IPaintGame
    {
        void PaintTTTField(Canvas canvas, ID_TTTGameField currentField);
    }

    public interface ID_TTTGameField : IGameField
    {
        int this[int r, int c] { get; set; }
    }

    public interface ID_TTTRules : IGameRules
    {
        ID_TTTGameField TTTField { get; }
        void DoTicTacToeMove(ID_TTTMove move);
    }

    public interface ID_TTTMove: IRowMove, IColumnMove
    {

    }

    public interface ID_HumanTTTPlayer : IHumanGamePlayer
    {
        ID_TTTMove GetMove(IMoveSelection selection, ID_TTTGameField field);
    }

    public interface ID_ComputerTTTPlayer : IComputerGamePlayer
    {
        ID_TTTMove GetMove(ID_TTTGameField field);
    }

    public interface IDrawableSymbol
    {
        void Draw(Canvas canvas, int x, int y);
    }
}
