using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public class B_Painter_BV : IB_Painter_BV
    {
        public string Name => "Blobby Volley Painter";

        public void PaintBlobbyVolley(Canvas canvas, IB_Field_BV playField)
        {
            throw new NotImplementedException();
        }

        public void PaintGameField(Canvas canvas, IGameField playField)
        {
            if (playField is IB_Field_BV)
            {
                PaintBlobbyVolley(canvas, (IB_Field_BV)playField);
            }
        }

        public void TickPaintGameField(Canvas canvas, IGameField playField)
        {
            if (playField is IB_Field_BV)
            {
                PaintBlobbyVolley(canvas, (IB_Field_BV)playField);
            }
        }
    }
}
