using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class B_Ball_BV : IB_Ball_BV
    {
        public bool GravityOn { get; set; }
        public double Pos_x { get; set; }
        public double Pos_y { get; set; }
        public double Velo_x { get; set; }
        public double Velo_y { get; set; }
        public double Ballsize { get; set; } = 75; // Default ball size

        public void B_Paint_Ball(Canvas canvas)
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
        }

        public void B_Move_Ball(IB_Field_BV field)
        {
            //Decrease Velo_y for Gravity
            if (GravityOn)
            {
                Velo_y += 0.01 * field.Height;
            }


            // Update ball position based on velocity
            Pos_x += Velo_x;
            Pos_y += Velo_y;

            // Keep Ball within boundaries
            // Left side
            if (Pos_x - Ballsize / 2 < 0)
            {
                Velo_x *= -1;
            }
            //Right side
            if (Pos_x + Ballsize / 2 > field.Width)
            {
                Velo_x *= -1;
            }

            // Collision Ball with Net
            double netLeft = field.Width / 2 - field.Net.Width / 2;
            double netRight = field.Width / 2 + field.Net.Width / 2;
            double netTop = field.Height - field.Net.Height;

            // Check if ball is colliding with the net
            if (Pos_x + Ballsize / 2 > netLeft && Pos_x - Ballsize / 2 < netRight && Pos_y + Ballsize / 2 > netTop)
            {
                // Reflect the ball's velocity based on collision
                if (Pos_x < field.Width / 2)
                {
                    Velo_x = -Math.Abs(Velo_x); // Ball is on the left side of the net
                }
                else
                {
                    Velo_x = Math.Abs(Velo_x); // Ball is on the right side of the net
                }
                Velo_y *= -1; // Reflect the vertical velocity
            }

            // Check if ball is colliding with the players
            foreach (var player in field.Player)
            {
                HandlePlayerCollision(player);
            }
        }

        public int B_On_Ground(IB_Field_BV field)
        {
            if (Pos_y > field.Height - field.Ground.Height - Ballsize / 2)
            {
                if (Pos_x < field.Width / 2)
                {
                    return 1; // Bottom collision left
                }
                else if (Pos_x > field.Width / 2)
                {
                    return 0; // Bottom collision right
                }

            }
            return -1; //no collision

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
