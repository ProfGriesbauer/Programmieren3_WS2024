using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class B_Painter_BV : IB_Painter_BV
    {
        public string Name => "Blobby Volley Painter";

        public void PaintBlobbyVolley(Canvas canvas, IB_Field_BV field)
        {
            field.Height = canvas.ActualHeight;
            field.Width = canvas.ActualWidth;

            canvas.Children.Clear();


            // Spielfeld zeichnen
            field.Ground.B_Paint_Ground(canvas);

            // Netz zeichnen
            field.Net.B_Paint_Net(canvas, field.Ground);
    
            // Spieler zeichnen
            field.Player1.B_Paint_Player(canvas);
            field.Player2.B_Paint_Player(canvas);

            // Ball zeichnen
            field.Ball.B_Paint_Ball(canvas);

            // Score zeichnen
            TextBlock scorePlayer1 = new TextBlock
            {
                Text = $"{field.Rules_BV.Points[0]}",
                FontSize = 24,
                Foreground = Brushes.Black
            };
            Canvas.SetLeft(scorePlayer1, 10);
            Canvas.SetTop(scorePlayer1, 10);
            canvas.Children.Add(scorePlayer1);

            TextBlock scorePlayer2 = new TextBlock
            {
                Text = $"{field.Rules_BV.Points[1]}",
                FontSize = 24,
                Foreground = Brushes.Black
            };
            Canvas.SetRight(scorePlayer2, 10);
            Canvas.SetTop(scorePlayer2, 10);
            canvas.Children.Add(scorePlayer2);
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
