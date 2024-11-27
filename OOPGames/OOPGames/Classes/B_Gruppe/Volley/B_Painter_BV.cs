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
            Ellipse player1 = new Ellipse
            {
                Width = 50,
                Height = 50,
                Fill = Brushes.Blue
            };
            Canvas.SetLeft(player1, field.Player1.Pos_x);
            Canvas.SetTop(player1, field.Player1.Pos_y);
            canvas.Children.Add(player1);

            Ellipse player2 = new Ellipse
            {
                Width = 50,
                Height = 50,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(player2, field.Player2.Pos_x);
            Canvas.SetTop(player2, field.Player2.Pos_y);
            canvas.Children.Add(player2);

            // Ball zeichnen
            Ellipse ball = new Ellipse
            {
                Width = 30,
                Height = 30,
                Fill = Brushes.Yellow
            };
            Canvas.SetLeft(ball, field.Ball.Pos_x);
            Canvas.SetTop(ball, field.Ball.Pos_y);
            canvas.Children.Add(ball);
        
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
