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
        public double Ballsize { get; set; } = 85; // Default ball size

        double _rotationangle = 0;
        double _rotationvelo = 1.5;

        public void B_Paint_Ball(Canvas canvas, int fieldStyle)
        {
            switch (fieldStyle)
            {
                case 0:
                    Ellipse ball = new Ellipse
                    {
                        Width = Ballsize,
                        Height = Ballsize,
                        Fill = Brushes.Red
                    };
                    Canvas.SetLeft(ball, Pos_x - Ballsize / 2);
                    Canvas.SetTop(ball, Pos_y - Ballsize / 2);
                    canvas.Children.Add(ball);
                    break;
                case 1:
                    var ball_img = new Image
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
                    ball_img.RenderTransform = new RotateTransform(_rotationangle, Ballsize / 2, Ballsize / 2);

                    Canvas.SetLeft(ball_img, Pos_x - Ballsize / 2);
                    Canvas.SetTop(ball_img, Pos_y - Ballsize / 2);
                    canvas.Children.Add(ball_img);
                    break;
                case 2:
                    var ball_drw = new Image
                    {
                        Width = Ballsize,
                        Height = Ballsize,
                        Source = new BitmapImage(new Uri("/Classes/B_Gruppe/Volley/Grafiken/Volleyball_Drawing.PNG", UriKind.Relative))
                    };
                    // Calculate the rotation angle based on the velocity
                    _rotationangle += Math.Sqrt(Velo_x * Velo_x + Velo_y * Velo_y) * _rotationvelo;
                    if (_rotationangle > 360)
                    {
                        _rotationangle -= 360;
                    }

                    // Apply the rotation transform
                    ball_drw.RenderTransform = new RotateTransform(_rotationangle, Ballsize / 2, Ballsize / 2);

                    Canvas.SetLeft(ball_drw, Pos_x - Ballsize / 2);
                    Canvas.SetTop(ball_drw, Pos_y - Ballsize / 2);
                    canvas.Children.Add(ball_drw);
                    break;
            }

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

            // Begrenze die maximale Ballgeschwindigkeit
            double maxSpeed = 120;
            double currentSpeed = Math.Sqrt(Velo_x * Velo_x + Velo_y * Velo_y);
            if (currentSpeed > maxSpeed)
            {
                double scale = maxSpeed / currentSpeed;
                Velo_x *= scale;
                Velo_y *= scale;
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

            // Check if ball is colliding with the net Left
            //Pos_x + Ballsize / 2 > netLeft && Pos_x - Ballsize / 2 < netRight && Pos_y + Ballsize / 2 > netTop + Ballsize / 2
            if (Pos_x + Ballsize / 2 > netLeft && Pos_x - Ballsize / 2 < netRight && Pos_y + Ballsize / 2 > netTop + Ballsize / 4)
            {
                // Reflect the ball's velocity based on collision
                if (Pos_x < field.Width / 2)
                {

                    Velo_x = -Math.Abs(Velo_x); // Ball is on the left side of the net
                    Pos_x = netLeft - Ballsize / 2;
                }
                else
                {
                    Velo_x = Math.Abs(Velo_x); // Ball is on the right side of the net
                    Pos_x = Ballsize / 2 + netRight;
                }
                Velo_y *= 0.8; // Reflect the vertical velocity
            }


            // Check if ball is colliding with the net top
            if (Pos_x + Ballsize / 2 > netLeft && Pos_x - Ballsize / 2 < netRight && Pos_y + Ballsize / 2 > netTop && Pos_y + Ballsize / 2 < netTop + Ballsize / 4)
            {
                // Reflect the ball's velocity based on collision
                if (Pos_x < field.Width / 2)
                {
                    Velo_x = -Math.Abs(Velo_x); // Ball is on the left side of the net top
                }
                else
                {
                    Velo_x = Math.Abs(Velo_x); // Ball is on the right side of the net top
                }
                Velo_y *= -0.8; // Reflect the vertical velocity
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
            // Berechne den Abstand zwischen Ball und Spieler
            double dx = Pos_x - player.Pos_x;
            double dy = Pos_y - player.Pos_y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            // Überprüfe zukünftige Position für Kollisionsvorhersage (Swept-Kollisionserkennung)
            double futurePosX = Pos_x + Velo_x;
            double futurePosY = Pos_y + Velo_y;
            double futureDx = futurePosX - player.Pos_x;
            double futureDy = futurePosY - player.Pos_y;
            double futureDistance = Math.Sqrt(futureDx * futureDx + futureDy * futureDy);

            // Prüfe auf Kollision (jetzt oder im nächsten Frame)
            if (distance <= Ballsize / 2 + player.Playersize / 2 || futureDistance <= Ballsize / 2 + player.Playersize / 2)
            {
                GravityOn = true;

                // Normalisiere den Kollisionsvektor
                double nx = dx / distance;
                double ny = dy / distance;

                // Reflektiere die Geschwindigkeit des Balls basierend auf dem Kollisionsvektor
                double relativeVeloX = Velo_x - player.Velo_x;
                double relativeVeloY = Velo_y - player.Velo_y;

                // Berechne den Reflexionsvektor
                double dotProduct = (relativeVeloX * nx + relativeVeloY * ny);
                Velo_x -= 2 * dotProduct * nx;
                Velo_y -= 2 * dotProduct * ny;

                // Dämpfung einfügen (realistisches Abprallen)
                Velo_x *= 0.9; // 90% der horizontalen Geschwindigkeit bleiben erhalten
                Velo_y *= 0.9; // 90% der vertikalen Geschwindigkeit bleiben erhalten

                // Verschiebe den Ball, damit er nicht im Spieler "stecken bleibt"
                Pos_x = player.Pos_x + nx * (Ballsize / 2 + player.Playersize / 2 + 1);
                Pos_y = player.Pos_y + ny * (Ballsize / 2 + player.Playersize / 2 + 1);
            }
        }


    }
}
