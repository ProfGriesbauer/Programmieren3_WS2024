using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class D_FB_Painter : ID_FB_Painter
    {
        public string Name => throw new NotImplementedException();

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            throw new NotImplementedException();
        }

        public void TickPaintGameField(Canvas canvas, IGameField currentField)
        {
            throw new NotImplementedException();
        }
    }

    public class D_FB_GameField : ID_FB_GameField
    {
        public bool CanBePaintedBy(IPaintGame painter)
        {
            throw new NotImplementedException();
        }
    }

    public class D_FB_Rules : ID_FB_Rules
    {
        public string Name => throw new NotImplementedException();

        public IGameField CurrentField => throw new NotImplementedException();

        public bool MovesPossible => throw new NotImplementedException();

        public int CheckIfPLayerWon()
        {
            throw new NotImplementedException();
        }

        public void ClearField()
        {
            throw new NotImplementedException();
        }

        public void DoMove(IPlayMove move)
        {
            throw new NotImplementedException();
        }

        public void StartedGameCall()
        {
            throw new NotImplementedException();
        }

        public void TickGameCall()
        {
            throw new NotImplementedException();
        }
    }

    public class D_FB_Move : ID_FB_Move
    {
        
    }

    public class D_FBHumanPlayer : ID_FB_HumanPlayer
    {
        public string Name => throw new NotImplementedException();

        public int PlayerNumber => throw new NotImplementedException();

        public bool CanBeRuledBy(IGameRules rules)
        {
            throw new NotImplementedException();
        }

        public IGamePlayer Clone()
        {
            throw new NotImplementedException();
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            throw new NotImplementedException();
        }

        public void SetPlayerNumber(int playerNumber)
        {
            throw new NotImplementedException();
        }
    }
}
