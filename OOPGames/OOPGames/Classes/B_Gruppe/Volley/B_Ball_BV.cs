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
    public class BV_Ball : IB_Ball_BV
    {
        public double Pos_x { get; set; }
        public double Pos_y { get; set; }
        public double Velo_x { get; set; }
        public double Velo_y { get; set; }
        public double Ballsize { get; set; } = 20; // Default ball size

        public Canvas B_Paint_Ball(Canvas canvas)
        {
            Ellipse ball = new Ellipse
            {
                Width = Ballsize,
                Height = Ballsize,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(ball, Pos_x - Ballsize / 2);
            Canvas.SetTop(ball, Pos_y - Ballsize / 2);
            canvas.Children.Add(ball);
            return canvas;
        }

        public void B_Move_Ball()
        {
            // Update ball position based on velocity
            Pos_x += Velo_x;
            Pos_y += Velo_y;

            // Simple collision with boundaries
            if (Pos_x < Ballsize / 2 || Pos_x > 1000 - Ballsize / 2) Velo_x *= -1; // Assuming 1000 as canvas width
            if (Pos_y < Ballsize / 2) Velo_y *= -1; // Ceiling collision
        }

        public void HandlePlayerCollision(IB_Player_BV player)
        {
            double dx = Pos_x - player.Pos_x;
            double dy = Pos_y - player.Pos_y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            // Check collision
            if (distance <= Ballsize / 2 + player.Playersize / 2)
            {
                // Reflect the ball's velocity based on collision
                Velo_x = dx > 0 ? Math.Abs(Velo_x) : -Math.Abs(Velo_x);
                Velo_y = dy > 0 ? Math.Abs(Velo_y) : -Math.Abs(Velo_y);

                // Slightly push the ball away from the player to avoid sticking
                Pos_x += Velo_x;
                Pos_y += Velo_y;
            }
        }
    }
}
