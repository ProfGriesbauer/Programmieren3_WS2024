using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public interface ID_FB_Painter : IPaintGame2
    {
        void PaintFlappyField(Canvas canvas, ID_FB_GameField currentField);

        void FlappyTickGameField(Canvas canvas, ID_FB_GameField currentField);
    }

	public interface ID_FB_GameField : IGameField
    { 
        D_Bird Bird { get; set; }
        List<D_Tubes> Obstacles { get; set; }
        List<D_Boden> Boden { get; set; } // Liste der Bodenteile
        int Width { get; set; }
        int Height { get; set; }
    }

    public interface ID_FB_Rules : IGameRules3
    {
        ID_FB_GameField FlappyField { get; }

        void DoFlappyMove(ID_FB_Move move);
    }

    public interface ID_FB_Move : IPlayMove
    {
        
    }

    public interface ID_FB_HumanPlayer : IHumanGamePlayer
    {
        ID_FB_Move GetMove(IMoveSelection selection, ID_FB_GameField field);
    }
    public interface ID_GameObject
    {
        int X { get; }
        int Y { get; }
        int Width { get; }
        int Height { get; }

        void MoveLeft(int speed);
        bool CheckCollision(D_Bird bird);
        bool IsOutOfScreen();
    }
}
