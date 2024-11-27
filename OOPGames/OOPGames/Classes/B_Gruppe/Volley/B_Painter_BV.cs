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
            canvas.Children.Clear();

            // Spielfeld zeichnen
            Rectangle ground = new Rectangle
            {
                Width = canvas.ActualWidth,
                Height = canvas.ActualHeight * 0.1,
                Fill = Brushes.Green
            };
            Canvas.SetTop(ground, canvas.ActualHeight - ground.Height);
            canvas.Children.Add(ground);

            // Netz zeichnen
            Rectangle net = new Rectangle
            {
                Width = 10,
                Height = canvas.ActualHeight * 0.6,
                Fill = Brushes.White
            };
            Canvas.SetLeft(net, canvas.ActualWidth / 2 - net.Width / 2);
            Canvas.SetTop(net, canvas.ActualHeight - ground.Height - net.Height);
            canvas.Children.Add(net);

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
                Foreground = Brushes.White
            };
            Canvas.SetLeft(scorePlayer1, 10);
            Canvas.SetTop(scorePlayer1, 10);
            canvas.Children.Add(scorePlayer1);

            TextBlock scorePlayer2 = new TextBlock
            {
                Text = $"{field.Rules_BV.Points[1]}",
                FontSize = 24,
                Foreground = Brushes.White
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
