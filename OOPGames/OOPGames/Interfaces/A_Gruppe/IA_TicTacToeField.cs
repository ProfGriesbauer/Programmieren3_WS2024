using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public interface IA_TicTacToeField : IGameField
    {

        int columns { get; }

        int rows { get; }

        int width { get; }

        int height { get; }

        int cellWidth { get; }

        int cellHeight { get; }

        void AdoptCanvas (Canvas canvas);

        int this[int r, int c] { get; }
    }
}
