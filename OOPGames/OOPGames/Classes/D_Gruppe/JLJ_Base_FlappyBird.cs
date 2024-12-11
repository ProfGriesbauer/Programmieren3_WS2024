using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames.Classes.D_Gruppe
{
    public abstract class D_FB_Base_FlappyPainter : ID_FB_Painter
    {
        public abstract string Name { get; }

        public abstract void PaintFlappyField(Canvas canvas, ID_FB_GameField currentField);

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (currentField is ID_FB_GameField)
            {
                PaintFlappyField(canvas, (ID_FB_GameField)currentField);
            }
        }
        public abstract void FlappyTickGameField(Canvas canvas, ID_FB_GameField currentField);
      
        public void TickPaintGameField(Canvas canvas, IGameField currentField)
        {
            if (currentField is ID_FB_GameField)
            {
                FlappyTickGameField(canvas, (ID_FB_GameField)currentField);
            }
        }
    }

    public abstract class D_FB_Base_FlappyFiedl : ID_FB_GameField
    {
        public abstract D_Bird Bird { get ; set; }
        public abstract List<D_Tubes> Obstacles { get ; set; }
        public abstract List<D_Boden> Boden { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is ID_FB_Painter;
        }
    }

    public abstract class D_FB_Base_FlappyRules : ID_FB_Rules
    {
        public abstract ID_FB_GameField FlappyField { get; }

        public abstract bool MovesPossible { get; }

        public abstract string Name { get; }

        public abstract int CheckIfPLayerWon();

        public abstract void ClearField();

        public abstract void DoFlappyMove(ID_FB_Move move);

        public IGameField CurrentField { get { return FlappyField; } }

        public void DoMove(IPlayMove move)
        {
            if (move is ID_FB_Move)
            {
                DoFlappyMove((ID_FB_Move)move);
            }
        }

        public abstract string StatusBar();

        public abstract void StartedGameCall();
     
        public abstract void TickGameCall();
    }

    public abstract class D_FB_Base_HumanFlappyPlayer : ID_FB_HumanPlayer
    {
        public abstract string Name { get; }

        public abstract int PlayerNumber { get; }

        public abstract ID_FB_Move GetMove(IMoveSelection selection, ID_FB_GameField field);

        public abstract void SetPlayerNumber(int playerNumber);

        public abstract IGamePlayer Clone();

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is ID_FB_Rules;
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (field is ID_FB_GameField)
            {
                return GetMove(selection, (ID_FB_GameField)field);
            }
            else
            {
                return null;
            }
        }
    }

}
