using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        double _rotationangle = 0;
        double _rotationvelo = 1.5;

        public void B_Paint_Ball(Canvas canvas)
        {
            //Ball as Ellipse
            /*
            Ellipse ball = new Ellipse
            {
                Width = Ballsize,
                Height = Ballsize,
                Fill = Brushes.Red
            };
            */

            //Ball as Image
            var ball = new Image
            {
                Width = Ballsize,
                Height = Ballsize,
                Source = new BitmapImage(new Uri("/Classes/B_Gruppe/Volley/Grafiken/Volleyball.PNG", UriKind.Relative))
            };
            // Calculate the rotation angle based on the velocity
            _rotationangle += Math.Sqrt(Velo_x * Velo_x + Velo_y * Velo_y) * _rotationvelo;
            if (_rotationangle > 360)
            {
                _rotationangle -= 360;
            }

            // Apply the rotation transform
            ball.RenderTransform = new RotateTransform(_rotationangle, Ballsize / 2, Ballsize / 2);

            Canvas.SetLeft(ball, Pos_x - Ballsize / 2);
            Canvas.SetTop(ball, Pos_y - Ballsize / 2);
            canvas.Children.Add(ball);

            // Draw arrow if ball is above the visible area
            if (Pos_y < 0 - Ballsize / 2)
            {
                double arrowSize = 20;
                double arrowPosX = Pos_x - arrowSize / 2;
                double arrowPosY = 0;

                Polygon arrow = new Polygon
                {
                    Points = new PointCollection
                         {
                         new System.Windows.Point(arrowPosX, arrowPosY + arrowSize),
                         new System.Windows.Point(arrowPosX + arrowSize, arrowPosY + arrowSize),
                         new System.Windows.Point(arrowPosX + arrowSize / 2, arrowPosY)
                         },
                    Fill = Brushes.Red
                };

                canvas.Children.Add(arrow);
            }
        }

        public void B_Move_Ball(IB_Field_BV field)
        {
            //Decrease Velo_y for Gravity
            if (GravityOn)
            {
                Velo_y += 0.007 * field.Height;
            }


            // Update ball position based on velocity
            Pos_x += Velo_x;
            Pos_y += Velo_y;

            // Keep Ball within boundaries
            // Left side
            if (Pos_x - Ballsize / 2 < 0)
            {
                Velo_x *= -1;
                Pos_x = Ballsize / 2 + 1;
            }
            //Right side
            if (Pos_x + Ballsize / 2 > field.Width)
            {
                Velo_x *= -1;
                Pos_x = field.Width - Ballsize / 2 - 1;
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
                GravityOn = true;
                // Normalize the direction vector
                double nx = dx / distance;
                double ny = dy / distance;

                // Reflect the ball's velocity based on collision
                double dotProduct = 0.8 * Velo_x * nx + 0.8 * Velo_y * ny - (player.Velo_x * nx + player.Velo_y * ny);
                Velo_x = -dotProduct * nx;
                Velo_y = -dotProduct * ny;

                // Move the ball away from the player
                Pos_x = player.Pos_x + nx * (Ballsize / 2 + player.Playersize / 2 + 1);
                Pos_y = player.Pos_y + ny * (Ballsize / 2 + player.Playersize / 2 + 1);

                // Slightly push the ball away from the player to avoid sticking
                //Pos_x += Velo_x;
                //Pos_y += Velo_y;
            }
        }
    }
}
