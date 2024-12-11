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
}
